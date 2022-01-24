using System.ComponentModel.DataAnnotations;

namespace TheTowerAPI.DTOs
{
    public class UserRegisterRequest
    {
        [Required]
        [MaxLength(20)]
        public string Nickname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}