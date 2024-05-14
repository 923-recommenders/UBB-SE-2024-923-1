using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1_TEST.Services
{
    public class TopGenresServiceTest
    {

        private Mock<IRepository<SongDataBaseModel>> songRepoMock;
        private Mock<IRepository<SongRecommendationDetails>> songRecommendationRepoMock;
        public TopGenresServiceTest()
        {
            songRepoMock = new Mock<IRepository<SongDataBaseModel>>();
            songRecommendationRepoMock = new Mock<IRepository<SongRecommendationDetails>>();
            songRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(GetExpectedSongs());
            songRecommendationRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(GetExpectedRecommendationSongs());
            
        }

        [Fact]
        public async Task GetTop3Genres_ValidMonthAndYear_ReturnsTop3Genres()
        {
            var _service = new TopGenresService(songRepoMock.Object, songRecommendationRepoMock.Object);

            // Act
            List<GenreData> result = await _service.GetTop3Genres(8, 2015);

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task GetTop3SubGenres_ValidMonthAndYear_ReturnsTop3SubGenres()
        {
            // Arrange
            var _service = new TopGenresService(songRepoMock.Object, songRecommendationRepoMock.Object);

            // Act
            var result = await _service.GetTop3SubGenres(8, 2015);

            // Assert
            Assert.Equal(3, result.Count);
        }

        private List<SongDataBaseModel> GetExpectedSongs()
        {
            return new List<SongDataBaseModel>
            {
                new SongDataBaseModel
                {
                    SongId = 1,
                    Name = "Song 1",
                    Genre = "Pop",
                    Subgenre = "Dance",
                    ArtistId = 101,
                    Language = "English",
                    Country = "USA",
                    Album = "Album 1",
                    Image = "song1_img.png"
                },
                new SongDataBaseModel
                {
                    SongId = 2,
                    Name = "Song 2",
                    Genre = "Rock",
                    Subgenre = "Hard Rock",
                    ArtistId = 102,
                    Language = "English",
                    Country = "Canada",
                    Album = "Album 2",
                    Image = "song2_img.png"
                },
                new SongDataBaseModel
                {
                    SongId = 3,
                    Name = "Song 3",
                    Genre = "Jazz",
                    Subgenre = "Smooth Jazz",
                    ArtistId = 103,
                    Language = "English",
                    Country = "UK",
                    Album = "Album 3",
                    Image = "song3_img.png"
                },
                new SongDataBaseModel
                {
                    SongId = 4,
                    Name = "Song 4",
                    Genre = "Classical",
                    Subgenre = "Symphony",
                    ArtistId = 104,
                    Language = "English",
                    Country = "Germany",
                    Album = "Album 4",
                    Image = "song4_img.png"
                },
                new SongDataBaseModel
                {
                    SongId = 5,
                    Name = "Song 5",
                    Genre = "Hip Hop",
                    Subgenre = "Rap",
                    ArtistId = 105,
                    Language = "English",
                    Country = "USA",
                    Album = "Album 5",
                    Image = "song5_img.png"
                },
                new SongDataBaseModel
                {
                    SongId = 6,
                    Name = "Song 6",
                    Genre = "Electronic",
                    Subgenre = "Techno",
                    ArtistId = 106,
                    Language = "English",
                    Country = "Australia",
                    Album = "Album 6",
                    Image = "song6_img.png"
                }
            };
        }

        private List<SongRecommendationDetails> GetExpectedRecommendationSongs()
        {
            return new List<SongRecommendationDetails>
            {
                new SongRecommendationDetails
                {
                    SongId = 4,
                    Likes = 5000,
                    Dislikes = 360,
                    MinutesListened = 15000,
                    NumberOfPlays = 270,
                    Month = 5,
                    Year = 2010
                },
                new SongRecommendationDetails
                {
                    SongId = 2,
                    Likes = 25000,
                    Dislikes = 6000,
                    MinutesListened = 120000,
                    NumberOfPlays = 43000,
                    Month = 8,
                    Year = 2015
                },
                new SongRecommendationDetails
                {
                    SongId = 3,
                    Likes = 52000,
                    Dislikes = 24000,
                    MinutesListened = 560000,
                    NumberOfPlays = 108000,
                    Month = 8,
                    Year = 2015
                },
                new SongRecommendationDetails
                {
                    SongId = 1,
                    Likes = 178000,
                    Dislikes = 98000,
                    MinutesListened = 880000,
                    NumberOfPlays = 340000,
                    Month = 8,
                    Year = 2015
                },
                new SongRecommendationDetails
                {
                    SongId = 10,
                    Likes = 218000,
                    Dislikes = 108000,
                    MinutesListened = 1090000,
                    NumberOfPlays = 670000,
                    Month = 8,
                    Year = 2015
                },
                new SongRecommendationDetails
                {
                    SongId = 5,
                    Likes = 88000,
                    Dislikes = 48000,
                    MinutesListened = 720000,
                    NumberOfPlays = 240000,
                    Month = 8,
                    Year = 2015
                },
                new SongRecommendationDetails
                {
                    SongId = 2,
                    Likes = 2550000,
                    Dislikes = 1060000,
                    MinutesListened = 4400000,
                    NumberOfPlays = 5320000,
                    Month = 8,
                    Year = 2015
                },
                new SongRecommendationDetails
                {
                    SongId = 5,
                    Likes = 212000,
                    Dislikes = 66000,
                    MinutesListened = 320000,
                    NumberOfPlays = 365000,
                    Month = 8,
                    Year = 2012
                },
            };
        }

    }
}
