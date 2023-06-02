using IdentityBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityBase.Context;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}