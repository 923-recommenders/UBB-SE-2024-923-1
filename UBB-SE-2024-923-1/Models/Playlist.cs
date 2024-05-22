using System.ComponentModel.DataAnnotations.Schema;

namespace UBB_SE_2024_923_1.Models
{
    public class Playlist
    {
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsPrivate { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
