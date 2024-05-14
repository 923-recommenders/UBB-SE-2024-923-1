using Humanizer.Localisation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1_TEST.Controllers
{
    public class TrendsControllerTest
    {
        [Fact]
        public async Task GetAllTrends_GetListOfTrends_ReturnsOkResult()
        {
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Trends>());
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.GetTrends();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Trends>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<Trends>>(okResult.Value);
            Assert.Empty(model);
        }

        [Fact]
        public async Task GetTrendById_GetExistingTrend_ReturnsOkResult()
        {
            var country = "Test Country";
            var genre = "Test Genre";
            var language = "Test Language";
            var songId = 1;
            var trend = new Trends {SongId = 1, Genre = "Test Genre", Language = "Test Language", Country = "Test Country"};
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.GetByFourIdentifiers(songId,genre,language,country)).ReturnsAsync(trend);
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.GetTrendById(songId, genre, language,country);

            var actionResult = Assert.IsType<ActionResult<Trends>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<Trends>(okResult.Value);
            Assert.Equal(songId, model.SongId);
        }

        [Fact]
        public async Task GetFeatureById_WhenFeatureDoesNotExist_ReturnsNotFound()
        {
            var country = "Test Country";
            var genre = "Test Genre";
            var language = "Test Language";
            var songId = 100;
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.GetByFourIdentifiers(songId, genre, language, country)).ReturnsAsync((Trends)null);
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.GetTrendById(songId, genre, language, country);

            var actionResult = Assert.IsType<ActionResult<Trends>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }


        [Fact]
        public async Task PostNewFeature_ValidInput_ReturnsOkResult()
        {
            var trend = new Trends { SongId = 1, Genre = "Test Genre", Language = "Test Language", Country = "Test Country" };
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.Add(trend)).Returns(Task.CompletedTask);
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.PostTrend(trend);

            var actionResult = Assert.IsType<ActionResult<Trends>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostNewFeature_NullInput_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.Add(null)).Returns(Task.FromException<BadRequest>);
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.PostTrend(null);

            var actionResult = Assert.IsType<ActionResult<Trends>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostFeature_WhenExceptionThrown_ReturnsStatusCode()
        {
            var songDataBaseModel = new Trends { };
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.Add(songDataBaseModel)).Returns(Task.FromException<Exception>);

            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.PostTrend(songDataBaseModel);

            var actionResult = Assert.IsType<ActionResult<Trends>>(result);
            var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }



        [Fact]
        public async Task PutFeature_ValidInput_ReturnsOkResult()
        {
            var country = "Test Country";
            var genre = "Test Genre";
            var language = "Test Language";
            var songId = 1;
            var trend = new Trends { SongId = 1, Genre = "Test Genre", Language = "Test Language", Country = "Test Country" };
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.Update(trend)).Returns(Task.CompletedTask);
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.PutTrend(songId, genre, language, country, trend);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutFeature_WhenSongDoesNotExist_ReturnsBadRequest()
        {
            var country = "Test Country";
            var genre = "Test Genre";
            var language = "Test Language";
            var songId = 100;
            var trend = new Trends { SongId = 1, Genre = "Test Genre", Language = "Test Language", Country = "Test Country" };
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.Update(trend)).Returns(Task.FromException<BadRequest>);
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.PutTrend(songId, genre, language, country, trend);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutFeature_WhenConcurrencyExceptionThrown_ReturnsNoContent()
        {
            var country = "Test Country";
            var genre = "Test Genre";
            var language = "Test Language";
            var songId = 1;
            var trend = new Trends { SongId = 1, Genre = "Test Genre", Language = "Test Language", Country = "Test Country" };
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.Update(trend)).ThrowsAsync(new DbUpdateConcurrencyException());

            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.PutTrend(songId, genre, language,country,trend);

            Assert.IsType<NoContentResult>(result);
        }



        [Fact]
        public async Task DeleteFeature_ValidInput_ReturnsOkResult()
        {
            var country = "Test Country";
            var genre = "Test Genre";
            var language = "Test Language";
            var songId = 1;
            var trend = new Trends { SongId = 1, Genre = "Test Genre", Language = "Test Language", Country = "Test Country" };
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.GetByFourIdentifiers(songId, genre, language, country)).ReturnsAsync(trend);
            mockRepository.Setup(repo => repo.Delete(trend)).Returns(Task.CompletedTask);
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.DeleteTrend(songId, genre, language, country);
            var actionResult = Assert.IsType<ActionResult<Trends>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteFeature_InvalidId_ReturnsNotFound()
        {
            var country = "Test Country";
            var genre = "Test Genre";
            var language = "Test Language";
            var songId = 1;
            var mockRepository = new Mock<IRepository<Trends>>();
            mockRepository.Setup(repo => repo.GetByFourIdentifiers(songId, genre, language, country)).ReturnsAsync((Trends)null);
            var controller = new TrendsController(mockRepository.Object);

            var result = await controller.DeleteTrend(songId, genre, language, country);
            var actionResult = Assert.IsType<ActionResult<Trends>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
