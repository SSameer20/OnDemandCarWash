
using backend.Interface;
using AutoMapper;

using Backend.Data;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using backend.DTO;
using Microsoft.AspNetCore.Authorization;

namespace backend.Repository;
public class WasherRepository : IWasherRepository{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    public WasherRepository(ApplicationDBContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }
    public async Task<WasherCreateDTO> CreateWasher(WasherCreateDTO washer){
        var NewWasher = _mapper.Map<User>(washer);
        NewWasher.Role = "Washer";
        NewWasher.IsActive = true;
        NewWasher.PasswordHash = Hash.EncryptPassword(washer.PasswordHash);
        _context.Add(NewWasher);
        await _context.SaveChangesAsync();
        return _mapper.Map<WasherCreateDTO>(NewWasher);
    }
    public async Task<WasherLoginDTO?> LoginWasher(string email, string password)
    {
        if(email == string.Empty || password == string.Empty){
            return null;
        }
        
        var Washer = await _context.Users
        .FirstOrDefaultAsync(w => w.Email == email && w.Role == "Washer");
    

    if (Washer == null) return null; 
    var isValidPassword = Hash.ComparePassword(password, Washer.PasswordHash);
        if(!isValidPassword){
            return null;
        }
        return _mapper.Map<WasherLoginDTO>(Washer);
    }

   
    public async Task<WasherDTO> GetAllWashers(){
        var washers = await _context.Users.Where(u => u.Role == "Washer").ToListAsync();
        return _mapper.Map<WasherDTO>(washers);
    }


    public async Task<ICollection<WashOrder>> GetAllWashOrders(int userId){
        var washers = await _context.WashOrders.Where(u => u.UserId == userId).ToListAsync();
        return washers;
    }
    public async Task<ICollection<WashOrder>> GetAllPendingWashOrders(int washerId){
        var orders = await _context.WashOrders.Where(wo => wo.WasherId == washerId && wo.Status == "pending").ToListAsync();
        return orders;
    }
    public async Task<ICollection<WashOrder>> GetAllCompletedWashOrders(int washerId){
         var orders = await _context.WashOrders.Where(wo => wo.WasherId == washerId && wo.Status == "completed").ToListAsync();
        return orders;
    }
    public async Task<ICollection<WashOrder>> GetAllCanceledWashOrders(int washerId){
         var orders = await _context.WashOrders.Where(wo => wo.WasherId == washerId && wo.Status == "canceled").ToListAsync();
        return orders;
    }

   
}