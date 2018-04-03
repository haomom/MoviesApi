﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Repository.Models
{
    public class UserMovieRating
    {
        public int UserMovieRatingId { get; set; }

        public int UserId { get; set; }

        public int MovieId { get; set; }

        public int Rating { get; set; }
    }
}
