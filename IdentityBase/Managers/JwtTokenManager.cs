﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityBase.Entities;
using IdentityBase.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IdentityBase.Managers
{
    public class JwtTokenManager
    {
        public readonly JwtOptions _jwtOption;

        public JwtTokenManager(IOptions<JwtOptions> jwtOption)
        {
            _jwtOption = jwtOption.Value;
        }

        public string GenerteToken(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var signingKey = System.Text.Encoding.UTF32.GetBytes(_jwtOption.SigningKey);
            var security = new JwtSecurityToken(
                issuer: _jwtOption.ValidIssuer,
                audience: _jwtOption.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtOption.ExpiresInMinutes),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(security);

            return token;

        }
    }
}