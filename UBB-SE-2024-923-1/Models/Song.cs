using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_923_1.Models
{
    [PrimaryKey(nameof(SongId))]
    public class Song
    {
        public int SongId { get; set; }
        public string ArtistName { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Subgenre { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public bool IsExplicit { get; set; }
        public string YoutubeLink { get; set; }
    }
}