using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Messenger.UI.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
#pragma warning disable CS8604

namespace Messenger.UI;

public partial class FirstPart_PassRecoveryWindow : Window
{
    public FirstPart_PassRecoveryWindow()
    {
        InitializeComponent();
    }

    private async void GetCodeButton_Click(object sender, RoutedEventArgs e)
    {
        using (HttpClient client = new HttpClient())
        {
            var content = JsonContentService.GetSendEmailCodeContent(EmailTextBox.Text);

            HttpResponseMessage response = await client.PostAsync("http://localhost:5243/api/SendEmailCode", content);

            if (response.IsSuccessStatusCode){
                new SecondPart_PassRecoveryWindow(EmailTextBox.Text).Show();
                Close();
            }
            else{
                await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка отправки запроса: {response.StatusCode}", ButtonEnum.Ok).ShowWindowAsync();
            }
        }
        
    }
}