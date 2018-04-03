using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Interfaces;

namespace Movie.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IMovieRepository _movieRepository;

        public MovieServices(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task UpdateUserMovieRating(IUserMovieRatingDto userMovieRatingDto)
        {
            await _movieRepository.UpdateUserMovieRating(userMovieRatingDto);
        }

        public async Task<IList<IMoviesDto>> GetMovies(string title, string genre, int? yearOfRelease)
        {
            var result = await _movieRepository.GetMovies(title, genre, yearOfRelease);

            return result.ToList();
        }

        public async Task<IList<IMoviesDto>> GetTopRatingMovies()
        {
            var result = await _movieRepository.GetTopRatingMovies();

            return result.ToList();
        }

        public async Task<IList<IMoviesDto>> GetTopRatingMoviesOfUser(int userId)
        {
            var result = await _movieRepository.GetTopRatingMoviesByUser(userId);

            return result.ToList();
        }
    }
}
