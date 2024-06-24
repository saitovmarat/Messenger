using Messenger.Core.Models;

namespace Messenger.Core.Repositories;

public interface IAuthRepository
{
    public Task Register(UserModel user);

    public Task<UserModel?> GetUserByUsername(string username);
}
