using System.ComponentModel.DataAnnotations;

namespace TheTowerAPI.DTOs
{
    public class UserLoginRequest
    {
        [Required]
        [MaxLength(20)]
        public string Nickname { get; set; }
        [Required]
        public string Password { get; set; }
    }
}