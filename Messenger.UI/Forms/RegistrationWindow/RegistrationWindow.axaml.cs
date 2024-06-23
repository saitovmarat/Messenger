using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
#pragma warning disable CS8604

namespace Messenger.UI;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent();
    }
    private void ShowNewPasswordButton_Click(object sender, RoutedEventArgs e){
        if (PasswordTextBox.PasswordChar == '*'){
            PasswordTextBox.PasswordChar = '\0';
            NewPasswordEye.Source = new Bitmap("Assets/Images/OpenEye.png");
        }
        else{
            PasswordTextBox.PasswordChar = '*';
            NewPasswordEye.Source = new Bitmap("Assets/Images/ClosedEye.png");
        }
    }

    private void ShowRepeatPasswordButton_Click(object sender, RoutedEventArgs e){
        if (RepeatPasswordTextBox.PasswordChar == '*'){
            RepeatPasswordTextBox.PasswordChar = '\0';
            RepeatPasswordEye.Source = new Bitmap("Assets/Images/OpenEye.png");
        }
        else{
            RepeatPasswordTextBox.PasswordChar = '*';
            RepeatPasswordEye.Source = new Bitmap("Assets/Images/ClosedEye.png");
        }
    }
    private async void Register_Click(object sender, RoutedEventArgs e)
    {
        using (HttpClient client = new HttpClient())
        {
            var data = new Dictionary<string, string>
            {
                { "userName", UserNameTextBox.Text },
                { "email", EmailTextBox.Text },
                { "password", PasswordTextBox.Text },
                { "repeatPassword", RepeatPasswordTextBox.Text }
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("http://localhost:5243/api/Register", content);

            if (response.IsSuccessStatusCode)
            {
                new EntryWindow().Show();
                Close();
            }
            else
            {
                await MessageBoxManager.GetMessageBoxStandard("Error", $"Ошибка отправки запроса: {response.StatusCode}", ButtonEnum.Ok).ShowWindowAsync();
            }
        }
    }
}