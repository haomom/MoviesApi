using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesApi
{
    public class MovieResult
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int YearOfRelease { get; set; }

        public int RunningTime { get; set; }

        public double AverageRating { get; set; }
    }
}