
using AutoMapper;
using backend.DTO;
using Backend.Model;
namespace backend.Mapper;

public class CarImageMapper : Profile
{
    public CarImageMapper()
    {
        CreateMap<CarImageDTO, CarImage>(); 
        CreateMap<CarImage, CarImageDTO>(); 
    }
}
