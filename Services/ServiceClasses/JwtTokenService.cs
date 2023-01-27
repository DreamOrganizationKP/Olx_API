using Data.Models;
using Google.Apis.Auth;
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

        public async Task<string> CreateTokenAsync(User user)
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

            var jwtKey = _configuration.GetSection("JwtOptions:JwtKey").Value;
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signInCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                    signingCredentials: signInCredentials,
                    expires: DateTime.Now.AddDays(1),
                    claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string googleToken)
        {
            string clientID = "17761511781-e38853miclpejbb61bhm76085f7oe1o2.apps.googleusercontent.com";

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { clientID }
            };

            var result = await GoogleJsonWebSignature.ValidateAsync(googleToken, settings);
            return result;
        }
    }
}
