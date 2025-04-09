using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using backend.helper;


namespace Backend.Model;
public class Notification{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Message {get;set;}
    public NotificationType Type {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [ForeignKey("User")]
    public int UserId {get; set;}
    public User User {get; set;}
  
}