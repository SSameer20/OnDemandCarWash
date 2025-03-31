using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Model;
public class User{
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public int Id { get; set; }
     public string Email { get; set; }
     public string PasswordHash { get; set; }
     public string Role { get; set; }
     public string Location { get; set; }
     public bool IsActive { get; set; } = true;
     public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}