using backend.DTO;
using backend.Interface;
using Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WashOrderController : ControllerBase {
    private readonly IWashOrderRepository _repo;
    public WashOrderController(IWashOrderRepository repo){
        _repo = repo;
    }
    [Authorize(Roles = "Admin")]
    [HttpGet("orders")]
    public async Task<IActionResult> GetAllWashOrders(){
        try{
            var data = await _repo.GetWashOrders();
            if(data.Count > 0){
                return Ok(data);
            }
            else {
                return Ok("No Wash Orders");

            }
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("validate")]
    public async Task<IActionResult> ValidateOrder([FromBody] WashOrderCreateDTO order){
        try{
            if(await _repo.ValidateOrder(order.UserId, order.WasherId, order.CarId)){
                return Ok("Valid");
            }
            else{
                return Ok("Invalid Order");
            }
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }
    [Authorize(Roles = "User")]
    [HttpGet("user/id")]
    public async Task<IActionResult> GetWashOrdersByUser(int userId){
        try{
            var UserRole = User.FindFirst("userRole").Value;
            if(UserRole == "User"){
                userId = int.Parse(User.FindFirst("userId").Value);
                if(userId.ToString() == null || userId.ToString() == string.Empty){
                    return BadRequest("Not Authorised");
                }
            }
            var WasherWashOrders = await _repo.GetWashOrdersByUser(userId);
            if(WasherWashOrders.Count > 0){
                return Ok(WasherWashOrders);
            }
            else{
                return Ok("No Wash Orders");
            }
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }

    }
    [Authorize(Roles = "Admin,Washer")]
    [HttpGet("washer/id")]
    public async Task<IActionResult> GetWashOrdersByWasher(int washerId){
        try{      
            var UserRole = User.FindFirst("userRole").Value;
            if(UserRole == "Washer"){
                washerId = int.Parse(User.FindFirst("userId").Value);
                if(washerId.ToString() == null || washerId.ToString() == string.Empty){
                    return BadRequest("Not Authorised");
                }
            }  

            if(washerId.ToString() == null || washerId.ToString() == string.Empty){
                    return BadRequest("Not Authorised");
            }  
            var WasherWashOrders = await _repo.GetWashOrdersByWasher(washerId);
            if(WasherWashOrders.Count > 0){
                return Ok(WasherWashOrders);
            }
            else{
                return Ok("No Wash Orders");
            }
        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }

    }
    [Authorize(Roles = "Admin,User")]
[HttpPost("create")]
    public async Task<IActionResult> CreateWashOrder(WashOrderCreateDTO order){
        try{
            if(order == null){
                throw new Exception("Order is null");
            }

            bool flag = await _repo.ValidateOrder(order.UserId, order.WasherId, order.CarId);
            if(!flag){
                throw new Exception("Invalid Details");
            }
            var createOrder = await _repo.CreateOrder(order);
            return Ok($"Order Created Successfully");

        }
        catch(Exception ex){
            return BadRequest($"Error : {ex.Message}");
        }
    }

}