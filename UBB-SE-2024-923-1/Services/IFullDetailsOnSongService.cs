using UBB_SE_2024_923_1.DTO;
using UBB_SE_2024_923_1.Enums;

namespace UBB_SE_2024_923_1.Services
{
    public interface IFullDetailsOnSongService
    {
        public Task<FullDetailsOnSong> GetFullDetailsOnSong(int songId);
        public Task<FullDetailsOnSong> GetCurrentMonthDetails(int songId);
    }
}
