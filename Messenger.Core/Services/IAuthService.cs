using Messenger.Core.Schemas;

namespace Messenger.Core.Services;

public interface IAuthService
{
    public Task RegisterUser(RegisterUserSchema createUserData);

    public Task<string> LoginUser(LoginUserSchema userData);
}
