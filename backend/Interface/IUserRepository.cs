using backend.DTO;

namespace backend.Interface;

public interface IUserRepository{
    Task<UserCreateDTO> Register(UserCreateDTO user);
    Task<bool> Login(UserLoginDTO user);
   Task<ICollection<UserDTO>> GetAllUsers();
   Task<UserDTO>GetUserDetails(string email);
}