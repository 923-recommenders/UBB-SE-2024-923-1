using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents trend data for songs, including genre, language, country,
    /// and the unique identifier for the song. It is stored in the database.
    /// </summary>
    [PrimaryKey(nameof(SongId), nameof(Genre), nameof(Language), nameof(Country))]
    public class Trends
    {
        public int SongId { get; set; }

        public string Genre { get; set; }

        public string Language { get; set; }

        public string Country { get; set; }
    }
}
