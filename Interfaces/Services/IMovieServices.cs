using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Interfaces
{
    public interface IMovieServices
    {
        Task UpdateUserMovieRating(IUserMovieRatingDto userMovieRatingDto);

        Task<IList<IMoviesDto>> GetMovies(string title, string genre, int? yearOfRelease);

        Task<IList<IMoviesDto>> GetTopRatingMovies();

        Task<IList<IMoviesDto>> GetTopRatingMoviesOfUser(int userId);
    }
}
