using System.Windows.Controls;
using System.Collections.Generic;
using System;

namespace The_Director.Windows
{
    public partial class RescueScriptPage : UserControl
    {
        public List<string> RescueTypeList = new() { "防守救援", "灌油救援", "跑图救援", "牺牲救援", "尸潮脚本"};

        private UserControl StandardRescue = Activator.CreateInstance(Type.GetType($"The_Director.Windows.StandardRescueSettings")) as UserControl;
        private UserControl GauntletRescue = Activator.CreateInstance(Type.GetType($"The_Director.Windows.GauntletRescueSettings")) as UserControl;

        public RescueScriptPage()
        {
            InitializeComponent();

            RescueTypeComboBox.ItemsSource = RescueTypeList;
            RescueTypeComboBox.SelectedIndex = 0;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RescueTypeComboBox.SelectedIndex == 0)
                RescueTypeControl.Content = StandardRescue;
            else if (RescueTypeComboBox.SelectedIndex == 1)
                RescueTypeControl.Content = GauntletRescue;
        }
    }
}
