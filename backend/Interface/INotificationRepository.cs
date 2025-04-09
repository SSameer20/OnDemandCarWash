
using backend.DTO;
using Backend.Model;

namespace backend.Interface;

public interface INotificationRepository{
    Task<NotificationDTO> CreateGeneralNotification(int userId, string message);
    Task<NotificationDTO> CreateCruciallNotification(int userId, string message);
    Task<ICollection<NotificationDTO>> GetAllUserNotifications(int userId);
    Task<ICollection<NotificationDTO>> GetGeneralNotifications(int userId);
    Task<ICollection<NotificationDTO>> GetCrucialNotifications(int userId);

    
}