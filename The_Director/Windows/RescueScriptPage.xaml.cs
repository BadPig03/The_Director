using System;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class RescueScriptPage : UserControl
    {

        private readonly UserControl StandardRescue = Activator.CreateInstance(Type.GetType($"The_Director.Windows.StandardRescueSettings")) as UserControl;
        private readonly UserControl ScavengeRescue = Activator.CreateInstance(Type.GetType($"The_Director.Windows.ScavengeRescueSettings")) as UserControl;
        private readonly UserControl GauntletRescue = Activator.CreateInstance(Type.GetType($"The_Director.Windows.GauntletRescueSettings")) as UserControl;
        private readonly UserControl SacrificeRescue = Activator.CreateInstance(Type.GetType($"The_Director.Windows.SacrificeRescueSettings")) as UserControl;

        public RescueScriptPage()
        {
            InitializeComponent();
            RescueTypeComboBox.ItemsSource = Globals.RescueTypeList;
            RescueTypeComboBox.SelectedIndex = 0;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RescueTypeComboBox.SelectedIndex == 0)
            {
                RescueTypeControl.Content = StandardRescue;
            }
            else if (RescueTypeComboBox.SelectedIndex == 1)
            {
                RescueTypeControl.Content = ScavengeRescue;
            }
            else if (RescueTypeComboBox.SelectedIndex == 2)
            {
                RescueTypeControl.Content = GauntletRescue;
            }
            else if (RescueTypeComboBox.SelectedIndex == 3)
            {
                RescueTypeControl.Content = SacrificeRescue;
            }
        }
    }
}
