using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Messenger.UI.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace Messenger.UI.ViewModels.SplitViewPane;

public partial class AddChatPageViewModel : ViewModelBase 
{
    [ObservableProperty]
    private string? _chatNameTextBox;

    [RelayCommand]
    private void CreateChat()
    {

    }
    // private async Task CreateChatAsync()
    // {
    //     HttpClient client = new HttpClient();
    //     client.DefaultRequestHeaders.Authorization =
    //         new AuthenticationHeaderValue("Bearer", Program.accessToken);
    //     var content = JsonContentService.GetAddChatContent(ChatNameTextBox);
    //     var response = await client.PostAsync("http://localhost:5243/api/Chats", content);
    //     if(response.IsSuccessStatusCode){
    //         MainWindowViewModel.Items.Add(new ListItemTemplate(new ChatPageViewModel(), ChatNameTextBox, "PeopleRegular"));
    //     }
    //     else{
    //         dynamic responseJson = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
    //         await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка получения чатов: {responseJson.errors}", ButtonEnum.Ok).ShowWindowAsync();

    //     }
    // }
}
