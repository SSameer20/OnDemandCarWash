using AutoMapper;
using backend.DTO;
using backend.Interface;
using Backend.Data;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository; 
public class WashOrderRepository : IWashOrderRepository{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    public WashOrderRepository(ApplicationDBContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<WashOrderCreateDTO> CreateOrder(WashOrderCreateDTO order){
       var NewWashOrder = _mapper.Map<WashOrder>(order);
       NewWashOrder.OrderDate = DateTime.Now;
       _context.WashOrders.Add(NewWashOrder);
       await _context.SaveChangesAsync();
       return _mapper.Map<WashOrderCreateDTO>(NewWashOrder);
    }

       public async Task<ICollection<WashOrderCreateDTO>> GetWashOrders(){
        var washOrders = await _context.WashOrders.ToListAsync();
        return _mapper.Map<ICollection<WashOrderCreateDTO>>(washOrders);
       }

        public async Task<bool> ValidateOrder(int userId, int washerId, int carId){
            var user = await _context.Users
            .Where(u => u.Role == "User" && u.Id == userId)
            .FirstOrDefaultAsync();
            var washer = await _context.Users
            .Where(u => u.Role ==  "Washer" && u.Id == washerId)
            .FirstOrDefaultAsync();
            var Car = await _context.Cars
            .Where(c => c.Id == carId)
            .FirstOrDefaultAsync();
            if(user != null && washer != null && Car != null){
                return true;
            }
            return false;
       }
        public async Task<ICollection<WashOrderCreateDTO>> GetWashOrdersByUser(int userId){
        var washOrdersByUser = await _context.WashOrders.Where(order => order.UserId == userId).ToListAsync();
        return _mapper.Map<ICollection<WashOrderCreateDTO>>(washOrdersByUser);
        }
        public async Task<ICollection<WashOrderCreateDTO>> GetWashOrdersByWasher(int washerId){
            var washOrdersByWasher = await _context.WashOrders.Where(order => order.WasherId == washerId).ToListAsync();
        return _mapper.Map<ICollection<WashOrderCreateDTO>>(washOrdersByWasher);
        }
}