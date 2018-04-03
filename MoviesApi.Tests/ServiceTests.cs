using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Movie.Services;
using Movies.Interfaces;

namespace MoviesApi.Tests
{
    [TestClass]
    public class ServiceTests
    {
        private IMovieServices _movieServices;

        private Mock<IMovieRepository> _movieRepositoryMock = new Mock<IMovieRepository>();
        private Mock<IUserMovieRatingDto> _userMovieRatingDtoMock = new Mock<IUserMovieRatingDto>();

        [TestInitialize]
        public void Initialise()
        {
            _movieServices = new MovieServices(_movieRepositoryMock.Object);
        }

        [TestMethod]
        public async Task
            When_calling_UpdateUserMovieRating_it_should_call_UpdateUserMovieRating_from_movieRepository_class()
        {
            //arrange
            _movieRepositoryMock
                .Setup(x => x.UpdateUserMovieRating(_userMovieRatingDtoMock.Object))
                .Returns(Task.CompletedTask);

            //act
            await _movieServices.UpdateUserMovieRating(_userMovieRatingDtoMock.Object);

            //assert
            _movieRepositoryMock.Verify(x => x.UpdateUserMovieRating(_userMovieRatingDtoMock.Object), Times.Once);
        }
    }
}
