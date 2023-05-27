using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityBase.Entities
{
    public  class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public required string UserName { get; set; }
        public string? PasswordHash { get; set; } = null!;
    }
}
