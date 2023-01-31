using System.Windows.Controls;
using The_Director.Utils;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System;

namespace The_Director.Windows
{
    public partial class RescueScriptPage : UserControl
    {
        public List<string> RescueTypeList = new() { "防守救援", "灌油救援", "跑图救援", "牺牲救援"};

        public int TotalWaves = new();
        public bool IsTotalWaveConfirmed = new();

        public Dictionary<string, BooleanString> RescueCheckButtons = new();
        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, string> TotalWaveDicts = new();

        public void MSGReceived(string value)
        {
            if(value != null)
                RescueCheckButtons["msg"] = (true, value);
            else
                MSGButton.IsChecked = false;
        }
        public void TotalWaveReceived(string value)
        {
            if (value == null)
                IsTotalWaveConfirmed = false;
            else
            {
                TotalWaveDicts.Clear();
                IsTotalWaveConfirmed = true;
            }
        }

        public RescueScriptPage()
        {
            InitializeComponent();

            RescueTypeComboBox.ItemsSource = RescueTypeList;
            RescueTypeComboBox.SelectedIndex = 0;

            RescueCheckButtons.Add("msg", new BooleanString(false, ""));
            RescueCheckButtons.Add("prohibitboss", new BooleanString(false, null));
            RescueCheckButtons.Add("showstage", new BooleanString(false, null));
            RescueCheckButtons.Add("locktempo", new BooleanString(false, null));

        }

        private void TotalWaveClick(object sender, RoutedEventArgs e) 
        {
            if(Functions.IsPositiveNumber(TotalWaveTextbox.Text))
            {
                TotalWaves = Functions.ConvertToInt(TotalWaveTextbox.Text);
                TryOpenTotalWaveWindow(TotalWaves);
            }
            else
            {
                MessageBox.Show("非法输入！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalWaveTextbox.Text = "3";
            }

        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            if (!RescueCheckButtons["msg"].Item1 && (bool)MSGButton.IsChecked)
                TryOpenMSGWindow();

            RescueCheckButtons["msg"] = ((bool)MSGButton.IsChecked, RescueCheckButtons["msg"].Item2);
            RescueCheckButtons["prohibitboss"] = ((bool)ProhibitBossButton.IsChecked, null);
            RescueCheckButtons["showstage"] = ((bool)ShowStageButton.IsChecked, null);
            RescueCheckButtons["locktempo"] = ((bool)LockTempoButton.IsChecked, null);

            UpdateScriptWindow();
        }

        private void TryOpenMSGWindow()
        {
            InputNewText InputWindow = new InputNewText
            {
                TextBoxText = RescueCheckButtons["msg"].Item2,
                SendMessage = MSGReceived
            };
            InputWindow.ShowDialog();
            UpdateScriptWindow();
        }

        private void TryOpenTotalWaveWindow(int WaveCount)
        {
            TextBoxDicts.Clear();
            ComboBoxDicts.Clear();

            TotalWaveSettings TotalWindow = new TotalWaveSettings
            {
                WaveCounts = WaveCount,
                TotalWaveDicts = TotalWaveDicts,
                SendMessage = TotalWaveReceived
            };

            TotalWindow.ShowDialog();

            if (!IsTotalWaveConfirmed)
                return;

            foreach (var item in TotalWindow.TotalWaveGrid.Children)
            {
                if(item is TextBox)
                {
                    TextBox textBox = (TextBox)item;
                    TextBoxDicts[Functions.ConvertToInt(textBox.Name.Remove(0, 1))] = textBox.Text;
                }
                if(item is ComboBox)
                { 
                    ComboBox comboBox= (ComboBox)item;
                    ComboBoxDicts[Functions.ConvertToInt(comboBox.Name.Remove(0, 1))] = comboBox.SelectedIndex;
                }
            }

            foreach (var item1 in ComboBoxDicts)
                foreach (var item2 in TextBoxDicts)
                    if(item1.Key == item2.Key)
                        TotalWaveDicts[$"{item1.Key+1}"] = $"{Functions.TotalWaveToString(item1.Value)}\x1b{item2.Value}";            

            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            var ScriptWindowText = "";
            if(RescueCheckButtons["msg"].Item1)
                ScriptWindowText += $"Msg(\"{RescueCheckButtons["msg"].Item2}\");\n\n";

            ScriptWindowText += "PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\n\nDirectorOptions <-\n{\n";

            if (RescueCheckButtons["prohibitboss"].Item1)
                ScriptWindowText += "\tProhibitBosses = true\n\n";

            ScriptWindowText += $"\tA_CustomFinale_StageCount = {TotalWaves}\n\n";

            foreach (var item in TotalWaveDicts)
            {
                ScriptWindowText += $"\tA_CustomFinale{item.Key} = {item.Value.Split('\x1b')[0]}\n";
                ScriptWindowText += $"\tA_CustomFinaleValue{item.Key} = {item.Value.Split('\x1b')[1]}\n";
            }

            if (RescueCheckButtons["locktempo"].Item1)
                ScriptWindowText += "\n\tLockTempo = true;\n\n";

            ScriptWindowText += "}\n";
            
            if (RescueCheckButtons["showstage"].Item1)
                ScriptWindowText += "\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \"+ type);\n}\n";

            ScriptWindow.Text = ScriptWindowText;
        }
    }
}
