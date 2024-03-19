using GrotHotelApi.HotelRepository.IServices;
using GrotHotelApi.HotelRepository.Services;
using GrotHotelApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrotHotelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUserService _service;

        public UserApiController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetUsers();

            return Ok(users);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser _login)
        {
            if (string.IsNullOrEmpty(_login.Username) || string.IsNullOrEmpty(_login.Password))
                throw new Exception("Credentials are not valid");

            var userData = await _service.ValidateUser(_login);

            if (userData != null)
            {
                return Ok(userData);
            }
            if (userData == null)
                throw new Exception("User is not valid");
            return Unauthorized();
        }

    }
}
