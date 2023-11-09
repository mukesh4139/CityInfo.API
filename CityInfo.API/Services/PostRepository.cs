using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly CityInfoContext _context;
        private readonly IUserRepository _userRepository;

        public PostRepository(CityInfoContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public async Task AddPostsForUserAsync(int userId, Post post)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsForUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var collection = _context.Posts as IQueryable<Post>;


            return await collection.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Post?> GetPostById(int postId)
        {
            
            return await _context.Posts.Where(p => p.Id == postId).FirstOrDefaultAsync();
        }
    }
}
