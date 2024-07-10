using System;
using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Messenger.UI.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
#pragma warning disable CS8604 

namespace Messenger.UI;

public partial class SecondPart_PassRecoveryWindow : Window
{
    private string? Email { get; set; }
    
    public SecondPart_PassRecoveryWindow(string email)
    {
        Email = email;
        InitializeComponent();
    }
    public SecondPart_PassRecoveryWindow() =>
        InitializeComponent();

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        new FirstPart_PassRecoveryWindow().Show();
        Close();
    }
    private void ShowNewPasswordButton_Click(object sender, RoutedEventArgs e){
        if (NewPasswordTextBox.PasswordChar == '*'){
            NewPasswordTextBox.PasswordChar = '\0';
            NewPasswordEye.Source = new Bitmap("Assets/OpenEye.png");
        }
        else{
            NewPasswordTextBox.PasswordChar = '*';
            NewPasswordEye.Source = new Bitmap("Assets/ClosedEye.png");
        }
    }
    private async void ChangePass_Click(object sender, RoutedEventArgs e)
    {
        HttpClient client = new HttpClient();
        var content = JsonContentService.GetChangePasswordContent(EmailCodeTextBox.Text, Email, NewPasswordTextBox.Text);

        HttpResponseMessage response = await client.PostAsync("http://localhost:5243/api/ChangePassword", content);
        Console.WriteLine(NewPasswordTextBox.Text);
        if (response.IsSuccessStatusCode){
            new EntryWindow().Show();
            Close();
        }
        else{
            await MessageBoxManager.GetMessageBoxStandard("Error", $"{await response.Content.ReadAsStringAsync()}", ButtonEnum.Ok).ShowWindowAsync();
        }
    }
}