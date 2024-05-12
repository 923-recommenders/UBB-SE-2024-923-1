using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents the features of a song stored in the database, including its
    /// unique identifier and the artist's unique identifier.
    /// </summary>
    [PrimaryKey(nameof(SongId), nameof(ArtistId))]
    public class SongFeatures
    {
        public int SongId { get; set; }
        public int ArtistId { get; set; }
    }
}
