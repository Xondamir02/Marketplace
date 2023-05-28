using ChatCore1.Context;
using Microsoft.EntityFrameworkCore;

namespace ChatApi.Extensions;

public static class WebApplicationExtensions
{
    public static void MigrateChatDbContext(this WebApplication app)
    {
        if (app.Services.GetService<ChatDbContext>() != null)
        {
            var chatDb = app.Services.GetRequiredService<ChatDbContext>();
            chatDb.Database.Migrate();
        }
    }
}