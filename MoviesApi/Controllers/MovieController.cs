using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using log4net;
using Movies.Interfaces;

namespace MoviesApi.Controllers
{
    [RoutePrefix("api/Movies")]
    public class MovieController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IUserMovieRatingDto _userMovieRatingDto;
        private readonly IMovieServices _movieServices;

        public MovieController(IUserMovieRatingDto userMovieRatingDto, IMovieServices movieServices)
        {
            _userMovieRatingDto = userMovieRatingDto;
            _movieServices = movieServices;
        }

        [HttpPut]
        [Route("RecordUserMovieRating/{userId:int}")]
        public async Task<IHttpActionResult> RecordUserMovieRating(int userId, int movieId, int rating)
        {
            if (rating < 1 || rating > 5)
            {
                return BadRequest("Rating value should be between 1 and 5");
            }

            _userMovieRatingDto.UserId = userId;
            _userMovieRatingDto.MovieId = movieId;
            _userMovieRatingDto.Rating = rating;

            try
            {
                await _movieServices.UpdateUserMovieRating(_userMovieRatingDto);

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error("Error in RecordUserMovieRating", e);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetMovies")]
        public async Task<IHttpActionResult> GetMovies(string title = "", string genre = "", int? yearOfRelease = null)
        {
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(genre) && !yearOfRelease.HasValue)
            {
                return BadRequest("search parameters are all empty");
            }

            if (yearOfRelease.HasValue && yearOfRelease.Value <= 0)
            {
                return BadRequest("Invalid year of release value");
            }

            try
            {
                IList<IMoviesDto> result = await _movieServices.GetMovies(title, genre, yearOfRelease);

                if (result.Any())
                {
                    var mappedResult = MapModels.DoMapping(result);
                    return Ok(mappedResult);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Log.Error("Error in GetMovies", e);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetTopRatingMovies")]
        public async Task<IHttpActionResult> GetTopRatingMovies()
        {
            try
            {
                IList<IMoviesDto> result = await _movieServices.GetTopRatingMovies();

                if (result.Any())
                {
                    var mappedResult = MapModels.DoMapping(result);
                    return Ok(mappedResult);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Log.Error("Error in GetTopRatingMovies", e);
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("GetTopRatingMoviesByUser/{userId:int}")]
        public async Task<IHttpActionResult> GetTopRatingMoviesByUser(int userId)
        {
            try
            {
                IList<IMoviesDto> result = await _movieServices.GetTopRatingMoviesOfUser(userId);

                if (result.Any())
                {
                    var mappedResult = MapModels.DoMapping(result);
                    return Ok(mappedResult);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                Log.Error("Error in GetTopRatingMoviesByUser", e);
                return InternalServerError();
            }
        }
    }
}