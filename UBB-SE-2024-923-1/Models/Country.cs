using Microsoft.EntityFrameworkCore;

namespace UBB_SE_2024_923_1.Models
{
    [PrimaryKey(nameof(CountryId))]
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
    }
}