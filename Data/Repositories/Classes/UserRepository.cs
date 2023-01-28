using Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Data.Repositories.Classes
{
    public class UserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(User model, string password)
        {
            var result = await _userManager.CreateAsync(model, password);
            return result;
        }

        public async Task<IdentityResult> RegisterAsync(User model)
        {
            var result = await _userManager.CreateAsync(model);
            return result;
        }

        public async Task<SignInResult> LoginAsync(string userName, string password, bool rememberMe)
        {
            
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, true);
            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo)
        {

            var result = await _userManager.AddLoginAsync(user, loginInfo);
            return result;
        }

        public async Task<User> GetUserByEmail(string email)
        {

            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }
    }
}
