using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Blazor.Server.UI.Models.User
{
    public class LoginUserDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
