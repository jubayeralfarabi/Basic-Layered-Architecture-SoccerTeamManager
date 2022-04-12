using Framework.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Soccer.Infrastructure
{
    public class SecurityContext : ISecurityContext
    {
        private UserContext userContext = new UserContext(0, null, null, null);
        public UserContext UserContext
        {
            get { return userContext; }

            set
            {
                userContext = value;
            }
        }

        public string GenerateJSONWebToken(int userId, string email, string firstName, string lastName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Thisisasecretkey"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("userId", userId.ToString()),
                new Claim("firstName", firstName),
                new Claim("lastName", lastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken("self.com",
                "self.com",
                claims,
                expires: DateTime.Now.AddHours(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
