using UBB_SE_2024_923_1.Enums;

namespace UBB_SE_2024_923_1.DTO
{
    public class EndOfYearRecapViewModel
    {
        public List<SongBasicInformation> Top5MostListenedSongs { get; set; }
        public Tuple<SongBasicInformation, decimal> MostPlayedSongPercentile { get; set; }
        public Tuple<string, decimal> MostPlayedArtistPercentile { get; set; }
        public int MinutesListened { get; set; }
        public List<string> Top5Genres { get; set; }
        public List<string> NewGenresDiscovered { get; set; }
        public ListenerPersonality ListenerPersonality { get; set; }
    }
}
