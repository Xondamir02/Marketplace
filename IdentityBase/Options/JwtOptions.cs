using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityBase.Options
{
    public class JwtOptions
    {
        public required string SigningKey { get; set; }
        public required string ValidAudience { get; set; }
        public required string ValidIssuer { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
