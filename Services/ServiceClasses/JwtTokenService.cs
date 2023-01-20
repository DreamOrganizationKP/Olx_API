using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.ServiceClasses
{
    public class JwtTokenService
    {
        IConfiguration _configuration;
        UserManager<User> _userManager;
        public JwtTokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> CreateToken(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("name", user.Name)
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            var jwtKey = _configuration.GetSection("JwtKey").Value;
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jwtKey"));
            var signInCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                    signingCredentials: signInCredentials,
                    expires: DateTime.Now.AddDays(1),
                    claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
