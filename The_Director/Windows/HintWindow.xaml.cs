using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using MahApps.Metro.Controls;

namespace The_Director.Windows
{
    public partial class HintWindow : MetroWindow
    {
        public string TextBlockString { get; set; }
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
            HintTextBlock.Text = TextBlockString;
            HyperlinkLabel.NavigateUri = new Uri(HyperlinkUri);
        }

        private void HyperlinkClick(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
    }
}
