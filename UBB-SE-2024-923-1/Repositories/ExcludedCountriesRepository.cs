using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public class ExcludedCountryRepsitory : Repository<Country>
    {
        public ExcludedCountryRepsitory(DataContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Country>> GetAllExcludedCounties(Song song)
        {
            var excludedCountryIds = await _context.ExcludedCountries
                .Where(ec => ec.SongId == song.SongId)
                .Select(ec => ec.CountryId)
                .ToListAsync();

            var excludedCountries = await _context.Countries
                .Where(c => excludedCountryIds.Contains(c.CountryId))
                .ToListAsync();

            return excludedCountries;
        }
    }
}