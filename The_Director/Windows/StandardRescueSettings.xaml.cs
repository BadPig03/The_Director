using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        public Dictionary<string, BooleanString> StandardDict = new();

        public List<string> PreferredMobDirectionList = new() { string.Empty, "SPAWN_ABOVE_SURVIVORS", "SPAWN_ANYWHERE", "SPAWN_BEHIND_SURVIVORS", "SPAWN_FAR_AWAY_FROM_SURVIVORS", "SPAWN_IN_FRONT_OF_SURVIVORS", "SPAWN_LARGE_VOLUME", "SPAWN_NEAR_IT_VICTIM", "SPAWN_NO_PREFERENCE" };
        public List<string> PreferredSpecialDirectionList = new() { string.Empty, "SPAWN_ABOVE_SURVIVORS", "SPAWN_SPECIALS_ANYWHERE", "SPAWN_BEHIND_SURVIVORS", "SPAWN_FAR_AWAY_FROM_SURVIVORS", "SPAWN_SPECIALS_IN_FRONT_OF_SURVIVORS", "SPAWN_LARGE_VOLUME", "SPAWN_NEAR_IT_VICTIM", "SPAWN_NO_PREFERENCE" };
        public void MSGReceived(string value)
        {
            if (value != null)
                StandardDict["MSG"] = (true, value);
            else
                MSGCheckBox.IsChecked = false;
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

        public StandardRescueSettings()
        {
            InitializeComponent();
            PreferredMobDirectionComboBox.ItemsSource = PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = PreferredSpecialDirectionList;
            StandardDict.Add("MSG", new BooleanString(false, string.Empty));
            StandardDict.Add("ShowStage", new BooleanString(false, null));
            StandardDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
            StandardDict.Add("LockTempo", new BooleanString(false, null));
            StandardDict.Add("MobRechargeRate", new BooleanString(false, string.Empty));
            StandardDict.Add("MobSpawnMaxTime", new BooleanString(false, string.Empty));
            StandardDict.Add("MobSpawnMinTime", new BooleanString(false, string.Empty));
            StandardDict.Add("MusicDynamicMobScanStopSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MusicDynamicMobSpawnSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MusicDynamicMobStopSize", new BooleanString(false, string.Empty));
            StandardDict.Add("PreferredMobDirection", new BooleanString(false, string.Empty));
            StandardDict.Add("RelaxMaxFlowTravel", new BooleanString(false, string.Empty));
            StandardDict.Add("RelaxMaxInterval", new BooleanString(false, string.Empty));
            StandardDict.Add("RelaxMinInterval", new BooleanString(false, string.Empty));
            StandardDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            StandardDict.Add("ProhibitBosses", new BooleanString(false, null));
            StandardDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            StandardDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            StandardDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
            StandardDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            StandardDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
            StandardDict.Add("BileMobSize", new BooleanString(false, string.Empty));
            StandardDict.Add("BoomerLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("ChargerLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("CommonLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("DominatorLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("HunterLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("JockeyLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("MaxSpecials", new BooleanString(false, string.Empty));
            StandardDict.Add("MegaMobSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MobMaxPending", new BooleanString(false, string.Empty));
            StandardDict.Add("MobMaxSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MobMinSize", new BooleanString(false, string.Empty));
            StandardDict.Add("MobSpawnSize", new BooleanString(false, string.Empty));
            StandardDict.Add("SmokerLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("SpitterLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("TankLimit", new BooleanString(false, string.Empty));
            StandardDict.Add("WitchLimit", new BooleanString(false, string.Empty));
        }

        private void TotalWaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (Functions.IsProperInt(TotalWaveTextbox.Text, 1, int.MaxValue))
            {
                if (TryOpenTotalWaveWindow(Functions.ConvertToInt(TotalWaveTextbox.Text)))
                    TotalWaveTextbox.Text = TotalWaves.ToString();
                else
                    TotalWaves = Functions.ConvertToInt(TotalWaveTextbox.Text);
            }
        }

        private void TryOpenMSGWindow()
        {
            InputNewText InputWindow = new InputNewText
            {
                TextBoxText = StandardDict["MSG"].Item2,
                SendMessage = MSGReceived
            };
            InputWindow.ShowDialog();
            UpdateScriptWindow();
        }

        private bool TryOpenTotalWaveWindow(int WaveCount)
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
                return true;
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
            return false;
        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox button = (CheckBox)sender;
            var Name = button.Name.Remove(button.Name.Length - 8, 8);
            var IsChecked = (bool)button.IsChecked;
            if (Name != "MSG")
                StandardDict[Name] = (IsChecked, null);
            else
            {
                if (!StandardDict[Name].Item1 && IsChecked)
                    TryOpenMSGWindow();
                StandardDict[Name] = (IsChecked, StandardDict[Name].Item2);
            }
            UpdateScriptWindow();
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            var Name = textBox.Name.Remove(textBox.Name.Length - 7, 7);
            switch(Functions.TextBoxIndex(Name))
            {
                case 0:
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 1, 99))
                    {
                        MessageBox.Show("非法输入！\n只能输入1到99的整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        textBox.Text = string.Empty;
                        TotalWaves = -1;
                    }
                    else if (textBox.Text == string.Empty)
                        TotalWaveButton.IsEnabled = false;
                    else
                    {
                        TotalWaveButton.IsEnabled = true;
                        if (textBox.Text != TotalWaves.ToString())
                            TotalWaveButtonClick(TotalWaveButton, null);
                    }
                    break;
                case 1:
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, 1))
                    {
                        MessageBox.Show("非法输入！\n只能输入0到1的浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 2:
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, float.MaxValue))
                    {
                        MessageBox.Show("非法输入！\n只能输入非负浮点数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 3:
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        MessageBox.Show("非法输入！\n只能输入非负整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 4:
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, -1, int.MaxValue))
                    {
                        MessageBox.Show("非法输入！\n只能输入大于等于-1的整数!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        textBox.Text = string.Empty;
                    }
                    break;
                default:
                    break;
            }
            if(Name != "TotalWave" || Name != "MSG")
                StandardDict[Name] = (textBox.Text != string.Empty, textBox.Text);
            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            StandardDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            var ScriptWindowText = string.Empty;
            if ((bool)MSGCheckBox.IsChecked)
                ScriptWindowText += $"Msg(\"{StandardDict["MSG"].Item2}\");\n\n";
            ScriptWindowText += "PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\n\nDirectorOptions <-\n{\n";
            if (TotalWaveDicts.Count != 0 && TotalWaves > 0)
            {
                ScriptWindowText += $"\tA_CustomFinale_StageCount = {TotalWaves}\n\n";
                foreach (var item in TotalWaveDicts)
                {
                    ScriptWindowText += $"\tA_CustomFinale{item.Key} = {item.Value.Split('\x1b')[0]}\n";
                    ScriptWindowText += $"\tA_CustomFinaleValue{item.Key} = {item.Value.Split('\x1b')[1]}\n";
                }
                ScriptWindowText += "\n";
            }
            foreach (var item in StandardDict)
            {
                if (item.Value.Item1 && item.Key != "TotalWave" && item.Key != "MSG")
                {
                    if (item.Value.Item2 != null)
                        ScriptWindowText += $"\t{item.Key} = {item.Value.Item2}\n";
                    else
                        ScriptWindowText += $"\t{item.Key} = {item.Value.Item1.ToString().ToLower()}\n";
                }
            }
            ScriptWindowText += "}\n";
            if (StandardDict["ShowStage"].Item1)
                ScriptWindowText += "\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \"+ type);\n}\n";
            ScriptWindow.Text = ScriptWindowText;
            if(ScriptWindow.Text != string.Empty)
            {
                PasteToClipboardButton.IsEnabled = true;
                SaveAsNutButton.IsEnabled = true;
            }
            else
            {
                PasteToClipboardButton.IsEnabled = false;
                SaveAsNutButton.IsEnabled = false;
            }
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

        private void BileMobSizeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "BileMobSize的值代表Boomer和胆汁炸弹引起的普通感染者数量最大值。\n\n有效范围为非负整数。\n\n无默认值。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void BoomerLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "BoomerLimit的值代表在场的Boomer最大数量。\n\n有效范围为整数。\n\n默认值为1。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void ChargerLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "ChargerLimit的值代表在场的Charger最大数量。\n\n有效范围为整数。\n\n默认值为1。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void CommonLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "CommonLimit的值代表在场的普通感染者最大数量。\n\n有效范围为整数。\n\n默认值为30。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void DominatorLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "DominatorLimit的值代表在场的控制型特殊感染者(Hunter, Jockey, Charger, Smoker)最大数量。\n\n有效范围为整数。\n\n无默认值。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void HunterLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "HunterLimit的值代表在场的Hunter最大数量。\n\n有效范围为整数。\n\n默认值为1。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void JockeyLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "JockeyLimit的值代表在场的Jockey最大数量。\n\n有效范围为整数。\n\n默认值为1。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MaxSpecialsButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MaxSpecials的值代表在场的特殊感染者最大数量。\n\n有效范围为整数。\n\n默认值为2。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MegaMobSizeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MegaMobSize的值代表一次尸潮能生成的普通感染者最大数量。\n\n有效范围为非负整数。\n\n无默认值。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MobMaxPendingButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MobMaxPending的值代表当尸潮的普通感染者数量超过CommonLimit时最多有多少普通感染者可以暂时等待生成。\n\n有效范围为非负整数。\n\n无默认值。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MobMaxSizeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MobMaxSize的值代表一次尸潮生成普通感染者的最大数量。\n\n有效范围为非负整数。\n\n默认值为30。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MobMinSizeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MobMinSize的值代表一次尸潮生成普通感染者的最小数量。\n\n有效范围为非负整数。\n\n默认值为10。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void MobSpawnSizeButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "MobSpawnSize的值代表一次尸潮生成普通感染者的数量。\n\n覆盖MobMaxSize与MobMinSize。\n\n有效范围为非负整数。\n\n无默认值。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void SmokerLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "SmokerLimit的值代表代表在场的Smoker最大数量。\n\n有效范围为整数。\n\n默认值为1。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void SpitterLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "SpitterLimit的值代表代表在场的Spitter最大数量。\n\n有效范围为整数。\n\n默认值为1。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void TankLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "TankLimit的值代表代表在场的Tank最大数量。\n\n有效范围为整数。\n\n小于0则代表无限制。\n\n默认值为-1。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void WitchLimitButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "WitchLimit的值代表代表在场的Witch最大数量。\n\n有效范围为整数。\n\n小于0则代表无限制。\n\n默认值为-1。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void PasteToClipboardClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ScriptWindow.Text);
            MessageBox.Show("已复制到粘贴板！", "复制", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveAsNutClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "导出脚本文件";
            saveFileDialog.Filter = "nut文件 (*.nut)|*.nut";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != string.Empty)
            {
                string filePath = saveFileDialog.FileName + (saveFileDialog.FileName.EndsWith(".nut")?string.Empty:".nut");
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, ScriptWindow.Text);
                }
                else
                {
                    MessageBox.Show("已存在该文件！\n是否覆盖？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
            }
        }
    }
}
