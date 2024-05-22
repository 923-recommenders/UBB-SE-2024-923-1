using AutoMapper;
using UBB_SE_2024_923_1.Models;

namespace UBB_SE_2024_923_1.Mappings
{
    public class PlaylistMappingProfile : Profile
    {
        public PlaylistMappingProfile()
        {
            CreateMap<Playlist, PlaylistForAddUpdateModel>().ReverseMap();
        }
    }
}
