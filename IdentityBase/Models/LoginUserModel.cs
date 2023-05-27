using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityBase.Models;

public class LoginUserModel
{
    public required string Password { get; set; }
    public required string UserName { get; set; }
}