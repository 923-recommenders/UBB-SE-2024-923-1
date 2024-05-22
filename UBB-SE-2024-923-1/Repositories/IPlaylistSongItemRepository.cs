using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public interface IPlaylistSongItemRepository
    {
        Task AddSongToPlaylist(int songId, int playlistId);
        Task<bool> DeleteSongFromPlaylist(int songId, int playlistId);
        Task<IEnumerable<Song>> GetSongsByPlaylistId(int playlistId);
        Task<IEnumerable<Playlist>> GetPlaylistsBySongId(int songId);
    }
}
