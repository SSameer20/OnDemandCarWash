namespace backend.DTO;
public class UserCreateDTO{

     public string Email { get; set; }
     public string PasswordHash { get; set; }
     public string Role { get; set; }
     public string Location { get; set; }

}