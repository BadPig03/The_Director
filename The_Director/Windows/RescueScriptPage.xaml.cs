using System.Windows.Controls;
using The_Director.Utils;
using System.Collections.Generic;
using System.Windows;

namespace The_Director.Windows
{
    public partial class RescueScriptPage : UserControl
    {
        public Dictionary<string, BooleanString> RescueCheckButtons = new();

        public void MSGReceived(string value)
        {
            RescueCheckButtons["msg"] = (true, value);
        }

        public RescueScriptPage()
        {
            InitializeComponent();

            RescueCheckButtons.Add("msg", new BooleanString(false, ""));
            RescueCheckButtons.Add("prohibitboss", new BooleanString(false, null));

        }

        private void TotalWaveClick(object sender, RoutedEventArgs e) 
        {
            if(Functions.IsPositiveNumber(TotalWaveTextbox.Text))
            {
                TryOpenTotalWaveWindow();
                Functions.ConvertToInt(TotalWaveTextbox.Text);
            }
            else
            {
                MessageBox.Show("非法输入！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalWaveTextbox.Text = "";
            }

        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            if (!RescueCheckButtons["msg"].Item1 && (bool)MSGButton.IsChecked)
                TryOpenMSGWindow();

            RescueCheckButtons["msg"] = ((bool)MSGButton.IsChecked, RescueCheckButtons["msg"].Item2);
            RescueCheckButtons["prohibitboss"] = ((bool)ProhibitBossButton.IsChecked, null);

            UpdateScriptWindow();
        }

        private void TryOpenMSGWindow()
        {
            InputNewText InputWindow = new InputNewText
            {
                SendMessage = MSGReceived
            };
            if((bool)InputWindow.ShowDialog())
                UpdateScriptWindow();
        }

        private void TryOpenTotalWaveWindow()
        {
            TotalWaveSettings TotalWindow = new TotalWaveSettings
            {
                Width = 360,
                Height = 200
            };
            if((bool)TotalWindow.ShowDialog())
                UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            var ScriptWindowText = "";
            if(RescueCheckButtons["msg"].Item1)
                ScriptWindowText += $"Msg(\"{RescueCheckButtons["msg"].Item2}\");\n\n";

            ScriptWindowText += "PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\n\nDirectorOptions <-\n{\n";

            if (RescueCheckButtons["prohibitboss"].Item1)
                ScriptWindowText += "\rProhibitBosses = true\n";

            ScriptWindowText += $"\rA_CustomFinale_StageCount = 2\n";
            ScriptWindowText += "}\n";
            ScriptWindow.Text = ScriptWindowText;
        }
    }
}
