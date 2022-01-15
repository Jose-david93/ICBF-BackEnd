using System.ComponentModel.DataAnnotations;

namespace ICBF.Domain.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
