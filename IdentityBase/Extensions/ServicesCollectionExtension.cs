﻿using IdentityBase.Context;
using IdentityBase.Managers;
using IdentityBase.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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
            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = async context =>
                {
                    var accessToken = context.Request.Query["token"];
                    context.Token = accessToken;

                    //if (string.IsNullOrEmpty(context.Token))
                    //{
                    //    var accessToken = context.Request.Query["access_token"];
                    //    var path = context.HttpContext.Request.Path;
                    //    if (string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                    //    {
                    //        context.Token=accessToken;
                    //    }
                    //}
                }
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