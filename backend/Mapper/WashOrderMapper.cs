using AutoMapper;
using backend.DTO;
using Backend.Model;
namespace backend.Mapper;

public class WashOrderMapper : Profile
{
    public WashOrderMapper()
    {
        CreateMap<WashOrder, WashOrderCreateDTO>(); 
        CreateMap<WashOrderCreateDTO, WashOrder>(); 
        
    }
}
