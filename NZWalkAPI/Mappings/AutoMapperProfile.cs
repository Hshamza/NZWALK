using AutoMapper;
using NZWalkAPI.Models.Domain;
using NZWalkAPI.Models.DTO;

namespace NZWalkAPI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionDTO, Region>().ReverseMap();
            CreateMap<AddWalkRequestDTO, Walk>().ReverseMap();
            CreateMap<Walk,WalkDTO>().ReverseMap(); 
            CreateMap<Difficulty,DifficultyDTO>().ReverseMap();
            CreateMap<UpdateWalkDTO, Walk>().ReverseMap();
        }
    }
}
