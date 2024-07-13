using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messenger.UI.Schemas;
using Messenger.UI.ViewModels.SplitViewPane;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace Messenger.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        InitializeAsync();
    }

    #region LoadingAllChats
    private async void InitializeAsync()
    {
        await LoadChatsAsync();
    }

    private async Task LoadChatsAsync()
    {
        List<ChatInfoSchema> chats = await GetChats();
        foreach (var chat in chats)
        {
            Items.Add(new ListItemTemplate(new ChatPageViewModel(chat.Id), chat.ChatName, "PeopleRegular"));
        }
    }
    private async Task<List<ChatInfoSchema>?> GetChats()
    {
        HttpClient client = new();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", Program.accessToken);

        var response = await client.GetAsync("http://localhost:5243/api/Chats");
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


    [ObservableProperty]
    private ViewModelBase? _currentPage = null;

    [ObservableProperty]
    private ListItemTemplate? _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;

        var viewModel = value.ViewModel;

        if (viewModel is ChatPageViewModel chatPageViewModel)
            CurrentPage = chatPageViewModel;

        else if (viewModel is SearchChatsPageViewModel searchPageViewModel)
            CurrentPage = searchPageViewModel;

        else
        {
            var viewModelInstance = Activator.CreateInstance(viewModel.GetType());
            if (viewModelInstance is null) return;
            CurrentPage = (ViewModelBase)viewModelInstance;
        }
    }

    public static ObservableCollection<ListItemTemplate> Items { get; } = [];

    #region OnViewsClick
    [ObservableProperty]
    private string? _searchTextBoxText;
    
    [RelayCommand]
    private void SearchChat()
    {
        if(string.IsNullOrWhiteSpace(SearchTextBoxText)) 
        {
            MessageBoxManager.GetMessageBoxStandard("Error", "Поле поиска пустое", ButtonEnum.Ok).ShowWindowAsync();
            return;
        }
        var instance = Activator.CreateInstance(typeof(SearchChatsPageViewModel), SearchTextBoxText); 
        if(instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }

    [RelayCommand]
    private void AddChat()
    {
        var instance = Activator.CreateInstance(typeof(AddChatPageViewModel)); 
        if(instance is null) return;
        CurrentPage = (ViewModelBase)instance;
    }
    #endregion
}

public class ListItemTemplate
{
    public ListItemTemplate(ViewModelBase viewModel, string chatName, string iconKey)
    {
        ViewModel = viewModel;
        Label = chatName;
        
        Application.Current!.TryFindResource(iconKey, out var res);
        ListItemIcon = (StreamGeometry)res!;
    }
    public ViewModelBase ViewModel { get; }
    public string Label { get; }
    public StreamGeometry ListItemIcon { get; }
}