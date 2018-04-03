using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Interfaces;

namespace Movie.ModelDto
{
    public class UserMovieRatingDto : IUserMovieRatingDto
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int Rating { get; set; }
    }
}
