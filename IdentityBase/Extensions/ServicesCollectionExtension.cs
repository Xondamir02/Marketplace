using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityBase.Managers;
using IdentityBase.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using IdentityBase.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace IdentityBase.Extensions;

public static class ServicesCollectionExtension
{
    private static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(nameof(JwtOptions));
        services.Configure<JwtOptions>(section);
        var jwtOptions = section.Get<JwtOptions>()!;

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            var signingKey = System.Text.Encoding.UTF32.GetBytes(jwtOptions.SigningKey);

            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtOptions.ValidIssuer,
                ValidAudience = jwtOptions.ValidAudience,
                ValidateIssuer = true,
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
    public static void AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwt(configuration);
        services.AddScoped<JwtTokenManager>();
        services.AddScoped<UserManager>();
    }

    public static void MigrateIdentityDb(this WebApplication app)
    {
        if (app.Services.GetService<IdentityDbContext>() != null)
        {
            var identityDb = app.Services.GetRequiredService<IdentityDbContext>();
            identityDb.Database.Migrate();
        }
    }
}