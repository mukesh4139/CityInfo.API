using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

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

            user.EncryptedPassword = BCrypt.Net.BCrypt.HashPassword(user.EncryptedPassword);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // check if user exists with same email
        public async Task<bool> UserExistsAsync(String email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<User?> ValidateUserAsync(string userName, string password)
        {
            var user = await _context.Users.Where(u => u.Email == userName).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new ArgumentException("Invalid User credentials");
            }

            bool verified =  BCrypt.Net.BCrypt.Verify(password, user.EncryptedPassword);
            if (!verified)
            {
                throw new ArgumentException("Invalid User credentials");
            }

            return user;
        }
    }
}
