using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace InternalModule.Boilerplate.Providers
{
    public class TokenManager
    {
        private static string jwtSecret = ConfigurationManager.AppSettings["jwtSecret"];

        public static string GenerateToken(string username)
        {
            byte[] key = Encoding.UTF8.GetBytes(jwtSecret);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);


            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, username)}),

                Expires = DateTime.Now.AddMinutes(30),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            //token.Payload["favouriteFood"] = "cheese";
            return handler.WriteToken(token);
        }

        public static string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);

            if (principal == null)
                return null;

            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }

            //Claim usernameClaim = identity.FindFirst("userid");
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;

            return username;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                var aa = jwtToken.Claims;
                if (jwtToken == null)
                    return null;

                byte[] key = Encoding.UTF8.GetBytes(jwtSecret);

                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    ValidateLifetime = false,
                    RequireExpirationTime = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
                };

                SecurityToken securityToken;

                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);

                return principal;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}