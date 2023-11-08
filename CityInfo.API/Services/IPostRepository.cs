using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface IPostRepository
    {
        public Task AddPostsForUserAsync(int userId, Post post);

        public Task<IEnumerable<Post>> GetPostsForUserAsync(int userId);
    }
}
