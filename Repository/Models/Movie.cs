using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Repository.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int YearOfRelease { get; set; }

        public int RunningTime { get; set; }

        public double AverageRating { get; set; }
    }
}
