using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using MahApps.Metro.Controls;
using System.Windows.Input;

namespace The_Director.Windows
{
    public partial class HintWindow : MetroWindow
    {
        public string TextBoxString { get; set; }
        public string HyperlinkUri { get; set; }

        public HintWindow()
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
            HintTextBox.Text = TextBoxString;
            HyperlinkLabel.NavigateUri = new Uri(HyperlinkUri);
        }

        private void HyperlinkClick(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
                ConfirmButtonClick(null, null);
        }
    }
}
