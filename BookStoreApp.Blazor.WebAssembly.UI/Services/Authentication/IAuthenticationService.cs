using BookStoreApp.Blazor.WebAssembly.UI.Models.User;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);
        public Task Logout();
    }
}
