using backend.DTO;
using Backend.Model;

namespace backend.Interface;

public interface IWasherRepository{
    Task<WasherCreateDTO> CreateWasher(WasherCreateDTO washer);
    Task<WasherLoginDTO?> LoginWasher(string email, string password);
    Task<WasherDTO> GetAllWashers();
    Task<ICollection<WashOrder>> GetAllWashOrders(int washerId);
    Task<ICollection<WashOrder>> GetAllPendingWashOrders(int washerId);
    Task<ICollection<WashOrder>> GetAllCompletedWashOrders(int washerId);
    Task<ICollection<WashOrder>> GetAllCanceledWashOrders(int washerId);

}