using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents information about song recommendations stored in the database,
    /// including likes, dislikes, minutes listened, number of plays,
    /// and the time period.
    /// </summary>
    [PrimaryKey(nameof(SongId), nameof(Month), nameof(Year))]
    public class SongRecommendationDetails
    {
        public int SongId { get; set; } = 0;
        public int Month { get; set; } = 0;
        public int Year { get; set; } = 0;
        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;
        public int MinutesListened { get; set; } = 0;
        public int NumberOfPlays { get; set; } = 0;
    }
}
