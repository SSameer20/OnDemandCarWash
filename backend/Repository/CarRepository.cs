using backend.DTO;
using backend.Interface;
using AutoMapper;
using Backend.Model;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
namespace backend.Repository;

public class CarRepository : ICarRepository{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    public CarRepository(ApplicationDBContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<CarCreateDTO> CreateCar(CarCreateDTO car){
        var NewCar = _mapper.Map<Car>(car);
        _context.Cars.Add(NewCar);
        await _context.SaveChangesAsync();
        return _mapper.Map<CarCreateDTO>(NewCar);
    }
    public async Task<List<CarDTO>> GetAllCarsOfUser(int userId){
        var allCars = await _context.Cars.Where(car => car.UserId == userId)
        .Include(car =>  car.CarImages)
        .ToListAsync();
        return _mapper.Map<List<CarDTO>>(allCars);
    }
    public async Task<bool> DeleteCar(int userId, int carId){
        try{
             var car = await _context.Cars
            .FirstOrDefaultAsync(c => c.Id == carId && c.UserId == userId);
            if(car == null){
                throw new Exception("Car not found");
            }
           if (car.CarImages != null && car.CarImages.Any())
        {
            _context.CarImages.RemoveRange(car.CarImages);
        }
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception){
            return false;
        }

    }

    
}