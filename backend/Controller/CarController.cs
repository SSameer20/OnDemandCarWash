using System.Security.Claims;
using backend.DTO;
using backend.helper;
using backend.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarRepository _repo;
    private readonly INotificationRepository _msg;
    private readonly JwtHelper _jwtHelper;

    public CarController(ICarRepository repo, INotificationRepository msg, JwtHelper jwtHelper)
    {
        _repo = repo;
        _msg = msg;
        _jwtHelper = jwtHelper;
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> Create(CarCreateDTO car)
    {
        try
        {
            if (car == null)
            {
                throw new Exception("Car Details are required");
            }

            var userClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userClaim))
            {
                return Unauthorized("Invalid token: UserId is missing.");
            }


            int userId = int.Parse(userClaim);
            car.UserId = userId;
            await _repo.CreateCar(car);
            await _msg.CreateGeneralNotification(userId, "Car Details Added successfully");

            return Ok("Car created");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error : {ex.Message}");
        }
    }

    [Authorize(Roles = "User,Admin")]
    [HttpGet("user")]
    public async Task<IActionResult> GetUserCar(int? userId)
    {
        try
        {
            var userRole = User.FindFirst("userRole")?.Value ?? string.Empty;

            if (userRole != "Admin")
            {
                var userIdClaim = User.FindFirst("userId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return Unauthorized("Invalid token: UserId is missing.");
                }

                userId = int.Parse(userIdClaim);
            }

            if (userRole == "Admin" && userId == null)
            {
                return BadRequest("specify a UserId.");
            }
            if(userId == null){
                return BadRequest("User Id Required.");
            }
            var cars = await _repo.GetAllCarsOfUser(userId.Value);
            if (cars.Count < 1)
            {
                return NotFound($"No car found for userId: {userId}");
            }

            return Ok(cars);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error: {ex.Message}");
        }
    }

    [Authorize(Roles = "User,Admin")]
[HttpDelete("{carId}")]
public async Task<IActionResult> DeleteCar(int carId, [FromQuery] int? userId = null)
{
    try
    {
        var userRole = User.FindFirst("userRole")?.Value ?? string.Empty;
        var userIdClaim = User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            return Unauthorized("Invalid token: UserId is missing.");
        }

        int loggedInUserId = int.Parse(userIdClaim);

        if (userRole == "User")
        {
            if (userId != null && userId != loggedInUserId)
            {
                return Unauthorized("User Not Authorized to Delete Others' Car ID.");
            }

            userId = loggedInUserId; 
        }

        if (userId == null)
        {
            return BadRequest("UserId is required.");
        }

        if (carId <= 0)
        {
            return BadRequest("Invalid CarId.");
        }

        await _repo.DeleteCar(userId.Value, carId);
        await _msg.CreateCruciallNotification(userId.Value, "Car details deleted");

        return Ok("Car deleted successfully.");
    }
    catch (Exception ex)
    {
        return BadRequest($"Error: {ex.Message}");
    }
} 
}
