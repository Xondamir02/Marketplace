using IdentityBase.Context;
using IdentityBase.Entities;
using IdentityBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityBase.Managers;

public class UserManager
{
    private readonly IdentityDbContext _dbContext;
    private ILogger<UserManager> _logger;
    private readonly JwtTokenManager _tokenManager;

    public UserManager(JwtTokenManager tokenManager, ILogger<UserManager> logger, IdentityDbContext dbContext)
    {
        _tokenManager = tokenManager;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<User> Register(CreatedUserModel createdUserModel)
    {
        var res = await _dbContext.Users.AnyAsync(u => u.UserName == createdUserModel.UserName);
        if (res)
        {
            throw new Exception("Username already taken 😒");
        }

        var user = new User()
        {
            UserName = createdUserModel.UserName,
            Name = createdUserModel.Name
        };
        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, createdUserModel.Password);
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;

    }

    public async Task<string> Login(LoginUserModel loginUserModel)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == loginUserModel.UserName);
        if (user == null)
        {
            throw new Exception("UserName or Password is incorrect");
        }
        var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, loginUserModel.Password);

        if (result != PasswordVerificationResult.Success)
        {
            throw new Exception("UserName or Password is incorrect");
        }

        var token = _tokenManager.GenerteToken(user);

        return token;
    }

    public async Task<User?> GetUser(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User?> GetUser(string userName)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

}