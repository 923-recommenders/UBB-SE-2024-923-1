using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Services
{
    public interface ISongService
    {
        Task<Song?> GetSongById(int songId);
        Task<IEnumerable<Song>> GetAllSongs();
        Task AddSong(Song song);
        Task DeleteSong(Song song);
    }
    public class SongService : ISongService
    {
        private readonly IRepository<Song> _repository;
        private readonly IRepository<Users> _userRepository;

        public SongService(IRepository<Song> repository, IRepository<Users> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        private static bool ValidSong(Song song)
        {
            if (song.Name.IsNullOrEmpty() || song.ArtistName.IsNullOrEmpty())
            {
                return false;
            }

            if (song.SongPath.IsNullOrEmpty() || song.ImagePath.IsNullOrEmpty())
            {
                return false;
            }

            return true;
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<IEnumerable<Song>> GetAllSongs()
        {
            return await _repository.GetAll();
        }

        public async Task<Song> GetSongById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task AddSong(Song song)
        {
            if (!ValidSong(song))
            {
                throw new ValidationException("Invalid song data.");
            }

            try
            {
                await _repository.Add(song);
            }
            catch (Exception)
            {
                throw new ArgumentException("Database internal song add error");
            }
        }

        public async Task DeleteSong(Song song)
        {
            try
            {
                await _repository.Delete(song);
            }
            catch (Exception)
            {
                throw new ArgumentException("Database internal song delete eror");
            }
        }
    }
}

