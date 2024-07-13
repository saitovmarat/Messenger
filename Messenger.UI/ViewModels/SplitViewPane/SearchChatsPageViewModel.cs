using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls;
using System.Threading.Tasks;
using System.Collections.Generic;
using Messenger.UI.Schemas;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using CommunityToolkit.Mvvm.Input;

namespace Messenger.UI.ViewModels.SplitViewPane;

public partial class SearchChatsPageViewModel : ViewModelBase 
{   
    public string? SearchText { get; set; }

    public SearchChatsPageViewModel(string searchText)
    {
        SearchText = searchText;
        InitializeAsync();
    }

    #region LoadingAllChats
    private async void InitializeAsync()
    {
        await LoadChatsAsync();
    }

    private async Task LoadChatsAsync()
    {
        List<ChatInfoSchema>? chats = await GetAllChatsByName();
        foreach (var chat in chats)
        {
            Items.Add(new ChatItemTemplate(chat.Id, chat.ChatName, chat.LastMessage?.Text));
        }
    }
    private async Task<List<ChatInfoSchema>?> GetAllChatsByName()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", Program.accessToken);

        var response = await client.GetAsync($"http://localhost:5243/api/Chats/All?chatName={SearchText}");
        if(response.IsSuccessStatusCode){
            var responseContent = await response.Content.ReadAsStringAsync();
            var chatList = JsonConvert.DeserializeObject<List<ChatInfoSchema>>(responseContent);
            return chatList;
        }
        else{
            await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка получения чатов: {await response.Content.ReadAsStringAsync()}", ButtonEnum.Ok).ShowWindowAsync();
            return [];
        }
    } 
    #endregion
    
    
    public ObservableCollection<ChatItemTemplate> Items { get; } = [];
}

public partial class ChatItemTemplate
{
    public ChatItemTemplate(string? id, string? chatName, string? lastMessage)
    {
        Id = id;
        ChatName = chatName;
        LastMessage = lastMessage;
        Application.Current!.TryFindResource("PeopleRegular", out var res);
        ChatItemIcon = (StreamGeometry)res!;
    }

    public string? Id { get; }
    public string? ChatName { get; }
    public string? LastMessage { get; }
    public StreamGeometry ChatItemIcon { get; }

    [RelayCommand]
    public async Task JoinChat()
    {
        bool responseResult = await AddToChat();
        if(responseResult)
        {
            var chatToAdd = new ListItemTemplate(new ChatPageViewModel(Id), ChatName, "PeopleRegular");
            MainWindowViewModel.Items.Add(chatToAdd);
        }
    }

    private async Task<bool> AddToChat()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", Program.accessToken);

        var response = await client.GetAsync($"http://localhost:5243/api/Chats/Add?chatId={Id}");
        if(response.IsSuccessStatusCode){
            await MessageBoxManager.GetMessageBoxStandard("Success", $"Чат добавлен", ButtonEnum.Ok).ShowWindowAsync();
            return true;
        }
        else{
            await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка получения чатов: {await response.Content.ReadAsStringAsync()}", ButtonEnum.Ok).ShowWindowAsync();
            return false;
        }
    }
    
}