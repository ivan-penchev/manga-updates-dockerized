using System.ComponentModel.DataAnnotations;
using static MU.Identity.Infrastructure.DataConstants.Identity;

namespace MU.Identity.Models.Identity
{
    public class UserInputModel
    {
        [EmailAddress]
        [Required]
        [MinLength(MinEmailLength)]
        [MaxLength(MaxEmailLength)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
