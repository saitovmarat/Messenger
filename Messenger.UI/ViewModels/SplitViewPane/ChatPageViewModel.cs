using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messenger.UI.Schemas;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;
using Avalonia.Controls;

#pragma warning disable CS8604 // Possible null reference argument.

namespace Messenger.UI.ViewModels.SplitViewPane;

public partial class ChatPageViewModel : ViewModelBase 
{
    public string Id { get; set; }
    private readonly HubConnection connection;
    public ChatPageViewModel(string id)
    {
        Id = id;
        connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5243/api/ChatMessages", options =>
            { 
                options.AccessTokenProvider = () => Task.FromResult(Program.accessToken);
            })
            .WithAutomaticReconnect()
            .Build();
        LoadAllMessages();
    }

    #region LoadAllMessages
    public async void LoadAllMessages()
    {
        List<MessageSchema>? messages = await GetAllMessages();
        for(int i = 0; i < messages?.Count; i++)
        {
            Items.Add(new MessageItemTemplate(messages[i].Sender?.UserName, messages[i].MessageSentTime.ToString(), messages[i].Text));
        }

        connection.On<MessageSchema>("ReceiveMessage", (message) =>
        {
            Items.Add(new MessageItemTemplate(message.Sender?.UserName, message.MessageSentTime.ToString(), message.Text));
        });

        await connection.StartAsync();
        await connection.InvokeAsync("JoinChat", new JoinChatScheme(Program.userName, Id));
    }

    private async Task<List<MessageSchema>?> GetAllMessages()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", Program.accessToken);
        if(!Guid.TryParse(Id, out Guid chatId))
        {
            await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка перевода строки в Guid", ButtonEnum.Ok).ShowWindowAsync();
            return [];
        }
        var response = await client.GetAsync($"http://localhost:5243/api/Messages?chatId={chatId}");
        if(response.IsSuccessStatusCode){
            var responseContent = await response.Content.ReadAsStringAsync();
            var messagesList = JsonConvert.DeserializeObject<List<MessageSchema>>(responseContent);
            return messagesList;
        }
        else{
            await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка получения сообщений: {await response.Content.ReadAsStringAsync()}", ButtonEnum.Ok).ShowWindowAsync();
            return [];
        }
    }
    #endregion
    
    [RelayCommand]
    private async Task SendMessage()
    {
        await connection.InvokeAsync("SendMessage", new SendMessageToChatSchema(Id, Program.userName, MessageTextBoxText));
        MessageTextBoxText = "";
    } 

    [ObservableProperty]
    private string? _messageTextBoxText;
    public ObservableCollection<MessageItemTemplate> Items { get; } = [];
}
public class MessageItemTemplate
{
    public MessageItemTemplate(string? userName, string? date, string? message)
    {
        Alignment = (Program.userName == userName)?
            HorizontalAlignment.Right : HorizontalAlignment.Left; 

        UserName = userName;
        Date = date;
        Message = message;
    }
    public HorizontalAlignment Alignment { get; }
    public string? UserName { get; }
    public string? Date { get; }
    public string? Message { get; }
    
}
