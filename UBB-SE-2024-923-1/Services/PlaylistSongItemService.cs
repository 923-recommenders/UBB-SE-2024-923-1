using System.ComponentModel.DataAnnotations;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Services
{
    public class PlaylistSongItemService : IPlaylistSongItemService
    {
        private readonly IPlaylistSongItemRepository playlistSongItemRepository;
        private readonly IPlaylistService playlistService;
        private readonly ISongService songService;

        public PlaylistSongItemService(
            IPlaylistSongItemRepository playlistSongItemRepository,
            IPlaylistService playlistService,
            ISongService songService)
        {
            this.playlistSongItemRepository = playlistSongItemRepository
                ?? throw new ArgumentNullException(nameof(playlistSongItemRepository));

            this.playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
            this.songService = songService ?? throw new ArgumentNullException(nameof(songService));
        }

        private async Task ValidateSongId(int songId)
        {
            if (songId < 0)
            {
                throw new ValidationException("Invalid song id provided.");
            }

            if (await songService.GetSongById(songId) == null)
            {
                throw new ValidationException("The given song does not exist.");
            }
        }

        private async Task ValidatePlaylistId(int playlistId)
        {
            if (playlistId < 0)
            {
                throw new ValidationException("Invalid playlist id provided.");
            }

            if (await playlistService.GetPlaylistById(playlistId) == null)
            {
                throw new ArgumentException("The given playlist does not exist!");
            }
        }

        public async Task AddSongToPlaylist(int playlistId, int songId)
        {
            await ValidateSongId(songId);
            await ValidatePlaylistId(playlistId);

            var songsInPlaylist = await playlistSongItemRepository.GetSongsByPlaylistId(playlistId);
            foreach (var song in songsInPlaylist)
            {
                if (song.SongId == songId)
                {
                    return;
                }
            }

            await playlistSongItemRepository.AddSongToPlaylist(songId, playlistId);
        }

        public async Task<bool> DeleteSongFromPlaylist(int songId, int playlistId)
        {
            await ValidateSongId(songId);
            await ValidatePlaylistId(playlistId);

            return await playlistSongItemRepository.DeleteSongFromPlaylist(songId, playlistId);
        }

        public async Task<IEnumerable<Playlist>> GetPlaylistsBySongId(int songId)
        {
            await ValidateSongId(songId);

            return await playlistSongItemRepository.GetPlaylistsBySongId(songId);
        }

        public async Task<IEnumerable<Song>> GetSongsByPlaylistId(int playlistId)
        {
            await ValidatePlaylistId(playlistId);

            return await playlistSongItemRepository.GetSongsByPlaylistId(playlistId);
        }
    }
}
