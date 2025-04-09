using backend.DTO;

namespace backend.Interface;

public interface IAdminRepository{
    Task<UserCreateDTO> Register(UserCreateDTO user);
    Task<bool> Login(AdminLoginDTO user);
    Task<AdminDTO>GetUserDetails(string email);
  
}