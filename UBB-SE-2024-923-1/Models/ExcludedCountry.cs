using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_923_1.Models
{
    [PrimaryKey(nameof(CountryId), nameof(SongId))]
    public class ExcludedCountry
    {
        public int CountryId { get; set; }
        public int SongId { get; set; }
    }
}