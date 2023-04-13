using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Input;

namespace The_Director.Windows
{
    public partial class YesOrNoWindow : MetroWindow
    {
        public string TextBlockString { get; set; }

        public delegate void _SendMessage(string value);
        public _SendMessage SendMessage;

        private bool cancelClosing = true;

        public YesOrNoWindow()
        {
            InitializeComponent();
        }

        private void YesButtonClick(object sender, RoutedEventArgs e)
        {
            SendMessage("Confirmed");
            cancelClosing = false;
            DialogResult = true;
            Close();
        }

        private void NoButtonClick(object sender, RoutedEventArgs e)
        {
            SendMessage(null);
            cancelClosing = false;
            DialogResult = true;
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Text = TextBlockString;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                YesButtonClick(null, null);
            }

            if (e.Key == Key.Escape)
            {
                NoButtonClick(null, null);
            }
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = cancelClosing;
        }
    }
}
