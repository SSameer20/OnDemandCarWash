using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using backend.DTO;
using backend.Interface;
using Backend.Data;
using Backend.Model;
using Microsoft.EntityFrameworkCore;

public class NotificationRepository : INotificationRepository {
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    public NotificationRepository(ApplicationDBContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<NotificationDTO> CreateGeneralNotification(int userId, string message){
        NotificationDTO notification = new NotificationDTO {
            Message = message,
            UserId = userId,
            Type = backend.helper.NotificationType.General
        };

        var notificationEntity = _mapper.Map<Notification>(notification);
        _context.Notifications.Add(notificationEntity);
        await _context.SaveChangesAsync();
        return notification;  
    }
    public async Task<NotificationDTO> CreateCruciallNotification(int userId, string message){
        NotificationDTO notification = new NotificationDTO {
            Message = message,
            UserId = userId,
            Type = backend.helper.NotificationType.Crucial
        };

        var notificationEntity = _mapper.Map<Notification>(notification);
        _context.Notifications.Add(notificationEntity);
        await _context.SaveChangesAsync();
        return notification;  
    }
    public async Task<ICollection<NotificationDTO>> GetAllUserNotifications(int userId){
        var Identity = await _context.Notifications.Where(u => u.UserId  == userId).ToListAsync();
        return _mapper.Map<ICollection<NotificationDTO>>(Identity);
    }
     public async Task<ICollection<NotificationDTO>> GetGeneralNotifications(int userId){
        var Identity = await _context.Notifications.Where(u => u.UserId  == userId && u.Type == backend.helper.NotificationType.General).ToListAsync();
        return _mapper.Map<ICollection<NotificationDTO>>(Identity);
    }
    public async Task<ICollection<NotificationDTO>> GetCrucialNotifications(int userId){
        var Identity = await _context.Notifications.Where(u => u.UserId  == userId && u.Type == backend.helper.NotificationType.Crucial).ToListAsync();
        return _mapper.Map<ICollection<NotificationDTO>>(Identity);
    }
}