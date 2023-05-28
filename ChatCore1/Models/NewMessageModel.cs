using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCore1.Models;
public class NewMessageModel
{
    public Guid ToUserId { get; set; }

    public required string Text { get; set; }
}