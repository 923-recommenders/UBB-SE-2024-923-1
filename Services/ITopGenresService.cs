using UBB_SE_2024_923_1.DTO;

namespace UBB_SE_2024_923_1.Services
{
    public interface ITopGenresService
    {
        Task<List<GenreData>> GetTop3Genres(int month, int year);
        Task<List<GenreData>> GetTop3SubGenres(int month, int year);
    }
}
