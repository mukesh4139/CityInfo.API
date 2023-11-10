namespace CityInfo.API.Models
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int UserId { get; set; }
    }
}
