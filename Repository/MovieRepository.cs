using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Movie.ModelDto;
using Movie.Repository;
using Movies.Interfaces;
using Movies.Repository.Models;

namespace Repository
{
    public class MovieRepository : IMovieRepository
    {
        public async Task UpdateUserMovieRating(IUserMovieRatingDto userMovieRatingDto)
        {
            using (var context = new MovieContext())
            {
                var userMovieRating = context.UserMovieRating.FirstOrDefault(x =>
                    x.UserId == userMovieRatingDto.UserId && x.MovieId == userMovieRatingDto.MovieId);

                if (userMovieRating != null)
                {
                    userMovieRating.Rating = userMovieRatingDto.Rating;
                    context.UserMovieRating.Attach(userMovieRating);
                    context.Entry(userMovieRating).State = EntityState.Modified;
                }
                else
                {
                    userMovieRating = new UserMovieRating
                    {
                        UserId = userMovieRatingDto.UserId,
                        MovieId = userMovieRatingDto.MovieId,
                        Rating = userMovieRatingDto.Rating
                    };
                    context.UserMovieRating.Add(userMovieRating);
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<IMoviesDto>> GetMovies(string title, string genre, int? yearOfRelease)
        {
            using (var context = new MovieContext())
            {
                var moviesQueryable = from m in context.Movies
                    select m;

                if (!string.IsNullOrEmpty(title))
                {
                    moviesQueryable = moviesQueryable.Where(x => x.Title.Contains(title));
                }

                if (!string.IsNullOrEmpty(genre))
                {
                    moviesQueryable = moviesQueryable.Where(x => x.Genre.Contains(genre));
                }

                if (yearOfRelease.HasValue)
                {
                    moviesQueryable = moviesQueryable.Where(x => x.YearOfRelease == yearOfRelease.Value);
                }

                var movies = await moviesQueryable.ToListAsync();
                var movieIds = movies.Select(x => x.Id);
                
                var rating = context.UserMovieRating
                    .Where(x => movieIds.Contains(x.MovieId))
                    .GroupBy(x => x.MovieId, r => r.Rating)
                    .Select(g => new {MovieId = g.Key, Rating = g.Average()});

                var query = from m in movies
                    join r in rating on m.Id equals r.MovieId into mr
                    from r in mr.DefaultIfEmpty()
                    select new MovieDTO
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Genre = m.Genre,
                        YearOfRelease = m.YearOfRelease,
                        RunningTime = m.RunningTime,
                        AverageRating = r?.Rating ?? 0
                    };
                
                return query.OrderByDescending(x => x.AverageRating).ThenBy(x => x.Title).Cast<IMoviesDto>().ToList();
            }
        }

        public async Task<IEnumerable<IMoviesDto>> GetTopRatingMovies()
        {
            using (var context = new MovieContext())
            {
                var rating = context.UserMovieRating
                                    .GroupBy(x => x.MovieId, r => r.Rating)
                                    .Select(g => new { MovieId = g.Key, Rating = g.Average() })
                                    .OrderByDescending(x => x.Rating)
                                    .Take(5);

                var movies = await (from r in rating
                              join m in context.Movies on r.MovieId equals m.Id
                              select new MovieDTO
                              {
                                  Id = m.Id,
                                  Title = m.Title,
                                  Genre = m.Genre,
                                  YearOfRelease = m.YearOfRelease,
                                  RunningTime = m.RunningTime,
                                  AverageRating = r.Rating
                              }).ToListAsync();

                return movies.OrderByDescending(x => x.AverageRating).ThenBy(x => x.Title).Cast<IMoviesDto>().ToList();
            }
        }

        public async Task<IEnumerable<IMoviesDto>> GetTopRatingMoviesByUser(int userId)
        {
            using (var context = new MovieContext())
            {
                var rating = context.UserMovieRating
                                    .Where(x => x.UserId == userId)
                                    .GroupBy(x => x.MovieId, r => r.Rating)
                                    .Select(g => new { MovieId = g.Key, Rating = g.Average() })
                                    .OrderByDescending(x => x.Rating)
                                    .Take(5);

                var movies = await (from r in rating
                              join m in context.Movies on r.MovieId equals m.Id
                              select new MovieDTO
                              {
                                  Id = m.Id,
                                  Title = m.Title,
                                  Genre = m.Genre,
                                  YearOfRelease = m.YearOfRelease,
                                  RunningTime = m.RunningTime,
                                  AverageRating = r.Rating
                              }).ToListAsync();

                return movies.OrderByDescending(x => x.AverageRating).ThenBy(x => x.Title).Cast<IMoviesDto>().ToList();
            }
        }
    }
}
