namespace backend.DTO;
public class UserDTO{
     public int Id { get; set; }
     public string Email { get; set; }
     public string Role { get; set; }
     public string Location { get; set; }
     public bool IsActive { get; set; } = true;

}