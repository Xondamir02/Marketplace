using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatCore1.Context
{
    public class ChatDbContext: DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {

        }

        public DbSet<Conversation> Conversations => Set<Conversation>();
        public DbSet<Message> Messages => Set<Message>();
    }
}
