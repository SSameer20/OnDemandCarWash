using backend.DTO;
using backend.Interface;
using AutoMapper;
using Backend.Model;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using backend.helper;
namespace backend.Repository;
public class UserRepository : IUserRepository{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    public UserRepository(ApplicationDBContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserCreateDTO> Register(UserCreateDTO user){
        string password = Hash.EncryptPassword(user.PasswordHash);
        user.PasswordHash = password;
        var NewUser = _mapper.Map<User>(user);
        NewUser.Role = "User";
        _context.Users.Add(NewUser);
        await _context.SaveChangesAsync();
        return _mapper.Map<UserCreateDTO>(NewUser);
       
    }
     public async Task<bool> Login(UserLoginDTO user){
        try{
            var findUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (findUser == null)
            throw new Exception("user not found"); 
            bool isPasswordValid = Hash.ComparePassword(user.PasswordHash, findUser.PasswordHash);
            if (isPasswordValid){
                
            }
            return isPasswordValid;
        }
        catch (Exception){
            return false;
        } 
    }
    public async Task<ICollection<UserDTO>> GetAllUsers(){
        var allUsers = await _context.Users.ToListAsync();  
        return _mapper.Map<ICollection<UserDTO>>(allUsers);
    }

    public async Task<UserDTO> GetUserDetails(string email){
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return _mapper.Map<UserDTO>(user);
    }
}