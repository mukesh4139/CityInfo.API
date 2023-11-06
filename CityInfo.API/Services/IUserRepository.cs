using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user); 

        public Task<bool> UserExistsAsync(String email);
    }
}
