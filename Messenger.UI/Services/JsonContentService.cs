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

    public static StringContent GetSendEmailCodeContent(string? email)
    {
        var data = new Dictionary<string, string?>
        {
            { "email", email }
        };

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        return content;
    }

    public static StringContent GetChangePasswordContent(string? resetCode, string? email, string? newPassword)
    {
        var data = new Dictionary<string, string?>
        {
            {"resetCode", resetCode },
            { "email", email },
            { "newPassword", newPassword }
        };

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        return content;
    }

    public static StringContent GetAddChatContent(string? chatName)
    {
        var data = new Dictionary<string, string?>
        {
            { "chatName", chatName }
        };

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        return content;
    }

    public static StringContent GetChatIdContent(string? Id)
    {
        var data = new Dictionary<string, string?>
        {
            { "chatId", Id }
        };

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        return content;
    }

}