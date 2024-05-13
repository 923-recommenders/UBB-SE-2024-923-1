using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using Xunit;

namespace UBB_SE_2024_923_1_TEST.Repositories
{
    public class UserPlaybackBehaviourRepositoryTest
    {
        [Fact]
        public async Task GetUserPlaybackBehaviour_FindsBehaviour_ReturnsCorrectBehaviour()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var timestamp = DateTime.UtcNow;
            var seededData = new UserPlaybackBehaviour
            {
                UserId = 1,
                SongId = 100,
                Timestamp = timestamp,
                EventType = PlaybackEventType.Like
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                context.UserPlaybackBehaviour.Add(seededData);
                await context.SaveChangesAsync();
            }

            using (var context = new DataContext(options))
            {
                var repository = new UserPlaybackBehaviourRepository(context);

                //act
                var result = await repository.GetUserPlaybackBehaviour(1, 100, timestamp);

                //assert
                Assert.NotNull(result);
                Assert.Equal(seededData.UserId, result.UserId);
                Assert.Equal(seededData.SongId, result.SongId);
                Assert.Equal(seededData.EventType, result.EventType);
                Assert.True(Math.Abs((result.Timestamp - timestamp).TotalSeconds) < 5);
            }
        }

        [Fact]
        public async Task GetListOfUserPlaybackBehaviourEntities_ReturnsCorrectList()
        {
            //arrange
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var userId = 1;
            var seededData = new List<UserPlaybackBehaviour>
            {
                new UserPlaybackBehaviour { UserId = userId, SongId = 100, Timestamp = DateTime.UtcNow, EventType = PlaybackEventType.Like },
                new UserPlaybackBehaviour { UserId = userId, SongId = 101, Timestamp = DateTime.UtcNow, EventType = PlaybackEventType.Like },
                new UserPlaybackBehaviour { UserId = userId, SongId = 102, Timestamp = DateTime.UtcNow, EventType = PlaybackEventType.Like }
            };

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
                await context.UserPlaybackBehaviour.AddRangeAsync(seededData);
                await context.SaveChangesAsync();
            }
            using (var context = new DataContext(options))
            {
                var repository = new UserPlaybackBehaviourRepository(context);

                // act
                var result = await repository.GetListOfUserPlaybackBehaviourEntities(userId);

                // assert
                Assert.NotNull(result);
                Assert.Equal(seededData.Count, result.Count);
                foreach (var expectedEntity in seededData)
                {
                    var actualEntity = result.FirstOrDefault(e => e.SongId == expectedEntity.SongId);
                    Assert.NotNull(actualEntity);
                    Assert.Equal(expectedEntity.UserId, actualEntity.UserId);
                    Assert.Equal(expectedEntity.SongId, actualEntity.SongId);
                    Assert.Equal(expectedEntity.EventType, actualEntity.EventType);
                    Assert.True(Math.Abs((actualEntity.Timestamp - expectedEntity.Timestamp).TotalSeconds) < 5);
                }
            }
        }
    }
}
