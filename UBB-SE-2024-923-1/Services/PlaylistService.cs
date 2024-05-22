using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository playlistRepository;
        private readonly IMapper mapper;

        public PlaylistService(IPlaylistRepository playlistRepository, IMapper mapper)
        {
            this.playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private static bool ValidPlaylistModel(PlaylistForAddUpdateModel playlistModel)
        {
            if (playlistModel.Name.IsNullOrEmpty() || playlistModel.ImagePath.IsNullOrEmpty())
            {
                return false;
            }

            return true;
        }

        public async Task<Playlist> AddPlaylist(PlaylistForAddUpdateModel playlistModel)
        {
            if (!ValidPlaylistModel(playlistModel))
            {
                throw new ValidationException("Invalid playlist data.");
            }

            var playlist = mapper.Map<Playlist>(playlistModel);

            int id = await playlistRepository.AddPlaylist(playlist);
            playlist.Id = id;

            return playlist;
        }

        public async Task<bool> DeletePlaylist(int playlistId)
        {
            if (playlistId < 0)
            {
                throw new ValidationException("Invalid playlist id.");
            }

            return await playlistRepository.DeletePlaylist(playlistId);
        }

        public async Task<bool> UpdatePlaylist(int playlistId, PlaylistForAddUpdateModel playlistModel)
        {
            if (playlistId < 0)
            {
                throw new ValidationException("Invalid playlist id.");
            }

            if (!ValidPlaylistModel(playlistModel))
            {
                throw new ValidationException("Invalid playlist data.");
            }

            return await playlistRepository.UpdatePlaylist(playlistId, mapper.Map<Playlist>(playlistModel));
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylists()
        {
            return await playlistRepository.GetAllPlaylists();
        }

        public async Task<IEnumerable<Playlist>> GetAllPlaylistsOfUser(int userId)
        {
            if (userId < 0)
            {
                throw new ValidationException("Invalid user id.");
            }

            return await playlistRepository.GetAllPlaylistsOfUser(userId);
        }

        public async Task<Playlist?> GetPlaylistById(int playlistId)
        {
            if (playlistId < 0)
            {
                throw new ValidationException("Invalid playlist id.");
            }

            return await playlistRepository.GetPlaylistById(playlistId);
        }
    }
}
