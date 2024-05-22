using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Services
{
    public interface IPlaylistService
    {
        Task<Playlist?> GetPlaylistById(int playlistId);
        Task<IEnumerable<Playlist>> GetAllPlaylists();
        Task<IEnumerable<Playlist>> GetAllPlaylistsOfUser(int userId);
        Task<Playlist> AddPlaylist(PlaylistForAddUpdateModel playlistModel);
        Task<bool> DeletePlaylist(int playlistId);
        Task<bool> UpdatePlaylist(int playlistId, PlaylistForAddUpdateModel playlistModel);
    }
}
