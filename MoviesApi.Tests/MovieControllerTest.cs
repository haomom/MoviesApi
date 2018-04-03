using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Movies.Interfaces;
using MoviesApi.Controllers;

namespace MoviesApi.Tests
{
    [TestClass]
    public class MovieControllerTest
    {
        private MovieController _movieController;

        private Mock<IUserMovieRatingDto> _userMovieRatingDtoMock = new Mock<IUserMovieRatingDto>();
        private Mock<IMovieServices> _movieServicesMock = new Mock<IMovieServices>();

        [TestInitialize]
        public void Initialise()
        {
            _movieController = new MovieController(_userMovieRatingDtoMock.Object, _movieServicesMock.Object);
        }

        [TestMethod]
        public async Task When_calling_RecordUserMovieRating_if_rating_is_less_than_1_then_return_badrequest()
        {
            //arrange
            //act
            IHttpActionResult result = await _movieController.RecordUserMovieRating(It.IsAny<int>(), It.IsAny<int>(), 0);
            var content = result as BadRequestErrorMessageResult;

            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual("Rating value should be between 1 and 5", content.Message);
        }

        [TestMethod]
        public async Task When_calling_RecordUserMovieRating_if_rating_is_more_than_5_then_return_badrequest()
        {
            //arrange
            //act
            IHttpActionResult result = await _movieController.RecordUserMovieRating(It.IsAny<int>(), It.IsAny<int>(), 6);
            var content = result as BadRequestErrorMessageResult;

            //assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual("Rating value should be between 1 and 5", content.Message);
        }

        [TestMethod]
        public async Task When_calling_RecordUserMovieRating_if_rating_is_between_1_and_5_then_return_ok()
        {
            //arrange
            _movieServicesMock
                .Setup(x => x.UpdateUserMovieRating(_userMovieRatingDtoMock.Object))
                .Returns(Task.CompletedTask);

            //act
            IHttpActionResult result = await _movieController.RecordUserMovieRating(It.IsAny<int>(), It.IsAny<int>(), 3);

            //assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task When_calling_RecordUserMovieRating_it_should_call_UpdateUserMovieRating_from_MovieService_class()
        {
            //arrange
            _movieServicesMock
                .Setup(x => x.UpdateUserMovieRating(_userMovieRatingDtoMock.Object))
                .Returns(Task.CompletedTask);

            //act
            IHttpActionResult result = await _movieController.RecordUserMovieRating(It.IsAny<int>(), It.IsAny<int>(), 3);

            //assert
            _movieServicesMock.Verify(x => x.UpdateUserMovieRating(_userMovieRatingDtoMock.Object), Times.Once);
        }
    }
}
