using System;
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
        public List<string> PreferredMobDirectionList = new() { "", "SPAWN_ABOVE_SURVIVORS", "SPAWN_ANYWHERE", "SPAWN_BEHIND_SURVIVORS", "SPAWN_FAR_AWAY_FROM_SURVIVORS", "SPAWN_IN_FRONT_OF_SURVIVORS", "SPAWN_LARGE_VOLUME", "SPAWN_NEAR_IT_VICTIM", "SPAWN_NO_PREFERENCE" };
        public List<string> PreferredSpecialDirectionList = new() { "", "SPAWN_ABOVE_SURVIVORS", "SPAWN_SPECIALS_ANYWHERE", "SPAWN_BEHIND_SURVIVORS", "SPAWN_FAR_AWAY_FROM_SURVIVORS", "SPAWN_SPECIALS_IN_FRONT_OF_SURVIVORS", "SPAWN_LARGE_VOLUME", "SPAWN_NEAR_IT_VICTIM", "SPAWN_NO_PREFERENCE" };

        public StandardRescueSettings()
        {
            InitializeComponent();

            PreferredMobDirectionComboBox.ItemsSource = PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = PreferredSpecialDirectionList;
            RescueCheckButtons.Add("msg", new BooleanString(false, ""));
            RescueCheckButtons.Add("prohibitboss", new BooleanString(false, null));
            RescueCheckButtons.Add("showstage", new BooleanString(false, null));
            RescueCheckButtons.Add("locktempo", new BooleanString(false, null));
            RescueCheckButtons.Add("ProhibitBosses", new BooleanString(false, null));
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
            RescueCheckButtons["ProhibitBosses"] = ((bool)ProhibitBossesCheckBox.IsChecked, null);
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

        private void IntensityRelaxThresholdButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "IntensityRelaxThreshold的值代表所有生还者的紧张度都必须小于多少才能让节奏从SUSTAIN_PEAK切换为RELAX。\n\n有效范围为0-1的浮点数。\n\n默认值为0.9。",
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
                TextBlockString = "设置LockTempo为true会无延迟地生成尸潮。\n\n默认值为false。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }
        private void MobRechargeRateButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MobRechargeRate的值代表一次尸潮内生成下一个普通感染者的速度。\n\n有效范围为非负浮点数。\n\n默认值为0.0025。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MobSpawnMaxTimeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MobSpawnMaxTime的值代表两波尸潮生成的最大时间隔秒数。\n\n有效范围为非负浮点数。\n\n默认值根据难度变化，为180.0-240.0。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MobSpawnMinTimeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MobSpawnMinTime的值代表两波尸潮生成的最小时间隔秒数。\n\n有效范围为非负浮点数。\n\n默认值根据难度变化，为90.0-120.0。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MusicDynamicMobScanStopSizeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MusicDynamicMobScanStopSize的值代表尸潮的大小不足此数时会停止背景音乐。\n\n有效范围为非负整数。\n\n默认值为3。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MusicDynamicMobSpawnSizeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MusicDynamicMobSpawnSize的值代表尸潮的大小达到此数时会开始播放背景音乐。\n\n有效范围为非负整数。\n\n默认值为25。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MusicDynamicMobStopSizeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MusicDynamicMobStopSize的值代表尸潮的大小达到此数时会停止背景音乐。\n\n有效范围为非负整数。\n\n默认值为8。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void PreferredMobDirectionButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "PreferredMobDirection的值代表尸潮生成的方位。\n\n有效范围为-1到10的整数。\n\n默认值为SPAWN_NO_PREFERENCE。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void PreferredSpecialDirectionButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "PreferredSpecialDirection的值代表特感生成的方位。\n\n有效范围为-1到10的整数。\n\n默认值为SPAWN_NO_PREFERENCE。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void RelaxMaxFlowTravelButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "RelaxMaxFlowTravel的值代表生还者最远能前进多少距离就会让节奏从RELAX切换到BUILD_UP。\n\n有效范围为非负浮点数。\n\n默认值为3000。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void RelaxMaxIntervalButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "RelaxMaxInterval的值代表节奏中RELAX最长持续秒数。\n\n有效范围为非负浮点数。\n\n默认值为45。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void RelaxMinIntervalButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "RelaxMinInterval的值代表节奏中RELAX最短持续秒数。\n\n有效范围为非负浮点数。\n\n默认值为30。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void ProhibitBossesButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "设置ProhibitBosses为true会防止Tank和Witch生成。\n\n默认值为false。",
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
                TextBlockString = "设置ShouldAllowMobsWithTank为true会允许在Tank在场时生成小僵尸。\n\nBoomer和胆汁炸弹引起的尸潮不受影响。\n\n仅适用于战役模式。\n\n默认为false。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void ShouldAllowSpecialsWithTankButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "设置ShouldAllowSpecialsWithTank为true会允许在Tank在场时生成特殊感染者。\n\n仅适用于战役模式。\n\n默认为false。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void SpecialRespawnIntervalButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "SpecialRespawnInterval的值代表特殊感染者重生所需要的秒数。\n\n有效范围为非负浮点数。\n\n默认值为：战役模式为45，对抗模式为20。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void SustainPeakMaxTimeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "SustainPeakMaxTime的值代表节奏中SUSTAIN_PEAK的最长持续分钟数。\n\n有效范围为非负浮点数。\n\n默认值为5。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void SustainPeakMinTimeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "SustainPeakMinTime的值代表节奏中SUSTAIN_PEAK的最短持续分钟数。\n\n有效范围为非负浮点数。\n\n默认值为3。",
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

            if (RescueCheckButtons["ProhibitBosses"].Item1)
                ScriptWindowText += "\tProhibitBosses = true\n";

            if (RescueCheckButtons["shouldallowmobswithtank"].Item1)
                ScriptWindowText += "\tShouldAllowMobsWithTank = true\n";

            ScriptWindowText += "}\n";

            if (RescueCheckButtons["showstage"].Item1)
                ScriptWindowText += "\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \"+ type);\n}\n";

            ScriptWindow.Text = ScriptWindowText;
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            bool updateFlag = true;
            if (TotalWaveTextbox.Text != "" && !Functions.IsProperInt(TotalWaveTextbox.Text, 1, 99))
            {
                MessageBox.Show("非法输入！\n只能输入1到99的整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                TotalWaveTextbox.Text = "";
            }
            else if (TotalWaveTextbox.Text == "")
            {
                updateFlag = false;
                TotalWaveButton.IsEnabled = false;
            }
            else
                TotalWaveButton.IsEnabled = true;

            if (IntensityRelaxThresholdTextBox.Text != "" && !Functions.IsProperFloat(IntensityRelaxThresholdTextBox.Text, 0, 1))
            {
                MessageBox.Show("非法输入！\n只能输入0到1的浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                IntensityRelaxThresholdTextBox.Text = "";
            }

            if (MobRechargeRateTextBox.Text != "" && !Functions.IsProperFloat(MobRechargeRateTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                MobRechargeRateTextBox.Text = "";
            }

            if (MobSpawnMaxTimeTextBox.Text != "" && !Functions.IsProperFloat(MobSpawnMaxTimeTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                MobSpawnMaxTimeTextBox.Text = "";
            }

            if (MobSpawnMinTimeTextBox.Text != "" && !Functions.IsProperFloat(MobSpawnMinTimeTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                MobSpawnMinTimeTextBox.Text = "";
            }

            if (MusicDynamicMobScanStopSizeTextBox.Text != "" && !Functions.IsProperInt(MusicDynamicMobScanStopSizeTextBox.Text, 0, int.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                MusicDynamicMobScanStopSizeTextBox.Text = "";
            }

            if (MusicDynamicMobSpawnSizeTextBox.Text != "" && !Functions.IsProperInt(MusicDynamicMobSpawnSizeTextBox.Text, 0, int.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                MusicDynamicMobSpawnSizeTextBox.Text = "";
            }

            if (MusicDynamicMobStopSizeTextBox.Text != "" && !Functions.IsProperInt(MusicDynamicMobStopSizeTextBox.Text, 0, int.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                MusicDynamicMobStopSizeTextBox.Text = "";
            }

            if (RelaxMaxFlowTravelTextBox.Text != "" && !Functions.IsProperFloat(RelaxMaxFlowTravelTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                RelaxMaxFlowTravelTextBox.Text = "";
            }

            if (RelaxMaxIntervalTextBox.Text != "" && !Functions.IsProperFloat(RelaxMaxIntervalTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                RelaxMaxIntervalTextBox.Text = "";
            }

            if (RelaxMinIntervalTextBox.Text != "" && !Functions.IsProperFloat(RelaxMinIntervalTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                RelaxMinIntervalTextBox.Text = "";
            }

            if (SpecialRespawnIntervalTextBox.Text != "" && !Functions.IsProperFloat(SpecialRespawnIntervalTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                SpecialRespawnIntervalTextBox.Text = "";
            }

            if (SustainPeakMaxTimeTextBox.Text != "" && !Functions.IsProperFloat(SustainPeakMaxTimeTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                SustainPeakMaxTimeTextBox.Text = "";
            }

            if (SustainPeakMinTimeTextBox.Text != "" && !Functions.IsProperFloat(SustainPeakMinTimeTextBox.Text, 0, float.MaxValue))
            {
                MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                SustainPeakMinTimeTextBox.Text = "";
            }

            if (updateFlag)
                UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PreferredMobDirectionComboBox.SelectedIndex == 0)
                Console.WriteLine("1");
        }
    }
}
