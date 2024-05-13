using Microsoft.AspNetCore.Http;
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
    public class MostPlayedArtistInformationControllerTest
    {
        [Fact]
        public async Task GetMostPlayedArtistInformation_ReturnsOkResult()
        {
            // arrange
            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<MostPlayedArtistInformation>());
            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.GetMostPlayedArtistInformation();

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<MostPlayedArtistInformation>>(okResult.Value);
            Assert.Empty(model);
        }

        [Fact]
        public async Task GetMostPlayedArtistInformation_WithItems_ReturnsOkResult()
        {
            // arrange
            var mostPlayedArtists = new List<MostPlayedArtistInformation>
            {
                new MostPlayedArtistInformation { Artist_Id = 1, Start_Listen_Events = 1 },
                new MostPlayedArtistInformation { Artist_Id = 2 , Start_Listen_Events =1}
            };
            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.GetAll()).ReturnsAsync(mostPlayedArtists);
            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.GetMostPlayedArtistInformation();

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<MostPlayedArtistInformation>>(okResult.Value);
            Assert.Equal(mostPlayedArtists, model);
        }


        [Fact]
        public async Task GetMostPlayedArtistInformationById_ReturnsOkResult()
        {
            // arrange
            var artistId = 1;
            var events = 1;
            var mostPlayedArtistInformation = new MostPlayedArtistInformation { Artist_Id = artistId, Start_Listen_Events = events };

            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.GetById(artistId)).ReturnsAsync(mostPlayedArtistInformation);

            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.GetMostPlayedArtistInformationById(artistId);

            // assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<MostPlayedArtistInformation>(okResult.Value);
            Assert.Equal(artistId, model.Artist_Id);
            Assert.Equal(events, model.Start_Listen_Events);
        }
        [Fact]
        public async Task GetMostPlayedArtistInformationById_WhenNoResult_ReturnsNotFound()
        {
            // arrange
            var artistId = 1;
            MostPlayedArtistInformation nullResult = null;
            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.GetById(artistId)).ReturnsAsync(nullResult);
            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.GetMostPlayedArtistInformationById(artistId);

            // assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutMostPlayedArtistInformation_ValidInput_ReturnsOkResult()
        {
            // arrange
            var artistId = 1;
            var mostPlayedArtistInformation = new MostPlayedArtistInformation { Artist_Id = artistId };

            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.Update(mostPlayedArtistInformation)).Returns(Task.CompletedTask);

            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.PutMostPlayedArtistInformation(artistId, mostPlayedArtistInformation);

            // assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task PutMostPlayedArtistInformation_InvalidInput_ReturnsBadRequest()
        {
            // arrange
            var artistId = 1;
            var mostPlayedArtistInformation = new MostPlayedArtistInformation { Artist_Id = 2 };

            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.PutMostPlayedArtistInformation(artistId, mostPlayedArtistInformation);

            // assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task PutMostPlayedArtistInformation_ReturnsNotFound_WhenResourceNotFound()
        {
            // arrange
            var artistId = 1;
            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.Update(It.IsAny<MostPlayedArtistInformation>())).ThrowsAsync(new DbUpdateConcurrencyException());
            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.PutMostPlayedArtistInformation(artistId, new MostPlayedArtistInformation { Artist_Id = artistId });

            // assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public async Task PostMostPlayedArtistInformation_ValidInput_ReturnsOkResult()
        {
            // arrange
            var mostPlayedArtistInformation = new MostPlayedArtistInformation { Artist_Id = 1 };

            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.Add(mostPlayedArtistInformation)).Returns(Task.CompletedTask);

            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.PostMostPlayedArtistInformation(mostPlayedArtistInformation);

            // assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostMostPlayedArtistInformation_InvalidInput_ReturnsBadRequest()
        {
            // arrange
            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.PostMostPlayedArtistInformation(null);

            // assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task PostMostPlayedArtistInformation_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // arrange
            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.Add(It.IsAny<MostPlayedArtistInformation>())).ThrowsAsync(new Exception("Simulated exception"));
            var controller = new MostPlayedArtistInformationController(mockRepository.Object);
            var mostPlayedArtistInformation = new MostPlayedArtistInformation { Artist_Id = 1 };

            // act
            var result = await controller.PostMostPlayedArtistInformation(mostPlayedArtistInformation);

            // assert
            var actionResult = Assert.IsType<ActionResult<MostPlayedArtistInformation>>(result);
            var statusCodeResult = Assert.IsType<ObjectResult>(actionResult.Result);
            Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    


        [Fact]
        public async Task DeleteMostPlayedArtistInformation_ValidInput_ReturnsOkResult()
        {
            // arrange
            var artistId = 1;
            var mostPlayedArtistInformation = new MostPlayedArtistInformation { Artist_Id = artistId };

            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            mockRepository.Setup(repo => repo.GetById(artistId)).ReturnsAsync(mostPlayedArtistInformation);
            mockRepository.Setup(repo => repo.Delete(mostPlayedArtistInformation)).Returns(Task.CompletedTask);

            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.DeleteMostPlayedArtistInformation(artistId);

            // assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteMostPlayedArtistInformation_InvalidInput_ReturnsNotFound()
        {
            // arrange
            var artistId = 1;

            var mockRepository = new Mock<IRepository<MostPlayedArtistInformation>>();
            var controller = new MostPlayedArtistInformationController(mockRepository.Object);

            // act
            var result = await controller.DeleteMostPlayedArtistInformation(artistId);

            // assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

    }
}

