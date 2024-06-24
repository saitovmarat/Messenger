using Messenger.Core.Models;

namespace Messenger.Core.Utils;

public interface IJwtProvider
{
    public string GenerateToken(UserModel user);
}