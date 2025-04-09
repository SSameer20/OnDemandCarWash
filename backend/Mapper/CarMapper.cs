using AutoMapper;
using backend.DTO;
using Backend.Model;
namespace backend.Mapper;

public class CarMapper : Profile
{
    public CarMapper()
    {
        CreateMap<CarDTO, Car>(); 
        CreateMap<Car, CarDTO>(); 
        CreateMap<CarCreateDTO, Car>()
            .ForMember(dest => dest.CarImages, opt => opt.MapFrom(src => src.CarImages));

        CreateMap<Car, CarCreateDTO>()
            .ForMember(dest => dest.CarImages, opt => opt.MapFrom(src => src.CarImages));
    }
}
