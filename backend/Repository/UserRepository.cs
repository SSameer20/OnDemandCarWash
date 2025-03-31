using backend.DTO;
using backend.Interface;
using AutoMapper;
using Backend.Model;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository{
    private readonly ApplictaionDBContext _context;
    private readonly IMapper _mapper;
    public UserRepository(ApplictaionDBContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserCreateDTO> Register(UserCreateDTO user){
        var newUser = user;
        string password = Hash.EncryptPassword(user.PasswordHash);
        newUser.PasswordHash = password;
        var NewUser = _mapper.Map<User>(newUser);
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
            return isPasswordValid;
        }
        catch (Exception){
            return false;
        }
        
       
    }
}