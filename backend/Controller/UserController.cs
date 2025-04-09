using backend.helper;
using backend.DTO;
using backend.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Model;
namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;
    private readonly INotificationRepository _msg;
    private readonly JwtHelper _jwtHelper;

    public UserController(IUserRepository repo, INotificationRepository msg,JwtHelper jwtHelper){
        _repo = repo;
        _msg = msg;
        _jwtHelper = jwtHelper;
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
            var userDetails = await _repo.GetUserDetails(user.Email);
            if(isValidUser){
                
                var token = _jwtHelper.GenerateToken(userDetails.Id,userDetails.Email, "User");
                return Ok(new {token = token });
            }
            else{
                return Ok("Invalid User");

            }

       }
       catch(Exception ex){
        return BadRequest($"Error : {ex.Message}");
       }
    }
    [HttpGet("debug-claims")]
public IActionResult DebugClaims()
{
    var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
    return Ok(claims);
}

    [Authorize(Roles = "admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUser(){
        try{
            var users = await _repo.GetAllUsers();
            return Ok(users);
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }
}