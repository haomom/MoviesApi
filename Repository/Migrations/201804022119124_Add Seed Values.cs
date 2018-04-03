namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeedValues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Genre = c.String(),
                        YearOfRelease = c.Int(nullable: false),
                        RunningTime = c.Int(nullable: false),
                        AverageRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserMovieRatings",
                c => new
                    {
                        UserMovieRatingId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserMovieRatingId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserMovieRatings");
            DropTable("dbo.Movies");
        }
    }
}
