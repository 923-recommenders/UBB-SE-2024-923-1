using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Enums;

namespace UBB_SE_2024_923_1.Services
{
    public interface IRecapService
    {
        public Task<List<SongBasicInformation>> GetTheTop5MostListenedSongs(int userId);
        public Task<Tuple<SongBasicInformation, decimal>> GetTheMostPlayedSongPercentile(int userId);
        public Task<Tuple<string, decimal>> GetTheMostPlayedArtistPercentile(int userId);
        public Task<int> GetTotalMinutesListened(int userId);
        public Task<List<string>> GetTheTop5Genres(int userId);
        public Task<List<string>> GetNewGenresDiscovered(int userId);
        public Task<ListenerPersonality> GetListenerPersonality(int userId);
        public Task<EndOfYearRecapViewModel> GenerateEndOfYearRecap(int userId);
    }
}