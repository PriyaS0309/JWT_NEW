using JWT_NEW.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT_NEW.Repos
{
    
    public class JWTManagerRepo : IJWTManagerRepo
    {
        private  IConfiguration configuration;

        Dictionary<string, string> userRecords = new Dictionary<string, string>
        {
            {"user1","password1" },
            {"user2","password2" }
        };

        public JWTManagerRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Tokens Authenticate(Users user)
        {
            if (!userRecords.Any(model => model.Key == user.Username && model.Value == user.Password))
            {
                return null;
            }
            else
            {
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, user.Username) }

                        ),

                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(tokenkey),SecurityAlgorithms.HmacSha256Signature)
                };

                //creating token
                var token = tokenhandler.CreateToken(tokenDescriptor);

                return new Tokens { Token = tokenhandler.WriteToken(token) };
            }
        }
    }
}
