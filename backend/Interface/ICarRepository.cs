using backend.DTO;

namespace backend.Interface;

public interface ICarRepository{
    Task<CarCreateDTO> CreateCar(CarCreateDTO car);
    Task<List<CarDTO>> GetAllCarsOfUser(int userId);
    Task<bool> DeleteCar(int userId, int carId);


}