using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1_TEST.Controllers
{
    public class SongFeaturesControllerTest
    {
        [Fact]
        public async Task GetAllFeatures_GetListOfFeatures_ReturnsOkResult()
        {
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<SongFeatures>());
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.GetSongFeatures();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<SongFeatures>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<SongFeatures>>(okResult.Value);
            Assert.Empty(model);
        }

        [Fact]
        public async Task GetFeatureById_GetExistingFeature_ReturnsOkResult()
        {
            var songId = 1;
            var artistId = 1;
            var song = new SongFeatures { ArtistId = 1, SongId = 1 };
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.GetByTwoIdentifiers(songId, artistId)).ReturnsAsync(song);
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.GetSongFeaturesById(songId, artistId);

            var actionResult = Assert.IsType<ActionResult<SongFeatures>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<SongFeatures>(okResult.Value);
            Assert.Equal(songId, model.SongId);
        }

        [Fact]
        public async Task GetFeatureById_WhenFeatureDoesNotExist_ReturnsNotFound()
        {
            var songId = 100;
            var artistId = 100;
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.GetByTwoIdentifiers(songId, artistId)).ReturnsAsync((SongFeatures)null);
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.GetSongFeaturesById(songId, artistId);

            var actionResult = Assert.IsType<ActionResult<SongFeatures>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }


        [Fact]
        public async Task PostNewFeature_ValidInput_ReturnsOkResult()
        {
            var song = new SongFeatures { ArtistId = 1, SongId = 1 };
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.Add(song)).Returns(Task.CompletedTask);
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.PostSongFeatures(song);

            var actionResult = Assert.IsType<ActionResult<SongFeatures>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostNewFeature_NullInput_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.Add(null)).Returns(Task.FromException<BadRequest>);
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.PostSongFeatures(null);

            var actionResult = Assert.IsType<ActionResult<SongFeatures>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostFeature_WhenExceptionThrown_ReturnsStatusCode()
        {
            var songDataBaseModel = new SongFeatures { };
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.Add(songDataBaseModel)).Returns(Task.FromException<Exception>);

            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.PostSongFeatures(songDataBaseModel);

            var actionResult = Assert.IsType<ActionResult<SongFeatures>>(result);
            var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }



        [Fact]
        public async Task PutFeature_ValidInput_ReturnsOkResult()
        {
            var songId = 1;
            var artistId = 1;
            var song = new SongFeatures { ArtistId = 1, SongId = 1 };
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.Update(song)).Returns(Task.CompletedTask);
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.PutSongFeatures(songId, artistId, song);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutFeature_WhenSongDoesNotExist_ReturnsBadRequest()
        {
            var songId = 1;
            var artistId = 1;
            var song = new SongFeatures { ArtistId = 100, SongId = 100 };
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.Update(song)).Returns(Task.FromException<BadRequest>);
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.PutSongFeatures(songId, artistId, song);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutFeature_WhenConcurrencyExceptionThrown_ReturnsNoContent()
        {
            var songId = 1;
            var artistId = 1;
            var song = new SongFeatures { ArtistId = 1, SongId = 1 };
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.Update(song)).ThrowsAsync(new DbUpdateConcurrencyException());

            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.PutSongFeatures(songId, artistId, song);

            Assert.IsType<NoContentResult>(result);
        }



        [Fact]
        public async Task DeleteFeature_ValidInput_ReturnsOkResult()
        {
            var songId = 1;
            var artistId = 1;
            var song = new SongFeatures { ArtistId = 1, SongId = 1 };
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.GetByTwoIdentifiers(songId, artistId)).ReturnsAsync(song);
            mockRepository.Setup(repo => repo.Delete(song)).Returns(Task.CompletedTask);
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.DeleteSongFeatures(songId, artistId);
            var actionResult = Assert.IsType<ActionResult<SongFeatures>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task DeleteFeature_InvalidId_ReturnsNotFound()
        {
            var songId = 100;
            var artistId = 100;
            var mockRepository = new Mock<IRepository<SongFeatures>>();
            mockRepository.Setup(repo => repo.GetByTwoIdentifiers(songId, artistId)).ReturnsAsync((SongFeatures)null);
            var controller = new SongFeaturesController(mockRepository.Object);

            var result = await controller.DeleteSongFeatures(songId, artistId);
            var actionResult = Assert.IsType<ActionResult<SongFeatures>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}