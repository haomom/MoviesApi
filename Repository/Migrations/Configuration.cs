using Movies.Repository.Models;

namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Movie.Repository.MovieContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Movie.Repository.MovieContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Movies.AddOrUpdate(x => x.Id,
                new Movies.Repository.Models.Movie { Id = 1, Title = "Avengers Assemble", Genre = "SuperHero", RunningTime = 134, YearOfRelease = 2004 },
                new Movies.Repository.Models.Movie { Id = 2, Title = "Justice Leagues", Genre = "SuperHero", RunningTime = 135, YearOfRelease = 2007 },
                new Movies.Repository.Models.Movie { Id = 3, Title = "Wonder Women", Genre = "SuperHero", RunningTime = 128, YearOfRelease = 2007 },
                new Movies.Repository.Models.Movie { Id = 4, Title = "Thor", Genre = "SuperHero", RunningTime = 134, YearOfRelease = 2003 },
                new Movies.Repository.Models.Movie { Id = 5, Title = "Iron Man", Genre = "SuperHero", RunningTime = 134, YearOfRelease = 2002 },
                new Movies.Repository.Models.Movie { Id = 6, Title = "SuperMan", Genre = "SuperHero", RunningTime = 134, YearOfRelease = 2005 },
                new Movies.Repository.Models.Movie { Id = 7, Title = "BatMan", Genre = "SuperHero", RunningTime = 134, YearOfRelease = 2004 });

            context.Users.AddOrUpdate(x => x.UserId,
                new User { UserId = 1, UserName = "Tom" },
                new User { UserId = 2, UserName = "Alex" },
                new User { UserId = 3, UserName = "Peter" },
                new User { UserId = 4, UserName = "James" },
                new User { UserId = 5, UserName = "John" });

            context.UserMovieRating.AddOrUpdate(x => x.UserMovieRatingId,
                new UserMovieRating { UserId = 1, MovieId = 1, Rating = 3 },
                new UserMovieRating { UserId = 1, MovieId = 3, Rating = 2 },
                new UserMovieRating { UserId = 1, MovieId = 5, Rating = 5 },
                new UserMovieRating { UserId = 1, MovieId = 7, Rating = 4 },
                new UserMovieRating { UserId = 2, MovieId = 2, Rating = 3 },
                new UserMovieRating { UserId = 2, MovieId = 4, Rating = 3 },
                new UserMovieRating { UserId = 2, MovieId = 6, Rating = 4 },
                new UserMovieRating { UserId = 3, MovieId = 7, Rating = 4 },
                new UserMovieRating { UserId = 3, MovieId = 2, Rating = 3 },
                new UserMovieRating { UserId = 3, MovieId = 1, Rating = 4 },
                new UserMovieRating { UserId = 5, MovieId = 3, Rating = 4 },
                new UserMovieRating { UserId = 5, MovieId = 4, Rating = 3 },
                new UserMovieRating { UserId = 5, MovieId = 5, Rating = 5 }
                );
        }
    }
}
