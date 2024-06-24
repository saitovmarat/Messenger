﻿namespace Messenger.Core.Options;

public class JwtOptions
{
    public string? SecretKey { get; set; }

    public int ExpiresHours{ get; set; }
}
