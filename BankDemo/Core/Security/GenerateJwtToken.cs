using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BankDemo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BankDemo.Core.Security
{
    public class GenerateJwtToken
    {
        static public void GenerateToken(ApplicationUser user, IList<string> userRoles, out string token)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Email", user.Email));
            
            IdentityOptions identityOptions = new IdentityOptions();
            foreach (string role in userRoles)
            {
                claims.Add(new Claim(identityOptions.ClaimsIdentity.RoleClaimType, role));
            }
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.Configuration["JWTkey"].ToString())),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            token = jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}