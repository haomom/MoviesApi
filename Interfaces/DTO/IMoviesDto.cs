using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Interfaces
{
    public interface IMoviesDto
    {
        int Id { get; set; }

        string Title { get; set; }

        string Genre { get; set; }

        int YearOfRelease { get; set; }

        int RunningTime { get; set; }

        double AverageRating { get; set; }
    }
}
