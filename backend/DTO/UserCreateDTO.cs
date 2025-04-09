using backend.helper;

namespace backend.DTO;
public class UserCreateDTO{

     public string Email { get; set; }= string.Empty;
     public string FirstName { get; set; }= string.Empty;
     public string LastName { get; set; }= string.Empty;
     public string PasswordHash { get; set; }= string.Empty;
     public string Location { get; set; }= string.Empty;

}