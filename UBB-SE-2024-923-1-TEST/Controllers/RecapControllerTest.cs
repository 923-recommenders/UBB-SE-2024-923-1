using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1_TEST.Controllers
{
    public class RecapControllerTest : IClassFixture<CustomWebApplicationFactory<RecapController>>
    {
        private readonly CustomWebApplicationFactory<RecapController> _factory;
        private readonly Mock<IRecapService> _mockRecapService;

        public RecapControllerTest(CustomWebApplicationFactory<RecapController> factory)
        {
            _factory = factory;
            _mockRecapService = new Mock<IRecapService>();
        }

        [Fact]
        public async Task GetTheTop5MostListenedSongs_ReturnsTop5Songs()
        {
            // Arrange
            var userId = 1;
            var expectedSongs = new List<SongBasicInformation>
            {
                new SongBasicInformation { SongId = 1, Name = "Song 1" },
                new SongBasicInformation { SongId = 2, Name = "Song 2" },
                // Add more songs as needed
            };
            _mockRecapService.Setup(service => service.GetTheTop5MostListenedSongs(userId)).ReturnsAsync(expectedSongs);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/Recap/getTop5MostListenedSongs/{userId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<List<SongBasicInformation>>();
            Assert.Equal(expectedSongs, content);
        }


        [Fact]
        public async Task GetTheMostPlayedSongPercentile_ReturnsMostPlayedSongPercentile()
        {
            // Arrange
            var userId = 1;
            var expectedSong = new SongBasicInformation { SongId = 1, Name = "Song 1" };
            var expectedPercentile = 0.85m;
            _mockRecapService.Setup(service => service.GetTheMostPlayedSongPercentile(userId)).ReturnsAsync(new Tuple<SongBasicInformation, decimal>(expectedSong, expectedPercentile));

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/Recap/getMostPlayedSongPercentile/{userId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<Tuple<SongBasicInformation, decimal>>();
            Assert.Equal(expectedSong, content.Item1);
            Assert.Equal(expectedPercentile, content.Item2);
        }

        [Fact]
        public async Task GetTheMostPlayedArtistPercentile_ReturnsMostPlayedArtistPercentile()
        {
            // Arrange
            var userId = 1;
            var expectedArtist = "Artist 1";
            var expectedPercentile = 0.75m;
            _mockRecapService.Setup(service => service.GetTheMostPlayedArtistPercentile(userId)).ReturnsAsync(new Tuple<string, decimal>(expectedArtist, expectedPercentile));

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/Recap/getMostPlayedArtistPercentile/{userId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<Tuple<string, decimal>>();
            Assert.Equal(expectedArtist, content.Item1);
            Assert.Equal(expectedPercentile, content.Item2);
        }

        [Fact]
        public async Task GetTotalMinutesListened_ReturnsTotalMinutesListened()
        {
            // Arrange
            var userId = 1;
            var expectedMinutes = 120;
            _mockRecapService.Setup(service => service.GetTotalMinutesListened(userId)).ReturnsAsync(expectedMinutes);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/Recap/getTotalMinutesListened/{userId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<int>();
            Assert.Equal(expectedMinutes, content);
        }

        [Fact]
        public async Task GetTheTop5Genres_ReturnsTop5Genres()
        {
            // Arrange
            var userId = 1;
            var expectedGenres = new List<string> { "Genre 1", "Genre 2" };
            _mockRecapService.Setup(service => service.GetTheTop5Genres(userId)).ReturnsAsync(expectedGenres);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/Recap/getTop5Genres/{userId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<List<string>>();
            Assert.Equal(expectedGenres, content);
        }

        [Fact]
        public async Task GetNewGenresDiscovered_ReturnsNewGenresDiscovered()
        {
            // Arrange
            var userId = 1;
            var expectedGenres = new List<string> { "Genre 3", "Genre 4" };
            _mockRecapService.Setup(service => service.GetNewGenresDiscovered(userId)).ReturnsAsync(expectedGenres);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/Recap/getNewGenresDiscovered/{userId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<List<string>>();
            Assert.Equal(expectedGenres, content);
        }

        [Fact]
        public async Task GetListenerPersonality_ReturnsListenerPersonality()
        {
            // Arrange
            var userId = 1;
            var expectedPersonality = ListenerPersonality.Melophile;
            _mockRecapService.Setup(service => service.GetListenerPersonality(userId)).ReturnsAsync(expectedPersonality);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/Recap/getListenerPersonality/{userId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<ListenerPersonality>();
            Assert.Equal(expectedPersonality, content);
        }

        [Fact]
        public async Task GenerateEndOfYearRecap_ReturnsEndOfYearRecap()
        {
            // Arrange
            var userId = 1;
            var expectedRecap = new EndOfYearRecapViewModel
            {
                Top5MostListenedSongs = new List<SongBasicInformation>
                {
                    new SongBasicInformation { SongId = 1, Name = "Song 1" },
                    new SongBasicInformation { SongId = 2, Name = "Song 2" }
                },
                MostPlayedSongPercentile = new Tuple<SongBasicInformation, decimal>(new SongBasicInformation { SongId = 1, Name = "Song 1" }, 0.85m),
                MostPlayedArtistPercentile = new Tuple<string, decimal>("Artist 1", 0.75m),
                MinutesListened = 120,
                Top5Genres = new List<string> { "Genre 1", "Genre 2" },
                NewGenresDiscovered = new List<string> { "Genre 3", "Genre 4" },
                ListenerPersonality = ListenerPersonality.Melophile
            };
            _mockRecapService.Setup(service => service.GenerateEndOfYearRecap(userId)).ReturnsAsync(expectedRecap);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/Recap/getEndOfYearRecap/{userId}");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var content = await response.Content.ReadFromJsonAsync<EndOfYearRecapViewModel>();
            Assert.Equal(expectedRecap, content);
        }

    }
}
