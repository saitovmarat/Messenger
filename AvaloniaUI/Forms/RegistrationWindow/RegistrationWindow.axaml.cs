using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI;

public partial class RegistrationWindow : Window
{
    public RegistrationWindow()
    {
        InitializeComponent();
    }
    private void RegistrationButton_Click(object sender, RoutedEventArgs e){
        new EntryWindow().Show();
        Close();
    }
}