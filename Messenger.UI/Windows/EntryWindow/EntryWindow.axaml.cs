using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Messenger.UI.Services;
using Messenger.UI.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace Messenger.UI;

public partial class EntryWindow : Window
{

    public EntryWindow()
    {
        InitializeComponent();
    }
    private void ShowPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        if (PasswordTextBox.PasswordChar == '*'){
            PasswordTextBox.PasswordChar = '\0';
            PasswordEye.Source = new Bitmap("Assets/OpenEye.png");
        }
        else{
            PasswordTextBox.PasswordChar = '*';
            PasswordEye.Source = new Bitmap("Assets/ClosedEye.png");
        }
    }

    private async void EntryButton_Click(object sender, RoutedEventArgs e)
    {
        using (HttpClient client = new HttpClient())
        {
            
            var content = JsonContentService.GetLoginContent(
                UserNameTextBox.Text, PasswordTextBox.Text);
                
            HttpResponseMessage response = await client.PostAsync("http://localhost:5243/api/Login", content);

            if (response.IsSuccessStatusCode)
            {   
                string responseContent = await response.Content.ReadAsStringAsync();
                var tokenObject = JsonConvert.DeserializeObject<JObject>(responseContent);
                Program.accessToken = tokenObject["accessToken"].ToString();
                Program.userName = UserNameTextBox.Text;
                new MainWindow()
                {
                    DataContext = new MainWindowViewModel()
                }.Show();
                Close();
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Error", $"{await response.Content.ReadAsStringAsync()}", ButtonEnum.Ok).ShowWindowAsync();
            }
        }
    }

    private void ForgotPassword_Click(object sender, RoutedEventArgs e)
    {
        new FirstPart_PassRecoveryWindow().Show();
        Close();
    }

    private void RegistrationButton_Click(object sender, RoutedEventArgs e)
    {
        new RegistrationWindow().Show();
        Close();
    }
    
}
