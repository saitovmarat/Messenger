using Messenger.Core.DataBase;
using Messenger.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Core.Repositories;

public class AuthRepository : IAuthRepository
{
    DataBaseContext _context;

    public AuthRepository(DataBaseContext context)
    {
        _context = context;
    }

    public async Task Register(UserModel user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserModel?> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}