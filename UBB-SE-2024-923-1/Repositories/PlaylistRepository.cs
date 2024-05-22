using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly DataContext context;

        public PlaylistRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> AddPlaylist(Playlist playlist)
        {
            await context.Playlists.AddAsync(playlist);
            await context.SaveChangesAsync();

            return playlist.Id;
        }

        public async Task<bool> DeletePlaylist(int playlistId)
        {
            var playlistToDelete = await context.Playlists.FirstOrDefaultAsync(playlist => playlist.Id == playlistId);
            if (playlistToDelete == null)
            {
                return false;
            }

            context.Playlists.Remove(playlistToDelete);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdatePlaylist(int playlistId, Playlist playlist)
        {
            var playlistToUpdate = await context.Playlists.FirstOrDefaultAsync(playlist => playlist.Id == playlistId);
            if (playlistToUpdate == null)
            {
                return false;
            }

            playlist.Id = playlistId;

            context.Playlists.Entry(playlistToUpdate).CurrentValues.SetValues(playlist);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylists()
        {
            return await context.Playlists.ToListAsync();
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylistsOfUser(int userId)
        {
            return await context.Playlists.Where(playlist => playlist.UserId == userId).ToListAsync();
        }

        public async Task<Playlist?> GetPlaylistById(int playlistId)
        {
            return await context.Playlists.FirstOrDefaultAsync(playlist => playlist.Id == playlistId);
        }
    }
}
