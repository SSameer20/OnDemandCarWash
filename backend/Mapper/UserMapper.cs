using AutoMapper;
using backend.DTO;
using Backend.Model;
namespace backend.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserDTO, User>(); 
        CreateMap<User, UserDTO>(); 
        CreateMap<UserCreateDTO, User>(); 
        CreateMap<User, UserCreateDTO>(); 
        CreateMap<UserLoginDTO, User>(); 
        CreateMap<User, UserLoginDTO>(); 
        CreateMap<User, WasherDTO>();
        CreateMap<User, WasherLoginDTO>(); 
        CreateMap<WasherLoginDTO,User>(); 
        CreateMap<WasherDTO, User>(); 
        CreateMap<WasherCreateDTO, User>();
        CreateMap<User, WasherCreateDTO>(); 
        CreateMap<User, AdminDTO>(); 
        CreateMap<AdminDTO,User>(); 
        CreateMap<User, AdminLoginDTO>(); 
        CreateMap<AdminLoginDTO, User>();
    }
}
