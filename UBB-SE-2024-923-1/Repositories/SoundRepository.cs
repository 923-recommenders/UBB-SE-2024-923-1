using Microsoft.EntityFrameworkCore;
using UBB_SE_2024_923_1.Data;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Repositories
{
    public class SoundRepository : ISoundRepository
    {
        private readonly DataContext context;

        public SoundRepository(DataContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> AddSound(Sound sound)
        {
            await context.Sounds.AddAsync(sound);
            await context.SaveChangesAsync();

            return sound.Id;
        }

        public async Task<bool> DeleteSound(int soundId)
        {
            var soundToRemove = await context.Sounds.FirstOrDefaultAsync(sound => sound.Id == soundId);
            if (soundToRemove == null)
            {
                return false;
            }

            context.Sounds.Remove(soundToRemove);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Sound>> GetAllSounds()
        {
            return await context.Sounds.ToListAsync();
        }

        public async Task<IEnumerable<Sound>> FilterSoundsByType(SoundType type)
        {
            return await context.Sounds.Where(sound => sound.Type == type).ToListAsync();
        }

        public async Task<Sound?> GetSoundById(int soundId)
        {
            return await context.Sounds.FirstOrDefaultAsync(sound => sound.Id == soundId);
        }

        public async Task<bool> UpdateSound(int soundId, Sound sound)
        {
            var soundToUpdate = await context.Sounds.FirstOrDefaultAsync(sound => sound.Id == soundId);
            if (soundToUpdate == null)
            {
                return false;
            }

            sound.Id = soundId;

            context.Sounds.Entry(soundToUpdate).CurrentValues.SetValues(sound);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
