using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public class PlaylistSongItemRepository : IPlaylistSongItemRepository
    {
        private readonly DataContext context;

        public PlaylistSongItemRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddSongToPlaylist(int songId, int playlistId)
        {
            PlaylistSongItem playlistSongItem = new ()
            {
                SongId = songId,
                PlaylistId = playlistId
            };

            await context.PlaylistSongItems.AddAsync(playlistSongItem);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSongFromPlaylist(int songId, int playlistId)
        {
            var playlistSongItemToDelete = await context.PlaylistSongItems
                .FirstOrDefaultAsync(playlistSongItem => playlistSongItem.SongId == songId && playlistSongItem.PlaylistId == playlistId);

            if (playlistSongItemToDelete == null)
            {
                return false;
            }

            context.PlaylistSongItems.Remove(playlistSongItemToDelete);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsBySongId(int songId)
        {
            return await context.PlaylistSongItems
                .Where(playlistSongItem => playlistSongItem.SongId == songId)
                .Select(playlistSongItem => playlistSongItem.Playlist)
                .ToListAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsByPlaylistId(int playlistId)
        {
            return await context.PlaylistSongItems
                .Where(playlistSongItem => playlistSongItem.PlaylistId == playlistId)
                .Select(playlistSongItem => playlistSongItem.Song)
                .ToListAsync();
        }
    }
}
