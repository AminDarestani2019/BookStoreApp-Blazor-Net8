using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Blazor.WebAssembly.UI.Models.User
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
