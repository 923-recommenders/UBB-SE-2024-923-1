using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public interface ICountryRepository
    {
        Task Add(Country newCountry);
        Task<Country> GetById(int id);

        Task<Country> GetByName(string name);

        Task<List<Country>> GetAll();
    }
}
