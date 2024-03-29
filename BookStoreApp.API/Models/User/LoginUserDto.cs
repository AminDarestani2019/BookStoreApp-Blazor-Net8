﻿using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models.User
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
