using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Interfaces
{
    public interface IUserMovieRatingDto
    {
        int UserId { get; set; }

        int MovieId { get; set; }

        int Rating { get; set; }
    }
}
