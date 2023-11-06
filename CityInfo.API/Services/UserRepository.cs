using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly CityInfoContext _context;

        public UserRepository(CityInfoContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // check if user exists with same email
        public async Task<bool> UserExistsAsync(String email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
