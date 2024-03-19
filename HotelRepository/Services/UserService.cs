using GrotHotelApi.Data;
using GrotHotelApi.HotelRepository.IServices;
using GrotHotelApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrotHotelApi.HotelRepository.Services
{
    public class UserService : IUserService
    {
        private readonly GrotHotelApiDbContext _context;

        public UserService(GrotHotelApiDbContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUsers()
        {
            return _context.Users.ToList();
        }
        public async Task<TempUser> ValidateUser(LoginUser _login)
        {
            var Isvalidate = _context.Users.Any(s => s.Username == _login.Username && s.Password == _login.Password);
            if (Isvalidate)
            {
                var user = _context.Users.FirstOrDefault(s => s.Username == _login.Username && s.Password == _login.Password);
                if (user != null)
                {
                    return new TempUser {id=user.Id,username=user.Username,role=user.Roles };

                }
            }
            return null;
        }
    }
}