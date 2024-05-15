using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using UBB_SE_2024_923_1.Controllers;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace UBB_SE_2024_923_1_TEST.Controllers
{
    public class SongDataBaseModelControllerTest
    {

        [Fact]
        public async Task GetAllSongs_GetListOfSongs_ReturnsOkResult()
        {
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<SongDataBaseModel>());
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.GetSongDataBaseModels();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<SongDataBaseModel>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<SongDataBaseModel>>(okResult.Value);
            Assert.Empty(model);
        }

        [Fact]
        public async Task GetSongById_GetExistingSong_ReturnsOkResult()
        {
            var songId = 1;
            var song = new SongDataBaseModel { SongId = songId, Name = "Test Song", Genre = "Test Genre", Subgenre = "Test Subgenre", ArtistId = 1, Language = "Test Language", Country = "Test Country", Album = "Test Album", Image = "Test Image" };
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.GetById(songId)).ReturnsAsync(song);
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.GetSongDataBaseModelById(songId);

            var actionResult = Assert.IsType<ActionResult<SongDataBaseModel>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var model = Assert.IsAssignableFrom<SongDataBaseModel>(okResult.Value);
            Assert.Equal(songId, model.SongId);
        }

        [Fact]
        public async Task GetSongById_WhenSongDoesNotExist_ReturnsNotFound()
        {
            var songId = 100;
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.GetById(songId)).ReturnsAsync((SongDataBaseModel)null);
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.GetSongDataBaseModelById(songId);

            var actionResult = Assert.IsType<ActionResult<SongDataBaseModel>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }


        [Fact]
        public async Task PostNewSong_ValidInput_ReturnsOkResult()
        {
            var song = new SongDataBaseModel { SongId = 1, Name = "Test Song", Genre = "Test Genre", Subgenre = "Test Subgenre", ArtistId = 1, Language = "Test Language", Country = "Test Country", Album = "Test Album", Image = "Test Image" };
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.Add(song)).Returns(Task.CompletedTask);
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.PostSongDataBaseModel(song);

            var actionResult = Assert.IsType<ActionResult<SongDataBaseModel>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostNewSong_NullInput_ReturnsBadRequest()
        {
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.Add(null)).Returns(Task.FromException<BadRequest>);
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.PostSongDataBaseModel(null);

            var actionResult = Assert.IsType<ActionResult<SongDataBaseModel>>(result);
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task PostSongDataBaseModel_WhenExceptionThrown_ReturnsStatusCode()
        {
            var songDataBaseModel = new SongDataBaseModel { };
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.Add(songDataBaseModel)).Returns(Task.FromException<Exception>);

            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.PostSongDataBaseModel(songDataBaseModel);

            var actionResult = Assert.IsType<ActionResult<SongDataBaseModel>>(result);
            var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }



        [Fact]
        public async Task PutSong_ValidInput_ReturnsOkResult()
        {
            var songId = 1;
            var song = new SongDataBaseModel { SongId = songId, Name = "Test Song", Genre = "Test Genre", Subgenre = "Test Subgenre", ArtistId = 1, Language = "Test Language", Country = "Test Country", Album = "Test Album", Image = "Test Image" };
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.Update(song)).Returns(Task.CompletedTask);
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.PutSongDataBaseModel(songId, song);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutSong_WhenSongDoesNotExist_ReturnsBadRequest()
        {
            var songId = 1;
            var song = new SongDataBaseModel { SongId = 100, Name = "Test Song", Genre = "Test Genre", Subgenre = "Test Subgenre", ArtistId = 1, Language = "Test Language", Country = "Test Country", Album = "Test Album", Image = "Test Image" };
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.Update(song)).Returns(Task.FromException<BadRequest>);
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.PutSongDataBaseModel(songId, song);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutSong_WhenConcurrencyExceptionThrown_ReturnsNoContent()
        {
            var songId = 1;
            var song = new SongDataBaseModel { SongId = songId, Name = "Test Song", Genre = "Test Genre", Subgenre = "Test Subgenre", ArtistId = 1, Language = "Test Language", Country = "Test Country", Album = "Test Album", Image = "Test Image" };
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.Update(song)).ThrowsAsync(new DbUpdateConcurrencyException());

            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.PutSongDataBaseModel(songId, song);

            Assert.IsType<NoContentResult>(result);
        }



        [Fact]
        public async Task DeleteSong_ValidInput_ReturnsOkResult()
        {
            var songId = 1;
            var song = new SongDataBaseModel { SongId = songId, Name = "Test Song", Genre = "Test Genre", Subgenre = "Test Subgenre", ArtistId = 1, Language = "Test Language", Country = "Test Country", Album = "Test Album",  Image = "Test Image" };
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.GetById(songId)).ReturnsAsync(song);
            mockRepository.Setup(repo => repo.Delete(song)).Returns(Task.CompletedTask);
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.DeleteSongDataBaseModel(songId);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteSong_InvalidId_ReturnsNotFound()
        {
            var songId = 100;
            var mockRepository = new Mock<IRepository<SongDataBaseModel>>();
            mockRepository.Setup(repo => repo.GetById(songId)).ReturnsAsync((SongDataBaseModel)null);
            var controller = new SongDataBaseModelController(mockRepository.Object);

            var result = await controller.DeleteSongDataBaseModel(songId);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
