using AutoMapper;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Mappings
{
    public class SoundMappingProfile : Profile
    {
        public SoundMappingProfile()
        {
            CreateMap<Sound, SoundForAddUpdateModel>().ReverseMap();
        }
    }
}
