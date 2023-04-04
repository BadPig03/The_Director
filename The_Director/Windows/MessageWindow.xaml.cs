using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace The_Director.Windows
{
    public partial class MessageWindow : MetroWindow
    {
        public string TextBoxString { get; set; }

        public MessageWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Text = TextBoxString;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space || e.Key == Key.Escape)
            {
                ConfirmButtonClick(null, null);
            }
        }
    }
}
