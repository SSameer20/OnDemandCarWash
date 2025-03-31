using backend.DTO;
using backend.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;
    public UserController(IUserRepository repo){
        _repo = repo;
    }

     [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO user)
    {
        try{
            if(user == null){
            throw new Exception("Invalid user data");
        }

        var createdUser = await _repo.Register(user);
        if (createdUser == null)
        {
            throw new Exception("Error while creating the user.");
        }
        return Ok("user created");
        }
       catch (Exception ex)
        {
            return BadRequest($"Error while registering: {ex.Message}");
        }       
    }
    
    [HttpPost("login")]
    public async Task<IActionResult>LoginUser([FromBody] UserLoginDTO user){
       try{
            if(user == null){
                throw new Exception("Incorrect Details");
            }

            bool isValidUser = await _repo.Login(user);
            if(isValidUser){
                return Ok("User logged in");
            }
            else{
                return Ok("Invalid User");

            }

       }
       catch(Exception ex){
        return BadRequest($"Error : {ex.Message}");
       }
    }
}