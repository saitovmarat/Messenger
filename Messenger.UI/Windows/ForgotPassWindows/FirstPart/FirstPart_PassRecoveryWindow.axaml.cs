using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Messenger.UI.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
namespace Messenger.UI;

public partial class FirstPart_PassRecoveryWindow : Window
{
    public FirstPart_PassRecoveryWindow()
    {
        InitializeComponent();
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        new EntryWindow().Show();
        Close();
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
                string errorResponse = await response.Content.ReadAsStringAsync();
                string errorMessage = "";
                try
                {
                    JObject errorObject = JObject.Parse(errorResponse);
                    errorMessage = errorObject["errors"]["Email"][0].ToString();
                }
                catch (JsonReaderException)
                {
                    errorMessage = errorResponse;
                }
                await MessageBoxManager.GetMessageBoxStandard("Error", errorMessage, ButtonEnum.Ok).ShowWindowAsync();
            }
        }
    }
}