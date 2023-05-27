using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityBase.Context;

public class IdentityDbContext : DbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users => Set<User>();
}