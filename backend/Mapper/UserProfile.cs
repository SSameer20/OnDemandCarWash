using AutoMapper;
using backend.DTO;
using Backend.Model;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDTO, User>(); 
        CreateMap<User, UserDTO>(); 
        CreateMap<UserCreateDTO, User>(); 
        CreateMap<User, UserCreateDTO>(); 
        CreateMap<UserLoginDTO, User>(); 
        CreateMap<User, UserLoginDTO>(); 
    }
}
