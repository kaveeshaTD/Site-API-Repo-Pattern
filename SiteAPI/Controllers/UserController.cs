using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteAPI.Applications.DTO;
using SiteAPI.Applications.Interfaces;

namespace SiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _IuserServices;
        public UserController(IUserServices userservices)
        {
            _IuserServices = userservices;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDTO createUserDTO)
        {
            var user = await _IuserServices.CreateUSerAsync(createUserDTO);
            return Ok(user);
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _IuserServices.GetAllUserAsync();
            return Ok(users);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var deleteuser = await _IuserServices.DdeletUserAsync(id);
            return Ok(deleteuser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id , [FromBody] CreateUserDTO updateuser)
        {
            var user = await _IuserServices.UpdateUserAsync(id,updateuser);
            return Ok(user);

        }
    }
}
