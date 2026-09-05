using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController (IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]        
        public async Task<IActionResult> GetUsers ()
        {
            
                var users = await _userService.GetUsers();

                return Ok(users);
            
           
            
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById (int id)
        {
           
                var user = await _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            
           
            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser (int id, TaskManagementAPI.DTOs.UpdateUserRequest user)
        {
            
                var updatedUser = await _userService.UpdateUser(id, user);

                if (updatedUser == null)
                {
                    return NotFound();
                }

                return Ok(updatedUser);
            
           
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser (int id)
        {
            
                var user = await _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                await _userService.DeleteUser(id);

                return NoContent();
            }
           
            
        
    }
}
