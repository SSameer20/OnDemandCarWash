using backend.DTO;
using backend.helper;
using backend.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WasherController : ControllerBase
{
    private readonly IWasherRepository _repo;
    private readonly JwtHelper _jwtHelper;
    private readonly INotificationRepository _msg;
 
    public WasherController(IWasherRepository repo, JwtHelper jwtHelper, INotificationRepository msg){
        _repo = repo;
        _jwtHelper = jwtHelper;
        _msg = msg;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateWasher([FromBody] WasherCreateDTO washerDTO){
        try{
            var washer = await _repo.CreateWasher(washerDTO);
            if(washer == null){
                throw new Exception("All fields are required");
            }
    
            return Ok("Washer Created");
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }

     [HttpPost("login")]
    public async Task<IActionResult> LoginWasher(string email, string password){
        try{
            if(email == string.Empty || password == string.Empty){
                return BadRequest("Require Credentials");
            }
            var washer = await _repo.LoginWasher(email, password);
            if(washer == null){
                throw new Exception("Wrong or Missing Credentials");
            }

            var Washer = await _repo.LoginWasher(email, password);
            if(Washer == null) {
                throw new Exception("Invalid Credentails");
            }
            
            if(Washer != null){
                
                var token = _jwtHelper.GenerateToken(Washer.Id, Washer.Email,"Washer");
                return Ok(new {token = token });
            }
            else{
                return Ok("Invalid Washer Credentails");
            }
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }
    [Authorize(Roles="Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllWashers(){
        try{
            return Ok(await _repo.GetAllWashers());
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }
    [Authorize(Roles="Admin,Washer")]
    [HttpGet("orders/all")]
    public async Task<IActionResult> GetAllWashOrders(int userId){
        try{
            var claim = User.FindFirst("userRole");
            if(claim == null) {
                return Unauthorized("Unauthorized Access");
            }
            var UserRole = claim.Value;
            if(UserRole == "Washer"){
                var claimId = User.FindFirst("userId");
            if(claimId == null) {
                return Unauthorized("Unauthorized Access");
            }
                userId = int.Parse(claimId.Value);
                if(userId.ToString() == string.Empty){
                    return Unauthorized("You are not authorized to access this endpoint");
                }
            }
            var washOrders = await _repo.GetAllWashOrders(userId);
            if(washOrders.Count > 0){
                return Ok(washOrders);
            }
            else return NotFound("No wash orders found for this washer");
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }
    [Authorize(Roles = "Admin,Washer")]
    [HttpGet("washer/pending")]
    public async Task<IActionResult> GetAllPendingWashOrders(int washerId){
        try{
            var claim = User.FindFirst("userRole");
            if(claim == null){
                return Unauthorized("Unauthorized Access");
            }
            var Washer = claim.Value;
            if(Washer == "Washer"){
                var claimId = User.FindFirst("userId");
            if(claimId == null){
                return Unauthorized("Unauthorized Access");
            }
                washerId = int.Parse(claimId.Value);    
                if(washerId.ToString() == null || washerId.ToString() == string.Empty){
                    return Unauthorized("You are not authorized to access this endpoint");
                }
            }

            if(washerId.ToString() == null || washerId.ToString() == string.Empty){
                return BadRequest("Washer Id is required");
            }
            var washOrders = await _repo.GetAllPendingWashOrders(washerId);
            if(washOrders.Count > 0){
            
                return Ok(washOrders);
            }
            else return NotFound("No pending orders found for this washer");
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }
    [Authorize(Roles = "Admin,Washer")]
    [HttpGet("washer/completed")]
    public async Task<IActionResult> GetAllCompletedWashOrders(int washerId){
        try{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var Washer = User.FindFirst("userRole").Value;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (Washer == "Washer"){
                var claim = User.FindFirst("userId");
                if(claim == null){
                    return Unauthorized("You are not authorized to access this endpoint");
                }
                washerId = int.Parse(claim.Value);
                if(washerId.ToString() == null || washerId.ToString() == string.Empty){
                    return Unauthorized("You are not authorized to access this endpoint");
                }
            }

            if(washerId.ToString() == null || washerId.ToString() == string.Empty){
                return BadRequest("Washer Id is required");
            }
            var washOrders = await _repo.GetAllCompletedWashOrders(washerId);
            if(washOrders.Count > 0){
                return Ok(washOrders);
            }
            else return NotFound("No pending orders found for this washer");
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }

}