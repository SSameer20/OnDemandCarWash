

using backend.DTO;

namespace backend.Interface;

public interface IWashOrderRepository{
    Task<WashOrderCreateDTO> CreateOrder(WashOrderCreateDTO order);
    Task<bool> ValidateOrder(int userId, int washerId, int carId);
   Task<ICollection<WashOrderCreateDTO>> GetWashOrders();
   Task<ICollection<WashOrderCreateDTO>> GetWashOrdersByUser(int userId);
   Task<ICollection<WashOrderCreateDTO>> GetWashOrdersByWasher(int washerId);

}