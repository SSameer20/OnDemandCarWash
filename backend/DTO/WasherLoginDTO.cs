namespace backend.DTO;
public class WasherLoginDTO{
    public int Id {get; set;}

     public string Email { get; set; }= string.Empty;
     public string PasswordHash { get; set; }= string.Empty;

}