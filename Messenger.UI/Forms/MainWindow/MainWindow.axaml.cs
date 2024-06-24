using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Input;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Messenger.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {   
        InitializeComponent();
    }


    private void Panel_Click(object sender, PointerPressedEventArgs e)
    {
        MessageBoxManager.GetMessageBoxStandard("лалала", "лалала", ButtonEnum.Ok).ShowWindowAsync();
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            TextBox textBox = (TextBox)sender;
            string? message = textBox.Text;

            textBox.Text = string.Empty;
        }
    }

}

