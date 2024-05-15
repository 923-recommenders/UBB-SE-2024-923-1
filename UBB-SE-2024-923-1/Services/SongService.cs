using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;

namespace UBB_SE_2024_923_1.Services
{
    public class SongService
    {
        private readonly IRepository<Song> _repository;
        private readonly IRepository<Users> _userRepository;

        public SongService(IRepository<Song> repository, IRepository<Users> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
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

