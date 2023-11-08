using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //[ForeignKey("UserId")]
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
