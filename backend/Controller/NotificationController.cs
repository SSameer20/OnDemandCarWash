using backend.DTO;
using backend.Interface;
using Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase {
    private readonly INotificationRepository _repo;
    public NotificationController(INotificationRepository repo){
        _repo = repo;
    }
     [Authorize(Roles = "Admin,User")]
    [HttpGet("user/all")]
    public async Task<IActionResult> GetAllUserNotifications(int? userId){
        try{
            var UserRole = User.FindFirst("userRole")?.Value;
            if(UserRole == null){
                return Unauthorized("Unauthorized");
            }
            
            if(UserRole == "User"){
                userId = int.Parse(User.FindFirst("userId").Value);
                
            }
            else{
                if(userId.ToString() == string.Empty){
                    return BadRequest("User Id Required to get all notifications");
                }
            }

            if(userId == null){
                return BadRequest("User Id Required to get all notifications");
            }

            var notifications = await _repo.GetAllUserNotifications(userId.Value);
            return Ok(notifications);
    }
    catch(Exception ex){
        return BadRequest($"Error : {ex.Message}");
    }
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet("user/general")]

    public async Task<IActionResult> GetGeneralNotifications(int userId){
        try{        
            var UserRole = User.FindFirst("userRole")?.Value;
            if(UserRole == null){
                return Unauthorized("Unauthorized");
            }
            
            if(UserRole == "User"){
                userId = int.Parse(User.FindFirst("userId").Value);
                
            }
            else{
                if(userId.ToString() == string.Empty){
                    return BadRequest("User Id Required to get all notifications");
                }
            }

            if(userId == null){
                return BadRequest("User Id Required to get general notifications");
            }

            var notifications = await _repo.GetGeneralNotifications(userId);
            return Ok(notifications);
    }
    catch(Exception ex){
        return BadRequest($"Error : {ex.Message}");
    }
    }

    [HttpGet("user/crucial")]
    public async Task<IActionResult> GetCrucialNotifications(int userId){
    try{
       var UserRole = User.FindFirst("userRole")?.Value;
            if(UserRole == null){
                return Unauthorized("Unauthorized");
            }
            
            if(UserRole == "User"){
                userId = int.Parse(User.FindFirst("userId").Value);
                
            }
            else{
                if(userId.ToString() == string.Empty){
                    return BadRequest("User Id Required to get all notifications");
                }
            }

            if(userId == null){
                return BadRequest("User Id Required to get general notifications");
            }

            var notifications = await _repo.GetCrucialNotifications(userId);
            return Ok(notifications);
    }
    catch(Exception ex){
        return BadRequest($"Error : {ex.Message}");
    }
    }
}