using backend.helper;

namespace backend.DTO;
public class UserDTO{
     public int Id { get; set; }
     public string FirstName { get; set; }= string.Empty;
     public string LastName { get; set; }= string.Empty;
     public string Email { get; set; }= string.Empty;
     public string Role { get; set; } = "User";
     public string Location { get; set; }= string.Empty;
     public bool IsActive { get; set; } = true;
}