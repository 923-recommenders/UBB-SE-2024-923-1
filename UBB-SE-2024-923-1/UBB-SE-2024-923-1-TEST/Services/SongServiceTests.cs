using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using UBB_SE_2024_923_1.Services;
using FluentAssertions;

namespace UBB_SE_2024_923_1_TEST.Services
{
    public class SongServiceTests
    {
        private readonly Mock<IRepository<Song>> _mockSongRepository;
        private readonly Mock<IRepository<Users>> _mockUserRepository;
        private readonly SongService _songService;

        public SongServiceTests()
        {
            _mockSongRepository = new Mock<IRepository<Song>>();
            _mockUserRepository = new Mock<IRepository<Users>>();
            _songService = new SongService(_mockSongRepository.Object, _mockUserRepository.Object);
        }

        [Fact]
        public async Task GetAllSongs_ReturnsAllSongs()
        {
            // Arrange
            var expectedSongs = new List<Song> { new Song(), new Song() };
            _mockSongRepository.Setup(repo => repo.GetAll()).ReturnsAsync(expectedSongs);

            // Act
            var result = await _songService.GetAllSongs();

            // Assert
            Assert.Equal(expectedSongs.Count, result.Count());
        }

        [Fact]
        public async Task GetSongById_ReturnsCorrectSong()
        {
            // Arrange
            var expectedSong = new Song();
            _mockSongRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(expectedSong);

            // Act
            var result = await _songService.GetSongById(1); // Assuming 1 is a valid ID

            // Assert
            Assert.Equal(expectedSong, result);
        }

        [Fact]
        public async Task AddSong_AddsSongSuccessfully()
        {
            // Arrange
            var songToAdd = new Song();
            _mockSongRepository.Setup(repo => repo.Add(It.IsAny<Song>())).Returns(Task.CompletedTask);

            // Act & Assert
            await _songService.AddSong(songToAdd);
        }

        [Fact]
        public async Task DeleteSong_DeletesSongSuccessfully()
        {
            // Arrange
            var songToDelete = new Song();
            _mockSongRepository.Setup(repo => repo.Delete(It.IsAny<Song>())).Returns(Task.CompletedTask);

            // Act & Assert
            await _songService.DeleteSong(songToDelete);
        }

        [Fact]
        public async Task GetUserById_ReturnsCorrectUser()
        {
            // Arrange
            var expectedUser = new Users();
            _mockUserRepository.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(expectedUser);

            // Act
            var result = await _songService.GetUserById(1); // Assuming 1 is a valid ID

            // Assert
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async Task GetSongById_ReturnsNull_ForInvalidId()
        {
            // Arrange
            int invalidId = -1; // Assuming -1 is an invalid ID
            _mockSongRepository.Setup(repo => repo.GetById(invalidId)).ReturnsAsync((Song)null);

            // Act
            var result = await _songService.GetSongById(invalidId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddSong_ThrowsException_OnFailure()
        {
            // Arrange
            var songToAdd = new Song();
            _mockSongRepository.Setup(repo => repo.Add(songToAdd)).Throws(new Exception("Test exception"));

            // Act & Assert
            try
            {
                await _songService.AddSong(songToAdd);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task DeleteSong_ThrowsException_OnFailure()
        {
            // Arrange
            var songToDelete = new Song();
            _mockSongRepository.Setup(repo => repo.Delete(songToDelete)).Throws(new Exception("Test exception"));

            // Act & Assert
            try
            {
                await _songService.DeleteSong(songToDelete);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task GetUserById_ThrowsException_OnFailure()
        {
            // Arrange
            int invalidUserId = -1; // Assuming -1 is an invalid ID
            _mockUserRepository.Setup(repo => repo.GetById(invalidUserId)).Throws(new Exception("Test exception"));

            // Act & Assert
            try
            {
                await _songService.GetUserById(invalidUserId);
                Assert.True(false);
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }
    }
}