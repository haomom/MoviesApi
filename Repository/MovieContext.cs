using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Repository.Models;

namespace Movie.Repository
{
    public class MovieContext : DbContext
    {
        public MovieContext() : base("Movie")
        {
        }

        public DbSet<Movies.Repository.Models.Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserMovieRating> UserMovieRating { get; set; }
    }
}
