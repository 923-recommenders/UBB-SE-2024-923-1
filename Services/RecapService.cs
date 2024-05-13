using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Enums;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Services
{
    public class RecapService(
        ISongBasicDetailsRepository songBasicDetailsRepository,
        IUserPlaybackBehaviourRepository userPlaybackBehaviourRepository)
    {
        private ISongBasicDetailsRepository songBasicDetailsRepository = songBasicDetailsRepository;
        private IUserPlaybackBehaviourRepository userPlaybackBehaviourRepository = userPlaybackBehaviourRepository;

        /// <summary>
        /// Retrieves the top 5 most listened songs for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of the top 5 most listened songs.</returns>
        public async Task<List<SongBasicInformation>> GetTheTop5MostListenedSongs(int userId)
        {
            var top5Songs = await songBasicDetailsRepository.GetTop5MostListenedSongs(userId);
            List<SongBasicInformation> top5SongsInformation = new List<SongBasicInformation>();
            foreach (var song in top5Songs)
            {
                top5SongsInformation.Add(await songBasicDetailsRepository.TransformSongBasicDetailsToSongBasicInfo(song));
            }
            return top5SongsInformation;
        }

        /// <summary>
        /// Retrieves the percentile of the most played song for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A tuple containing the most played song
        /// and its percentile.</returns>
        public async Task<Tuple<SongBasicInformation, decimal>> GetTheMostPlayedSongPercentile(int userId)
        {
            var mostPlayedSong = await songBasicDetailsRepository.GetMostPlayedSongPercentile(userId);
            return new Tuple<SongBasicInformation, decimal>(await songBasicDetailsRepository.TransformSongBasicDetailsToSongBasicInfo(mostPlayedSong.Item1), mostPlayedSong.Item2);
        }

        /// <summary>
        /// Retrieves the percentile of the most played artist for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A tuple containing the most played artist and its percentile.</returns>
        public async Task<Tuple<string, decimal>> GetTheMostPlayedArtistPercentile(int userId)
        {
            return await songBasicDetailsRepository.GetMostPlayedArtistPercentile(userId);
        }

        /// <summary>
        /// Calculates the total minutes listened by a user based on their playback behavior.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The total minutes listened by the user.</returns>
        public async Task<int> GetTotalMinutesListened(int userId)
        {
            var userEvents = await userPlaybackBehaviourRepository.GetListOfUserPlaybackBehaviourEntities(userId);
            int totalMinutesListened = 0;
            for (int firstCounter = 0; firstCounter < userEvents.Count; firstCounter++)
            {
                if (userEvents[firstCounter].EventType == PlaybackEventType.StartSongPlayback)
                {
                    for (int secondCounter = firstCounter + 1; secondCounter < userEvents.Count; secondCounter++)
                    {
                        if (userEvents[secondCounter].EventType == PlaybackEventType.EndSongPlayback)
                        {
                            totalMinutesListened += (int)(userEvents[secondCounter].Timestamp - userEvents[firstCounter].Timestamp).TotalMinutes;
                            firstCounter = secondCounter;
                            break;
                        }
                    }
                }
            }
            return totalMinutesListened;
        }

        /// <summary>
        /// Retrieves the top 5 genres for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of the top 5 genres.</returns>
        public async Task<List<string>> GetTheTop5Genres(int userId)
        {
            return await songBasicDetailsRepository.GetTop5Genres(userId);
        }

        /// <summary>
        /// Retrieves new genres discovered by a user in the current year.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of new genres discovered by the user.</returns>
        public async Task<List<string>> GetNewGenresDiscovered(int userId)
        {
            return await songBasicDetailsRepository.GetAllNewGenresDiscovered(userId);
        }

        /// <summary>
        /// Determines the listener personality based on user
        /// playback behavior and genre discovery.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The listener personality of the user.</returns>
        public async Task<ListenerPersonality> GetListenerPersonality(int userId)
        {
            var userEvents = await userPlaybackBehaviourRepository.GetListOfUserPlaybackBehaviourEntities(userId);
            int playCount = 0;
            for (int counter = 0; counter < userEvents.Count; counter++)
            {
                if ((userEvents[counter].EventType == PlaybackEventType.StartSongPlayback) && (userEvents[counter].Timestamp.Year == DateTime.Now.Year))
                {
                    playCount++;
                }
            }
            if (playCount > 100)
            {
                return Enums.ListenerPersonality.Melophile;
            }
            var newGenres = await GetNewGenresDiscovered(userId);
            if (newGenres.Count > 3)
            {
                return Enums.ListenerPersonality.Explorer;
            }
            if (playCount < 10)
            {
                return Enums.ListenerPersonality.Casual;
            }
            return Enums.ListenerPersonality.Vanilla;
        }

        /// <summary>
        /// Generates an end-of-year recap for a user, including top songs,
        /// artist percentiles, minutes listened, genres, and listener personality.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>An <see cref="EndOfYearRecapViewModel"/> containing
        /// the end-of-year recap for the user.</returns>
        public async Task<EndOfYearRecapViewModel> GenerateEndOfYearRecap(int userId)
        {
            var endOfYearRecap = new EndOfYearRecapViewModel();
            endOfYearRecap.Top5MostListenedSongs = await GetTheTop5MostListenedSongs(userId);
            endOfYearRecap.MostPlayedSongPercentile = await GetTheMostPlayedSongPercentile(userId);
            endOfYearRecap.MostPlayedArtistPercentile = await GetTheMostPlayedArtistPercentile(userId);
            endOfYearRecap.MinutesListened = await GetTotalMinutesListened(userId);
            endOfYearRecap.Top5Genres = await GetTheTop5Genres(userId);
            endOfYearRecap.NewGenresDiscovered = await GetNewGenresDiscovered(userId);
            endOfYearRecap.ListenerPersonality = await GetListenerPersonality(userId);
            return endOfYearRecap;
        }
    }
}