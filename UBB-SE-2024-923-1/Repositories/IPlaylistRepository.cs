using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public interface IPlaylistRepository
    {
        Task<Playlist?> GetPlaylistById(int playlistId);
        Task<IEnumerable<Playlist>> GetAllPlaylists();
        Task<IEnumerable<Playlist>> GetAllPlaylistsOfUser(int userId);
        Task<int> AddPlaylist(Playlist playlist);
        Task<bool> DeletePlaylist(int playlistId);
        Task<bool> UpdatePlaylist(int playlistId, Playlist playlist);
    }
}
