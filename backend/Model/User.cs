using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.DTO;
using backend.helper;

namespace Backend.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string Role { get; set; }
        
        public string Location { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public ICollection<Car> ?Cars { get; set; }
        public ICollection<WashOrder> ?WashOrders { get; set; } 
        public ICollection<Notification> ?Notifications { get; set; }
    }
}
