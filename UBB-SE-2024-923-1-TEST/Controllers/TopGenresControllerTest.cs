using Microsoft.AspNetCore.Mvc;
using Moq;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1_TEST.Controllers
{
    public class TopGenresControllerTest
    {
        [Fact]
        public async Task GetTopGenres_ValidMonthAndYear_ReturnsTop3Genres()
        {
            // Arrange
            var mockService = new Mock<ITopGenresService>();
            mockService.Setup(service => service.GetTop3Genres(1, 2024))
               .ReturnsAsync(new List<GenreData> { new GenreData("Genre1", 1,0), new GenreData("Genre2", 1, 0), new GenreData("Genre3", 1, 0)});

            var controller = new TopGenresController(mockService.Object);

            // Act
            var result = await controller.GetTopGenres(1, 2024);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<GenreData>>(okResult.Value);
            Assert.Equal(3, model.Count());
        }
        [Fact]
        public async Task GetTopSubGenres_ValidMonthAndYear_ReturnsTop3SubGenres()
        {
            // Arrange
            var mockService = new Mock<ITopGenresService>();
            mockService.Setup(service => service.GetTop3SubGenres(1, 2024))
               .ReturnsAsync(new List<GenreData> { new GenreData("SubGenre1", 1, 0), new GenreData("SubGenre2", 1, 0), new GenreData("SubGenre3", 1, 0) });

            var controller = new TopGenresController(mockService.Object);

            // Act
            var result = await controller.GetTopSubGenres(1, 2024);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<IEnumerable<GenreData>>(okResult.Value);
            Assert.Equal(3, model.Count());
        }
    }
}
