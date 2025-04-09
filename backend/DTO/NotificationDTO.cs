namespace backend.DTO;
using backend.helper;

public class NotificationDTO{
    public int UserId {get; set;}
    public  string Message { get; set; } = string.Empty;
    public NotificationType Type {get;set;} = NotificationType.General;
}