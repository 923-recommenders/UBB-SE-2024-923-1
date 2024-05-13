using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_923_1.Controllers;
using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Services;
using UBB_SE_2024_923_1_TEST.Services.Stubs;

namespace UBB_SE_2024_923_1_TEST.Services
{
    public class RecapServiceTest
    {
        [Fact]
        public async Task GetTheTop5MostListenedSongs_WhenUserHasSongs_ReturnsListOfSongs()
        {
            var songBasicDetailsRepository = new TestSongBasicDetailsRepository();
            var userPlaybackBehaviourRepository = new TestUserPlaybackBehaviourRepository();
            var recapService = new RecapService(songBasicDetailsRepository, userPlaybackBehaviourRepository);
            int userId = 1;

            var top5Songs = await recapService.GetTheTop5MostListenedSongs(userId);

            Assert.NotNull(top5Songs);
            Assert.Equal(5, top5Songs.Count);
        }

        [Fact]
        public async Task GetTheMostPlayedSongPercentile_ReturnsTuple()
        {
            var songBasicDetailsRepository = new TestSongBasicDetailsRepository();
            var userPlaybackBehaviourRepository = new TestUserPlaybackBehaviourRepository();
            var recapService = new RecapService(songBasicDetailsRepository, userPlaybackBehaviourRepository);
            int userId = 1;

            var mostPlayedSongPercentile = await recapService.GetTheMostPlayedSongPercentile(userId);

            Assert.NotNull(mostPlayedSongPercentile);
            Assert.IsType<Tuple<SongBasicInformation, decimal>>(mostPlayedSongPercentile);
            Assert.Equal("Test", mostPlayedSongPercentile.Item1.Name);
        }

        [Fact]
        public async Task GetTheMostPlayedArtistPercentile_ReturnsTuple()
        {
            var songBasicDetailsRepository = new TestSongBasicDetailsRepository();
            var userPlaybackBehaviourRepository = new TestUserPlaybackBehaviourRepository();
            var recapService = new RecapService(songBasicDetailsRepository, userPlaybackBehaviourRepository);
            int userId = 1;

            var mostPlayedArtistPercentile = await recapService.GetTheMostPlayedArtistPercentile(userId);

            Assert.NotNull(mostPlayedArtistPercentile);
            Assert.IsType<Tuple<string, decimal>>(mostPlayedArtistPercentile);
            Assert.Equal("Test", mostPlayedArtistPercentile.Item1);
        }

        [Fact]
        public async Task GetTotalMinutesListened_ReturnsInt()
        {
            var songBasicDetailsRepository = new TestSongBasicDetailsRepository();
            var userPlaybackBehaviourRepository = new TestUserPlaybackBehaviourRepository();
            var recapService = new RecapService(songBasicDetailsRepository, userPlaybackBehaviourRepository);
            int userId = 1;
            int userId2 = 5;

            var totalMinutesListened = await recapService.GetTotalMinutesListened(userId);
            var totalMinutesListened2 = await recapService.GetTotalMinutesListened(userId2);

            Assert.NotNull(totalMinutesListened);
            Assert.IsType<int>(totalMinutesListened);

            Assert.NotNull(totalMinutesListened2);
            Assert.Equal(5, totalMinutesListened2);
        }

        [Fact]
        public async Task GetTheTop5Genres_WhenUserHasGenres_ReturnsListOfGenres()
        {
            var songBasicDetailsRepository = new TestSongBasicDetailsRepository();
            var userPlaybackBehaviourRepository = new TestUserPlaybackBehaviourRepository();
            var recapService = new RecapService(songBasicDetailsRepository, userPlaybackBehaviourRepository);
            int userId = 1;

            var top5Genres = await recapService.GetTheTop5Genres(userId);

            Assert.NotNull(top5Genres);
            Assert.Equal(5, top5Genres.Count);
            Assert.Equal("Test1", top5Genres[0]);
            Assert.Equal("Test2", top5Genres[1]);
        }

        [Fact]
        public async Task GetNewGenresDiscovered_WhenUserHasNewGenres_ReturnsListOfGenres()
        {
            var songBasicDetailsRepository = new TestSongBasicDetailsRepository();
            var userPlaybackBehaviourRepository = new TestUserPlaybackBehaviourRepository();
            var recapService = new RecapService(songBasicDetailsRepository, userPlaybackBehaviourRepository);
            int userId = 1;

            var newGenres = await recapService.GetNewGenresDiscovered(userId);

            Assert.NotNull(newGenres);
            Assert.Equal(5, newGenres.Count);
            Assert.Equal("Test1", newGenres[0]);
        }

        [Fact]
        public async Task GetListenerPersonality_WhenUserHasPlaybackBehaviour_ReturnsListenerPersonality()
        {
            var songBasicDetailsRepository = new TestSongBasicDetailsRepository();
            var userPlaybackBehaviourRepository = new TestUserPlaybackBehaviourRepository();
            var recapService = new RecapService(songBasicDetailsRepository, userPlaybackBehaviourRepository);
            int userId1 = 1;
            int userId2 = 2;
            int userId3 = 3;
            int userId4 = 4;

            var listenerPersonality1 = await recapService.GetListenerPersonality(userId1);
            var listenerPersonality2 = await recapService.GetListenerPersonality(userId2);
            var listenerPersonality3 = await recapService.GetListenerPersonality(userId3);
            var listenerPersonality4 = await recapService.GetListenerPersonality(userId4);

            Assert.NotNull(listenerPersonality1);
            Assert.IsType<ListenerPersonality>(listenerPersonality1);
            Assert.Equal(ListenerPersonality.Explorer, listenerPersonality1);

            Assert.NotNull(listenerPersonality2);
            Assert.IsType<ListenerPersonality>(listenerPersonality2);
            Assert.Equal(ListenerPersonality.Casual, listenerPersonality2);

            Assert.NotNull(listenerPersonality3);
            Assert.IsType<ListenerPersonality>(listenerPersonality3);
            Assert.Equal(ListenerPersonality.Melophile, listenerPersonality3);

            Assert.NotNull(listenerPersonality4);
            Assert.IsType<ListenerPersonality>(listenerPersonality4);
            Assert.Equal(ListenerPersonality.Vanilla, listenerPersonality4);
        }

        [Fact]
        public async Task GenerateEndOfTheYearRecap_WhenUserHasPlaybackBehaviour_ReturnsRecap()
        {
            var songBasicDetailsRepository = new TestSongBasicDetailsRepository();
            var userPlaybackBehaviourRepository = new TestUserPlaybackBehaviourRepository();
            var recapService = new RecapService(songBasicDetailsRepository, userPlaybackBehaviourRepository);
            int userId = 1;

            var recap = await recapService.GenerateEndOfYearRecap(userId);

            Assert.NotNull(recap);
            Assert.Equal(5, recap.Top5Genres.Count);
            Assert.Equal("Test", recap.MostPlayedSongPercentile.Item1.Name);
            Assert.Equal("Test1", recap.NewGenresDiscovered[0]);
            Assert.Equal(ListenerPersonality.Explorer, recap.ListenerPersonality);
        }
    }
}
