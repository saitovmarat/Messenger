using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaUI;

public partial class SecondPart_PassRecoveryWindow : Window
{
    public SecondPart_PassRecoveryWindow()
    {
        InitializeComponent();
    }

    private void GoFurther_Click(object sender, RoutedEventArgs e)
    {
        new ThirdPart_PassRecoveryWindow().Show();
        Close();
    }
}