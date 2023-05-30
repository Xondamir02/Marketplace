using System.ComponentModel.DataAnnotations;

namespace IdentityBase.Models;

public class CreatedUserModel
{
    public required string Name { get; set; }
    public required string Password { get; set; }
    [Compare(nameof(Password))]
    public required string ConfirmPassword { get; set; }
    public required string UserName { get; set; }
}