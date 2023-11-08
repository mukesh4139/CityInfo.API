using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user); 

        public Task<bool> UserExistsAsync(string email);

        public Task<User?> GetUserByIdAsync(int id);

        public Task<User?> ValidateUserAsync(string userName, string password);
    }
}
