using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_923_1.Models
{
    /// <summary>
    /// Represents the data distribution of ads across different songs,
    /// genres, languages, and time periods.
    /// </summary>

    [PrimaryKey(nameof(SongId), nameof(AdCampaign))]
    public class AdDistributionData
    {
        public int SongId { get; set; }
        public int AdCampaign { get; set; }


        public string Genre { get; set; }
        public string Language { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
