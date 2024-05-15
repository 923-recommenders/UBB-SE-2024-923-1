using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using UBB_SE_2024_923_1.Services;

namespace UBB_SE_2024_923_1_TEST.Services
{
    public class FullDetailsOnSongServiceTest
    {
        private Mock<IRepository<UserPlaybackBehaviour>> userPlaybackBehaviourMock;

        [Fact]
        public async Task GetFullDetailsOnSong_GetSongThatExists_returnsCorrectData()
        {
            userPlaybackBehaviourMock = new Mock<IRepository<UserPlaybackBehaviour>>();
            userPlaybackBehaviourMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<UserPlaybackBehaviour>
            {
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.StartSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.EndSongPlayback, Timestamp = DateTime.Now.AddDays(-1).AddMinutes(1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.Like, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.Dislike, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.Skip, Timestamp = DateTime.Now.AddDays(-1) }
            });

            var service = new FullDetailsOnSongService(userPlaybackBehaviourMock.Object);
            var result = service.GetFullDetailsOnSong(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Result.TotalPlays);
            Assert.Equal(1, result.Result.TotalLikes);
            Assert.Equal(1, result.Result.TotalDislikes);
            Assert.Equal(1, result.Result.TotalSkips);
            Assert.Equal(1, result.Result.TotalMinutesListened);
        }

        [Fact]
        public async Task GetFullDetailsOnSong_GetStartSongData_returnsCorrectStartingTimes()
        {
            userPlaybackBehaviourMock = new Mock<IRepository<UserPlaybackBehaviour>>();
            userPlaybackBehaviourMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<UserPlaybackBehaviour>
            {
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.StartSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.EndSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.StartSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.EndSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.StartSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.EndSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.StartSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.EndSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
            });

            var service = new FullDetailsOnSongService(userPlaybackBehaviourMock.Object);


            var result = service.GetFullDetailsOnSong(1);

            Assert.NotNull(result);
            Assert.Equal(4, result.Result.TotalPlays);
            Assert.Equal(0, result.Result.TotalLikes);
            Assert.Equal(0, result.Result.TotalDislikes);
            Assert.Equal(0, result.Result.TotalSkips);
            Assert.Equal(0, result.Result.TotalMinutesListened);
        }

        [Fact]
        public async Task GetFullDetailsOnSong_SongWhichNotExists_ReturnsNull()
        {
            userPlaybackBehaviourMock = new Mock<IRepository<UserPlaybackBehaviour>>();
            userPlaybackBehaviourMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<UserPlaybackBehaviour> { });

            var service = new FullDetailsOnSongService(userPlaybackBehaviourMock.Object);

            var result = service.GetFullDetailsOnSong(1);

            Assert.Null(result.Result);
        }

        [Fact]
        public void GetCurrentMonthDetails_GetDetailsOfCurrentMonthSong_AggregatesPlaybackBehaviorDataForCurrentMonth()
        {
            userPlaybackBehaviourMock = new Mock<IRepository<UserPlaybackBehaviour>>();
            userPlaybackBehaviourMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<UserPlaybackBehaviour>
            {
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.StartSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.EndSongPlayback, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.Like, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.Dislike, Timestamp = DateTime.Now.AddDays(-1) },
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.Skip, Timestamp = DateTime.Now.AddDays(-1) }
            });

            var service = new FullDetailsOnSongService(userPlaybackBehaviourMock.Object);
            var result = service.GetCurrentMonthDetails(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Result.TotalPlays);
            Assert.Equal(1, result.Result.TotalLikes);
            Assert.Equal(1, result.Result.TotalDislikes);
            Assert.Equal(1, result.Result.TotalSkips);
            Assert.Equal(0, result.Result.TotalMinutesListened);
        }

        [Fact]
        public async Task GetCurrentMonthDetails_GetDetailsForSongNotFromCurrentMonth_ReturnsDefaultSongDetails()
        {
            userPlaybackBehaviourMock = new Mock<IRepository<UserPlaybackBehaviour>>();
            userPlaybackBehaviourMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<UserPlaybackBehaviour>
            {
                new UserPlaybackBehaviour { SongId = 1, EventType = PlaybackEventType.StartSongPlayback, Timestamp = DateTime.Now.AddMonths(-1) },
            });

            var service = new FullDetailsOnSongService(userPlaybackBehaviourMock.Object);

            var result = service.GetCurrentMonthDetails(1);

            Assert.NotNull(result);
            Assert.Equal(0, result.Result.TotalPlays);
            Assert.Equal(0, result.Result.TotalLikes);
            Assert.Equal(0, result.Result.TotalDislikes);
            Assert.Equal(0, result.Result.TotalSkips);
            Assert.Equal(0, result.Result.TotalMinutesListened);
        }

        [Fact]
        public void GetCurrentMonthDetails_GetSongWhichNotExists_ReturnsDefaultSongDetails()
        {
            userPlaybackBehaviourMock = new Mock<IRepository<UserPlaybackBehaviour>>();
            userPlaybackBehaviourMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<UserPlaybackBehaviour> { });

            var service = new FullDetailsOnSongService(userPlaybackBehaviourMock.Object);

            var result = service.GetCurrentMonthDetails(1);

            Assert.NotNull(result);
            Assert.Equal(0, result.Result.TotalPlays);
            Assert.Equal(0, result.Result.TotalLikes);
            Assert.Equal(0, result.Result.TotalDislikes);
            Assert.Equal(0, result.Result.TotalSkips);
            Assert.Equal(0, result.Result.TotalMinutesListened);
        }

    }
}
