using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1_TEST.Services.Stubs
{
    internal class TestUserPlaybackBehaviourRepository : IUserPlaybackBehaviourRepository
    {
        public Task<UserPlaybackBehaviour> GetUserPlaybackBehaviour(int userId, int? songId = null, DateTime? timestamp = null)
        {
            return Task.FromResult(new UserPlaybackBehaviour()
            {
                UserId = 1,
                SongId = 1,
                Timestamp = DateTime.Now
            });
        }

        public Task<List<UserPlaybackBehaviour>> GetListOfUserPlaybackBehaviourEntities(int userId)
        {
            // Example implementation for testing purposes
            if (userId == 3)
            {
                var userPlaybackBehaviourList = new List<UserPlaybackBehaviour>();
                for (int i = 0; i < 102; i++)
                {
                    userPlaybackBehaviourList.Add(new UserPlaybackBehaviour()
                    {
                        UserId = 3,
                        SongId = i,
                        EventType = PlaybackEventType.StartSongPlayback,
                        Timestamp = DateTime.Now
                    });
                }
                return Task.FromResult(userPlaybackBehaviourList);
            }
            else if (userId == 4)
            {
                var userPlaybackBehaviours = new List<UserPlaybackBehaviour>();
                for (int i = 0; i < 12; i++)
                {
                    userPlaybackBehaviours.Add(new UserPlaybackBehaviour()
                    {
                        UserId = 4,
                        SongId = i,
                        EventType = PlaybackEventType.StartSongPlayback,
                        Timestamp = DateTime.Now
                    });
                }
                return Task.FromResult(userPlaybackBehaviours);
            }
            else if (userId == 5)
            {
                return Task.FromResult(new List<UserPlaybackBehaviour>()
                {
                    new UserPlaybackBehaviour
                    {
                        UserId = 1, SongId = 1, EventType = PlaybackEventType.StartSongPlayback,
                        Timestamp = DateTime.Now
                    },
                    new UserPlaybackBehaviour
                    {
                        UserId = 1, SongId = 2, EventType = PlaybackEventType.EndSongPlayback,
                        Timestamp = DateTime.Now.AddMinutes(5)
                    }
                });
            }
            else
            {
                return Task.FromResult(new List<UserPlaybackBehaviour>()
                {
                    new UserPlaybackBehaviour() { UserId = 1, SongId = 1, Timestamp = DateTime.Now },
                    new UserPlaybackBehaviour() { UserId = 1, SongId = 2, Timestamp = DateTime.Now },
                    new UserPlaybackBehaviour() { UserId = 1, SongId = 3, Timestamp = DateTime.Now }
                });
            }
        }
    }
}
