using GrotHotelApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrotHotelApi.HotelRepository.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<TempUser> ValidateUser(LoginUser _login);
    }
}