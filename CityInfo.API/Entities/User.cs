using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        public string EncryptedPassword { get; set; }

        //[ForeignKey("OrganizationId")]
        [ForeignKey("OrganizationId")]
        public Organization? Organization { get; set; }
        public int OrganizationId { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
