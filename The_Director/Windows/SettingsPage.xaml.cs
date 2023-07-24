using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class SettingsPage : UserControl
    {
        public SettingsPage()
        {
            InitializeComponent();
            LanguageComboBox.ItemsSource = Globals.LanguageList;
            LanguageComboBox.SelectedIndex = 0;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
