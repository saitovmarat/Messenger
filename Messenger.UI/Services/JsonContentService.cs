using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Messenger.UI.Services;

public static class JsonContentService
{
    public static StringContent GetRegistrationContent(
        string? userName, string? email, string? password, string? repeatPassword)
    {
        var data = new Dictionary<string, string?>
        {
            { "userName", userName },
            { "email", email },
            { "password", password },
            { "repeatPassword", repeatPassword }
        };

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        return content;
    }

    public static StringContent GetLoginContent(
        string? userName, string? password)
    {
        var data = new Dictionary<string, string?>
        {
            { "userName", userName },
            { "password", password }
        };

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        return content;
    }
}