namespace UBB_SE_2024_923_1.DTO
{
    public class FullDetailsOnSong
    {
        public int TotalMinutesListened { get; set; }
        public int TotalPlays { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public int TotalSkips { get; set; }
        public FullDetailsOnSong()
        {
            TotalMinutesListened = 0;
            TotalPlays = 0;
            TotalLikes = 0;
            TotalDislikes = 0;
            TotalSkips = 0;
        }
    }
}
