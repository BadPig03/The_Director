using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;

namespace The_Director.Windows
{
    public partial class InputNewText : MetroWindow
    {
        public delegate void _SendMessage(string value);
        public _SendMessage SendMessage;

        public string TextBoxText { get; set; }
        public InputNewText()
        {
            InitializeComponent();
        }
        
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            SendMessage(MSGTextBox.Text);
            DialogResult = true;
            Close();
        }

        private void CancleButtonClick(object sender, RoutedEventArgs e)
        {
            SendMessage(null);
            DialogResult = true;
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MSGTextBox.Text = TextBoxText;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ConfirmButtonClick(null, null);
            else if (e.Key == Key.Escape)
                CancleButtonClick(null, null);
        }
    }
}
