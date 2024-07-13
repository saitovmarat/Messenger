using System;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Messenger.UI.Services;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

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
        HttpClient client = new();
        var content = JsonContentService.GetChangePasswordContent(EmailCodeTextBox.Text, Email, NewPasswordTextBox.Text);

        HttpResponseMessage response = await client.PostAsync("http://localhost:5243/api/ChangePassword", content);
        if (response.IsSuccessStatusCode){
            new EntryWindow().Show();
            Close();
        }
        else{
            string errorMessage = await GetErrorMessage(response);
            await MessageBoxManager.GetMessageBoxStandard("Error", errorMessage, ButtonEnum.Ok).ShowWindowAsync();
        }
    }

    private static async Task<string> GetErrorMessage(HttpResponseMessage response)
    {
        string errorResponse = await response.Content.ReadAsStringAsync();
        string errorMessage = "";

        try
        {
            JObject? errorObject = JObject.Parse(errorResponse);

            if (errorObject.ContainsKey("errors"))
            {
                JObject errors = (JObject)errorObject["errors"];

                if (errors.ContainsKey("Email"))
                {
                    errorMessage += "• " + errors["Email"][0].ToString() + "\n";
                }

                if (errors.ContainsKey("ResetCode"))
                {
                    errorMessage += "• " + errors["ResetCode"][0].ToString() + "\n";
                }

                if (errors.ContainsKey("NewPassword"))
                {
                    errorMessage += "• " + errors["NewPassword"][0].ToString();
                }
            }
            else
            {
                errorMessage = errorObject["title"].ToString();
            }
        }
        catch (JsonReaderException)
        {
            errorMessage = errorResponse;
        }

        return errorMessage;
    }
}