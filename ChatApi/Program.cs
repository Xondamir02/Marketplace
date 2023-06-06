using ChatApi.Extensions;
using ChatApi.Hubs;
using ChatCore1.Context;
using ChatCore1.Managers;
using IdentityBase.Context;
using IdentityBase.Extensions;
using IdentityBase.Middlewares;
using IdentityBase.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer { token }\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<ChatDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb"));
});

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("IdentityDb");
    options.UseNpgsql(connectionString);

    //options.UseInMemoryDatabase("Memory");
});

builder.Services.AddIdentity(builder.Configuration);

builder.Services.AddScoped<ConversationManager>();
builder.Services.AddScoped<UserProvider>();
builder.Services.AddSingleton(typeof(UserConnectionIdService));

//builder.Services.AddSingleton<UserConnectionIdService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(cors =>
{
    cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
});

app.MigrateChatDbContext();
app.MigrateIdentityDb();


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseErrorHandlerMiddleware();

app.MapHub<ConversationHub>("/hubs/conversation");

app.MapControllers();

app.Run();