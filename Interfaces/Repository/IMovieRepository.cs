using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Interfaces
{
    public interface IMovieRepository
    {
        Task UpdateUserMovieRating(IUserMovieRatingDto userMovieRatingDto);

        Task<IEnumerable<IMoviesDto>> GetMovies(string title, string genre, int? yearOfRelease);

        Task<IEnumerable<IMoviesDto>> GetTopRatingMovies();

        Task<IEnumerable<IMoviesDto>> GetTopRatingMoviesByUser(int userId);
    }
}
