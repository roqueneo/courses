using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain;
using Microsoft.IdentityModel.Tokens;

namespace Security.JsonWebTokens
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CreateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey# 3215"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var descriptionToken = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = credentials
            };

            var tokenManager = new JwtSecurityTokenHandler();
            var token = tokenManager.CreateToken(descriptionToken);

            return tokenManager.WriteToken(token);
        }
    }
}