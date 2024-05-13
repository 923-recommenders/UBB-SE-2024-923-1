using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1_TEST.Repositories
{
    public class SongBasicDetailsRepositoryTests
    {
        /*private Mock<DataContext> _mockDbContext;
        private SongBasicDetailsRepository _repository;

        public SongBasicDetailsRepositoryTests()
        {
            _mockDbContext = new Mock<DataContext>();
            _repository = new SongBasicDetailsRepository(_mockDbContext.Object);
        }

        [Fact]
        public async Task TransformSongBasicDetailsToSongBasicInfo_ShouldReturnSongBasicInfo()
        {
            var songBasicDetails = new SongDataBaseModel
            {
                ArtistId = 1,
                SongId = 1,
                Name = "Test Song",
                Genre = "TestGenre",
                Subgenre = "TestSubgenre",
                Language = "TestLanguage",
                Country = "TestCountry",
                Album = "TestAlbum",
                Image = "TestImage.png"
            };

            var mockArtistDetails = new Mock<DbSet<ArtistDetails>>();
            mockArtistDetails.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync(new ArtistDetails { ArtistId = 1, Name = "Test Artist" });
            _mockDbContext.Setup(m => m.ArtistDetails).Returns(mockArtistDetails.Object);

            var result = await _repository.TransformSongBasicDetailsToSongBasicInfo(songBasicDetails);

            Assert.NotNull(result);
            Assert.Equal(1, result.SongId);
            Assert.Equal("Test Song", result.Name);
            Assert.Equal("TestGenre", result.Genre);
            Assert.Equal("TestSubgenre", result.Subgenre);
            Assert.Equal("Test Artist", result.Artist);
            Assert.Equal("TestLanguage", result.Language);
            Assert.Equal("TestCountry", result.Country);
            Assert.Equal("TestAlbum", result.Album);
            Assert.Equal("TestImage.png", result.Image);
        }

        [Fact]
        public async Task GetSongBasicDetails_WhenSongExists_ShouldReturnSongDetails()
        {
            var songId = 1;
            var expectedSong = new SongDataBaseModel
            {
                SongId = songId,
                Name = "Test Song",
                Genre = "TestGenre",
                Subgenre = "TestSubgenre",
                Language = "TestLanguage",
                Country = "TestCountry",
                Album = "TestAlbum",
                Image = "TestImage.png"
            };

            var mockSongDetails = new Mock<DbSet<SongDataBaseModel>>();
            mockSongDetails.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync(expectedSong);
            _mockDbContext.Setup(m => m.SongDataBaseModel).Returns(mockSongDetails.Object);

            var result = await _repository.GetSongBasicDetails(songId);

            Assert.NotNull(result);
            Assert.Equal(songId, result.SongId);
            Assert.Equal("Test Song", result.Name);
            Assert.Equal("TestGenre", result.Genre);
            Assert.Equal("TestSubgenre", result.Subgenre);
            Assert.Equal("TestLanguage", result.Language);
            Assert.Equal("TestCountry", result.Country);
            Assert.Equal("TestAlbum", result.Album);
            Assert.Equal("TestImage.png", result.Image);
        }

        [Fact]
        public async Task GetTop5MostListenedSongs_WhenSongsExist_ShouldReturnTop5Songs()
        {
            var userId = 1;
            var expectedSongs = new List<SongDataBaseModel>
            {
                new SongDataBaseModel { SongId = 1, Name = "Song 1", Genre = "Genre1", Subgenre = "Subgenre1", Language = "Language1", Country = "Country1", Album = "Album1", Image = "Image1.png" },
                new SongDataBaseModel { SongId = 2, Name = "Song 2", Genre = "Genre2", Subgenre = "Subgenre2", Language = "Language2", Country = "Country2", Album = "Album2", Image = "Image2.png" },
                new SongDataBaseModel { SongId = 3, Name = "Song 3", Genre = "Genre3", Subgenre = "Subgenre3", Language = "Language3", Country = "Country3", Album = "Album3", Image = "Image3.png" },
                new SongDataBaseModel { SongId = 4, Name = "Song 4", Genre = "Genre4", Subgenre = "Subgenre4", Language = "Language4", Country = "Country4", Album = "Album4", Image = "Image4.png" },
                new SongDataBaseModel { SongId = 5, Name = "Song 5", Genre = "Genre5", Subgenre = "Subgenre5", Language = "Language5", Country = "Country5", Album = "Album5", Image = "Image5.png" }
            };

            var mockPlaybackBehaviour = new Mock<DbSet<UserPlaybackBehaviour>>();
            mockPlaybackBehaviour.Setup(m => m.Where(It.IsAny<Expression<Func<UserPlaybackBehaviour, bool>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.GroupBy(It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>())).Returns((IQueryable<IGrouping<int, UserPlaybackBehaviour>>)mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.OrderByDescending(It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>())).Returns((IOrderedQueryable<UserPlaybackBehaviour>)mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.Select(It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>())).Returns((IQueryable<int>)mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.Take(5)).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.Select(It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>())).Returns((IQueryable<int>)expectedSongs.Select(s => s.SongId));
            _mockDbContext.Setup(m => m.UserPlaybackBehaviour).Returns(mockPlaybackBehaviour.Object);

            var mockSongDetails = new Mock<DbSet<SongDataBaseModel>>();
            mockSongDetails.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync((object[] ids) => Task.FromResult(expectedSongs.FirstOrDefault(s => s.SongId == (int)ids[0])));
            _mockDbContext.Setup(m => m.SongDataBaseModel).Returns(mockSongDetails.Object);

            var result = await _repository.GetTop5MostListenedSongs(userId);

            Assert.Equal(5, result.Count);
            Assert.Equal("Song 1", result[0].Name);
            Assert.Equal("Song 5", result[4].Name);
        }

        [Fact]
        public async Task GetMostPlayedSongPercentile_ShouldReturnCorrectPercentile()
        {
            var userId = 1;
            var mostPlayedSong = new SongDataBaseModel { SongId = 1, Name = "Most Played Song", Genre = "Genre1", Subgenre = "Subgenre1", Language = "Language1", Country = "Country1", Album = "Album1", Image = "Image1.png" };
            var totalSongs = 10;
            var mostListenedSongCount = 5;

            // Setup mock DbSet for UserPlaybackBehaviour
            var mockPlaybackBehaviour = new Mock<DbSet<UserPlaybackBehaviour>>();
            mockPlaybackBehaviour.Setup(m => m.Where(It.IsAny<Expression<Func<UserPlaybackBehaviour, bool>>>()))
               .Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.GroupBy(It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>()))
               .Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.OrderByDescending(It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>()))
               .Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.Select(It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>()))
               .Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.FirstOrDefaultAsync())
               .ReturnsAsync(1); // Assuming this is the correct setup for your test case
            _mockDbContext.Setup(m => m.UserPlaybackBehaviour).Returns(mockPlaybackBehaviour.Object);

            // Setup mock DbSet for SongDataBaseModel
            var mockSongDetails = new Mock<DbSet<SongDataBaseModel>>();
            mockSongDetails.Setup(m => m.FindAsync(It.IsAny<object[]>()))
               .ReturnsAsync((object[] ids) => Task.FromResult(mostPlayedSong)); // Adjust this to return the correct song based on the test case
            _mockDbContext.Setup(m => m.SongDataBaseModel).Returns(mockSongDetails.Object);

            // Assuming GetMostPlayedSongPercentile is a method in YourRepository that uses the DbContext
            var result = await _repository.GetMostPlayedSongPercentile(userId);

            Assert.NotNull(result);
            Assert.Equal(mostPlayedSong, result.Item1);
            Assert.Equal(1, result.Item2); // Adjust this assertion based on your expected result
        }

        [Fact]
        public async Task GetMostPlayedArtistPercentile_ShouldReturnCorrectPercentile()
        {
            var userId = 1;
            var mostPlayedArtistInfo = new MostPlayedArtistInformation { Artist_Id = 1, Start_Listen_Events = 5 };
            var mostPlayedArtist = "Most Played Artist";
            var totalSongs = 10;

            var mockPlaybackBehaviour = new Mock<DbSet<UserPlaybackBehaviour>>();
            mockPlaybackBehaviour.Setup(m => m.Where(ub => ub.UserId == userId)).Returns(mockPlaybackBehaviour.Object);

            // Assuming Join is a method that takes two parameters of type Expression<Func<UserPlaybackBehaviour, int>> and returns an IQueryable<MostPlayedArtistInformation>
            mockPlaybackBehaviour.Setup(m => m.Join(It.IsAny<IQueryable<UserPlaybackBehaviour>>(), It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>(), It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>(), It.IsAny<Expression<Func<UserPlaybackBehaviour, MostPlayedArtistInformation>>>()))
                .Returns(mockPlaybackBehaviour.Object); mockPlaybackBehaviour.Setup(m => m.GroupBy(It.IsAny<Expression<Func<MostPlayedArtistInformation, int>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.OrderByDescending(It.IsAny<Expression<Func<MostPlayedArtistInformation, int>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.FirstOrDefaultAsync()).ReturnsAsync(mostPlayedArtistInfo);
            _mockDbContext.Setup(m => m.UserPlaybackBehaviour).Returns(mockPlaybackBehaviour.Object);

            var mockArtistDetails = new Mock<DbSet<ArtistDetails>>();
            mockArtistDetails.Setup(m => m.FindAsync(It.IsAny<object[]>())).ReturnsAsync(new ArtistDetails { ArtistId = 1, Name = "Most Played Artist" });
            _mockDbContext.Setup(m => m.ArtistDetails).Returns(mockArtistDetails.Object);

            var result = await _repository.GetMostPlayedArtistPercentile(userId);

            Assert.NotNull(result);
            Assert.Equal(mostPlayedArtist, result.Item1);
            Assert.Equal(0.5m, result.Item2);
        }

        [Fact]
        public async Task GetTop5Genres_ShouldReturnCorrectGenres()
        {
            var userId = 1;
            var expectedGenres = new List<string> { "Genre1", "Genre2", "Genre3", "Genre4", "Genre5" };

            var mockPlaybackBehaviour = new Mock<DbSet<UserPlaybackBehaviour>>();
            mockPlaybackBehaviour.Setup(m => m.Where(It.IsAny<Expression<Func<UserPlaybackBehaviour, bool>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.Join(It.IsAny<IQueryable<UserPlaybackBehaviour>>(), It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>(), It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>(), It.IsAny<Expression<Func<UserPlaybackBehaviour, string>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.GroupBy(It.IsAny<Expression<Func<string, int>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.OrderByDescending(It.IsAny<Expression<Func<IGrouping<string, string>, int>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.Select(It.IsAny<Expression<Func<IGrouping<string, string>, string>>>())).Returns(expectedGenres.AsQueryable());
            _mockDbContext.Setup(m => m.UserPlaybackBehaviour).Returns(mockPlaybackBehaviour.Object);

            var result = await _repository.GetTop5Genres(userId);

            Assert.NotNull(result);
            Assert.Equal(expectedGenres, result);
        }

        [Fact]
        public async Task GetAllNewGenresDiscovered_ShouldReturnCorrectNewGenres()
        {
            var userId = 1;
            var expectedNewGenres = new List<string> { "NewGenre1", "NewGenre2" };

            var mockPlaybackBehaviour = new Mock<DbSet<UserPlaybackBehaviour>>();
            mockPlaybackBehaviour.Setup(m => m.Where(It.IsAny<Expression<Func<UserPlaybackBehaviour, bool>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.Select(It.IsAny<Expression<Func<UserPlaybackBehaviour, int>>>())).Returns(mockPlaybackBehaviour.Object);
            mockPlaybackBehaviour.Setup(m => m.Contains(It.IsAny<string>())).Returns(true);
            _mockDbContext.Setup(m => m.UserPlaybackBehaviour).Returns(mockPlaybackBehaviour.Object);

            var mockSongDetails = new Mock<DbSet<SongDataBaseModel>>();
            mockSongDetails.Setup(m => m.Where(It.IsAny<Expression<Func<SongDataBaseModel, bool>>>())).Returns(mockSongDetails.Object);
            mockSongDetails.Setup(m => m.Select(It.IsAny<Expression<Func<SongDataBaseModel, string>>>())).Returns(mockSongDetails.Object);
            mockSongDetails.Setup(m => m.Distinct()).Returns(mockSongDetails.Object);
            mockSongDetails.Setup(m => m.ToListAsync()).ReturnsAsync(expectedNewGenres);
            _mockDbContext.Setup(m => m.SongDataBaseModel).Returns(mockSongDetails.Object);

            var result = await _repository.GetAllNewGenresDiscovered(userId);

            Assert.NotNull(result);
            Assert.Equal(expectedNewGenres, result);
        }*/
    }
}
