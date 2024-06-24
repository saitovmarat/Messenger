using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;

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
            PasswordEye.Source = new Bitmap("Assets/Images/OpenEye.png");
        }
        else{
            PasswordTextBox.PasswordChar = '*';
            PasswordEye.Source = new Bitmap("Assets/Images/ClosedEye.png");
        }
    }

    private async void EntryButton_Click(object sender, RoutedEventArgs e)
    {
        using (HttpClient client = new HttpClient())
        {
            var data = new Dictionary<string, string?>
            {
                { "userName", UserNameTextBox.Text },
                { "password", PasswordTextBox.Text }
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("http://localhost:5243/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                new MainWindow().Show();
                Close();
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка отправки запроса: {response.StatusCode}", ButtonEnum.Ok).ShowWindowAsync();
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
