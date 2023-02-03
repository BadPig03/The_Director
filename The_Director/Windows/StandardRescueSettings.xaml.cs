using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class StandardRescueSettings : UserControl
    {
        public int TotalWaves = new();
        public bool IsTotalWaveConfirmed = new();

        public Dictionary<string, string> TotalWaveDicts = new();
        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, BooleanString> RescueCheckButtons = new();


        public StandardRescueSettings()
        {
            InitializeComponent();

            RescueCheckButtons.Add("msg", new BooleanString(false, ""));
            RescueCheckButtons.Add("prohibitboss", new BooleanString(false, null));
            RescueCheckButtons.Add("showstage", new BooleanString(false, null));
            RescueCheckButtons.Add("locktempo", new BooleanString(false, null));
            RescueCheckButtons.Add("nomobspawns", new BooleanString(false, null));
            RescueCheckButtons.Add("shouldallowmobswithtank", new BooleanString(false, null));
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

        private void TotalWaveClick(object sender, RoutedEventArgs e)
        {
            TryOpenTotalWaveWindow(Functions.ConvertToInt(TotalWaveTextbox.Text));
        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            if (!RescueCheckButtons["msg"].Item1 && (bool)MSGCheckBox.IsChecked)
                TryOpenMSGWindow();

            RescueCheckButtons["msg"] = ((bool)MSGCheckBox.IsChecked, RescueCheckButtons["msg"].Item2);
            RescueCheckButtons["prohibitboss"] = ((bool)ProhibitBossCheckBox.IsChecked, null);
            RescueCheckButtons["showstage"] = ((bool)ShowStageCheckBox.IsChecked, null);
            RescueCheckButtons["locktempo"] = ((bool)LockTempoCheckBox.IsChecked, null);
            RescueCheckButtons["nomobspawns"] = ((bool)NoMobSpawnsCheckBox.IsChecked, null);
            RescueCheckButtons["shouldallowmobswithtank"] = ((bool)ShouldAllowMobsWithTankCheckBox.IsChecked, null);


            UpdateScriptWindow();
        }


        public void MSGReceived(string value)
        {
            if (value != null)
                RescueCheckButtons["msg"] = (true, value);
            else
                MSGCheckBox.IsChecked = false;
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
                if (item is TextBox)
                {
                    TextBox textBox = (TextBox)item;
                    TextBoxDicts[Functions.ConvertToInt(textBox.Name.Remove(0, 1))] = textBox.Text;
                }
                if (item is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)item;
                    ComboBoxDicts[Functions.ConvertToInt(comboBox.Name.Remove(0, 1))] = comboBox.SelectedIndex;
                }
            }

            foreach (var item1 in ComboBoxDicts)
                foreach (var item2 in TextBoxDicts)
                    if (item1.Key == item2.Key)
                        TotalWaveDicts[$"{item1.Key + 1}"] = $"{Functions.TotalWaveToString(item1.Value)}\x1b{item2.Value}";

            UpdateScriptWindow();
        }

        private void BuildUpMinIntervalButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "BuildUpMinInterval的值代表导演生成尸潮的节奏中BUILD_UP阶段的最短持续时间。\n\n有效范围为非负整数。\n\n默认为15。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void FarAcquireRangeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "FarAcquireRange的值代表小僵尸能发现生还者的最大距离。\n\n有效范围为非负浮点数。\n\n默认为2500。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void NearAcquireRangeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "NearAcquireRange的值代表小僵尸可以用最短的时间就能发现生还者的距离。\n\n有效范围为非负浮点数。\n\n默认为200。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void ClearedWandererRespawnChanceButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "ClearedWandererRespawnChance的值代表会有百分之多少的几率在已经清理过的导航区块上重新生成游荡的小僵尸。\n\n有效范围为0~100的整数。\n\n默认为0，而清道夫模式中默认为3。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void LockTempoButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "设置LockTempo = true会无延迟地生成尸潮。\n\n默认为false。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void NoMobSpawnsButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "设置NoMobSpawns = true会停止新的僵尸生成。\n\n原有暂时等待生成的僵尸仍会继续生成。\n\n不会重置生成计时器。\n\n默认为false。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void ShouldAllowMobsWithTankButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "设置ShouldAllowMobsWithTank = true会允许在Tank在场时自然生成小僵尸。\n\nBoomer和胆汁炸弹引起的尸潮不受影响。\n\n仅适用于战役模式。\n\n默认为false。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void UpdateScriptWindow()
        {
            var ScriptWindowText = "";
            if (RescueCheckButtons["msg"].Item1)
                ScriptWindowText += $"Msg(\"{RescueCheckButtons["msg"].Item2}\");\n\n";

            ScriptWindowText += "PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\n\nDirectorOptions <-\n{\n";

            if (RescueCheckButtons["prohibitboss"].Item1)
                ScriptWindowText += "\tProhibitBosses = true\n\n";

            ScriptWindowText += $"\tA_CustomFinale_StageCount = {TotalWaves}\n\n";

            if (TotalWaveDicts.Count != 0)
            {
                foreach (var item in TotalWaveDicts)
                {
                    ScriptWindowText += $"\tA_CustomFinale{item.Key} = {item.Value.Split('\x1b')[0]}\n";
                    ScriptWindowText += $"\tA_CustomFinaleValue{item.Key} = {item.Value.Split('\x1b')[1]}\n";
                }
                ScriptWindowText += "\n";
            }

            if (RescueCheckButtons["locktempo"].Item1)
                ScriptWindowText += "\tLockTempo = true\n";

            if (RescueCheckButtons["nomobspawns"].Item1)
                ScriptWindowText += "\tNoMobSpawns = true\n";

            if (RescueCheckButtons["shouldallowmobswithtank"].Item1)
                ScriptWindowText += "\tShouldAllowMobsWithTank = true\n";

            ScriptWindowText += "}\n";

            if (RescueCheckButtons["showstage"].Item1)
                ScriptWindowText += "\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \"+ type);\n}\n";

            ScriptWindow.Text = ScriptWindowText;
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TotalWaveTextbox.Text != "" && !Functions.IsProperInt(TotalWaveTextbox.Text, 1, 99))
            {
                MessageBox.Show("非法输入！\n只能输入1到99的整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalWaveTextbox.Text = "3";
            }

            if (BuildUpMinIntervalTextBox.Text != "" && !Functions.IsProperInt(BuildUpMinIntervalTextBox.Text, 0, int.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                BuildUpMinIntervalTextBox.Text = "15";
            }

            if (FarAcquireRangeTextBox.Text != "" && !Functions.IsProperFloat(FarAcquireRangeTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                FarAcquireRangeTextBox.Text = "2500";
            }

            if (NearAcquireRangeTextBox.Text != "" && !Functions.IsProperFloat(NearAcquireRangeTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                NearAcquireRangeTextBox.Text = "200";
            }

            if (ClearedWandererRespawnChanceTextBox.Text != "" && !Functions.IsProperInt(ClearedWandererRespawnChanceTextBox.Text, 0, 100))
            {
                MessageBox.Show("非法输入！\n只能输入0到100的整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                ClearedWandererRespawnChanceTextBox.Text = "0";
            }
        }
    }
}
