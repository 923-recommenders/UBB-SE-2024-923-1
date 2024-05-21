using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using UBB_SE_2024_923_1.Models;
using UBB_SE_2024_923_1.Repositories;
using UBB_SE_2024_923_1.Services;

namespace NamespaceCBlurred.Business.Services
{
    public class SoundService : ISoundService
    {
        private readonly ISoundRepository soundRepository;
        private readonly IMapper mapper;

        public SoundService(ISoundRepository soundRepository, IMapper mapper)
        {
            this.soundRepository = soundRepository ?? throw new ArgumentNullException(nameof(soundRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private static bool ValidSoundModel(SoundForAddUpdateModel soundModel)
        {
            if (soundModel.Name.IsNullOrEmpty() || soundModel.SoundFilePath.IsNullOrEmpty())
            {
                return false;
            }

            return true;
        }

        public async Task<Sound> AddSound(SoundForAddUpdateModel soundModel)
        {
            if (!ValidSoundModel(soundModel))
            {
                throw new ValidationException("Invalid sound data.");
            }

            var sound = mapper.Map<Sound>(soundModel);

            int id = await soundRepository.AddSound(sound);
            sound.Id = id;

            return sound;
        }

        public async Task<bool> DeleteSound(int soundId)
        {
            if (soundId < 0)
            {
                throw new ValidationException("Invalid sound id.");
            }

            return await soundRepository.DeleteSound(soundId);
        }

        public async Task<IEnumerable<Sound>> GetAllSounds()
        {
            return await soundRepository.GetAllSounds();
        }

        public async Task<IEnumerable<Sound>> FilterSoundsByType(SoundType type)
        {
            return await soundRepository.FilterSoundsByType(type);
        }

        public async Task<Sound?> GetSoundById(int soundId)
        {
            if (soundId < 0)
            {
                throw new ValidationException("Invalid sound id.");
            }

            return await soundRepository.GetSoundById(soundId);
        }

        public async Task<bool> UpdateSound(int soundId, SoundForAddUpdateModel soundModel)
        {
            if (soundId < 0)
            {
                throw new ValidationException("Invalid sound id.");
            }

            if (!ValidSoundModel(soundModel))
            {
                throw new ValidationException("Invalid sound data.");
            }

            return await soundRepository.UpdateSound(soundId, mapper.Map<Sound>(soundModel));
        }
    }
}
