using Avalonia.Controls;
using Avalonia.Interactivity;
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

    private void GoFurther_Click(object sender, RoutedEventArgs e)
    {
        new ThirdPart_PassRecoveryWindow(Email).Show();
        Close();
    }
}