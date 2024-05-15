using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1_TEST.Controllers
{
    public class FullDetailsOnSongControllerTest
    {
        [Fact]
        public async Task GetFullDetailsOnSong_ValidSongId_ReturnsFullDetailsOnSong()
        {
            // Arrange
            var mockService = new Mock<IFullDetailsOnSongService>();
            mockService.Setup(service => service.GetFullDetailsOnSong(1)).ReturnsAsync(new FullDetailsOnSong());

            var controller = new FullDetailsOnSongController(mockService.Object);

            // Act
            var result = await controller.GetFullDetailsOnSong(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<FullDetailsOnSong>(okResult.Value);
            Assert.NotNull(result);
            Assert.Equal(0, model.TotalPlays);
            Assert.Equal(0, model.TotalLikes);
            Assert.Equal(0, model.TotalDislikes);
            Assert.Equal(0, model.TotalSkips);
            Assert.Equal(0, model.TotalMinutesListened);
        }

        [Fact]
        public async Task GetCurrentMonthDetails_ValidSongId_ReturnsFullDetailsOnSongForCurrentMonth()
        {
            // Arrange
            var mockService = new Mock<IFullDetailsOnSongService>();
            mockService.Setup(service => service.GetCurrentMonthDetails(1))
                .ReturnsAsync(new FullDetailsOnSong());

            var controller = new FullDetailsOnSongController(mockService.Object);

            // Act
            var result = await controller.GetCurrentMonthDetails(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<FullDetailsOnSong>(okResult.Value);
            Assert.NotNull(model);
            Assert.Equal(0, model.TotalPlays);
            Assert.Equal(0, model.TotalLikes);
            Assert.Equal(0, model.TotalDislikes);
            Assert.Equal(0, model.TotalSkips);
            Assert.Equal(0, model.TotalMinutesListened);
        }
    }
}
