using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public interface ISoundRepository
    {
        Task<Sound?> GetSoundById(int soundId);
        Task<IEnumerable<Sound>> GetAllSounds();
        Task<IEnumerable<Sound>> FilterSoundsByType(SoundType type);
        Task<int> AddSound(Sound sound);
        Task<bool> DeleteSound(int soundId);
        Task<bool> UpdateSound(int soundId, Sound sound);
    }
}
