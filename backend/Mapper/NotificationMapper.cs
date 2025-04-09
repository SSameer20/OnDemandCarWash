using AutoMapper;
using backend.DTO;
using Backend.Model;
namespace backend.Mapper;

public class NotificationMapper : Profile
{
    public NotificationMapper()
    {
        CreateMap<NotificationDTO, Notification>(); 
        CreateMap<Notification, NotificationDTO>(); 
        

    }
}
