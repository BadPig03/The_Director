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
        public bool IsScriptWindowEnabled = new();

        public Dictionary<string, string> TotalWaveDicts = new();
        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, BooleanString> StandardDict = new();

        public List<string> PreferredMobDirectionList = new() { string.Empty, "SPAWN_ABOVE_SURVIVORS", "SPAWN_ANYWHERE", "SPAWN_BEHIND_SURVIVORS", "SPAWN_FAR_AWAY_FROM_SURVIVORS", "SPAWN_IN_FRONT_OF_SURVIVORS", "SPAWN_LARGE_VOLUME", "SPAWN_NEAR_IT_VICTIM", "SPAWN_NO_PREFERENCE" };
        public List<string> PreferredSpecialDirectionList = new() { string.Empty, "SPAWN_ABOVE_SURVIVORS", "SPAWN_SPECIALS_ANYWHERE", "SPAWN_BEHIND_SURVIVORS", "SPAWN_FAR_AWAY_FROM_SURVIVORS", "SPAWN_SPECIALS_IN_FRONT_OF_SURVIVORS", "SPAWN_LARGE_VOLUME", "SPAWN_NEAR_IT_VICTIM", "SPAWN_NO_PREFERENCE" };
        public List<string> CheckBoxBlackList = new() { "TotalWave", "MSG", "ShowStage" };

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
            InputNewText inputNewText = new InputNewText
            {
                TextBoxText = StandardDict["MSG"].Item2,
                SendMessage = MSGReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            inputNewText.ShowDialog();
            UpdateScriptWindow();
        }

        private bool TryOpenTotalWaveWindow(int WaveCount)
        {
            TextBoxDicts.Clear();
            ComboBoxDicts.Clear();
            TotalWaveSettings totalWaveSettings = new TotalWaveSettings
            {
                WaveCounts = WaveCount,
                TotalWaveDicts = TotalWaveDicts,
                SendMessage = TotalWaveReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            totalWaveSettings.ShowDialog();
            if (!IsTotalWaveConfirmed)
                return true;
            foreach (var item in totalWaveSettings.TotalWaveGrid.Children)
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

            if (IsChecked && TotalWaves <= 0)
            {
                MessageWindow messageWindow = new MessageWindow()
                {
                    Title = "警告",
                    TextBoxString = "未设置尸潮波数！",
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                messageWindow.ShowDialog();
                button.IsChecked = false;
                return;
            }
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
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "错误",
                            TextBoxString = "非法输入！\n只能输入1到99的整数!",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        textBox.Text = string.Empty;
                        TotalWaves = -1;
                    }
                    else if (textBox.Text == string.Empty)
                    {
                        foreach(var item in StandardDict)
                            if(item.Value.Item1)
                            {
                                MessageWindow messageWindow = new MessageWindow()
                                {
                                    Title = "警告",
                                    TextBoxString = "未设置尸潮波数！",
                                    Owner = Application.Current.MainWindow,
                                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                                };
                                messageWindow.ShowDialog();
                                IsScriptWindowEnabled = false;
                                break;
                            }
                        TotalWaveButton.IsEnabled = false;
                        TotalWaves = -1;
                    }
                    else
                    {
                        IsScriptWindowEnabled = true;
                        TotalWaveButton.IsEnabled = true;
                        if (textBox.Text != TotalWaves.ToString())
                            TotalWaveButtonClick(TotalWaveButton, null);
                    }
                    break;
                case 1:
                    if (textBox.Text != string.Empty && TotalWaves <= 0)
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "警告",
                            TextBoxString = "未设置尸潮波数！",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, 1))
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "错误",
                            TextBoxString = "非法输入！\n只能输入0到1的浮点数!",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        textBox.Text = string.Empty;
                    }
                    break;
                case 2:
                    if (textBox.Text != string.Empty && TotalWaves <= 0)
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "警告",
                            TextBoxString = "未设置尸潮波数！",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, float.MaxValue))
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "错误",
                            TextBoxString = "非法输入！\n只能输入非负浮点数!",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        textBox.Text = string.Empty;
                    }
                    break;
                case 3:
                    if (textBox.Text != string.Empty && TotalWaves <= 0)
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "警告",
                            TextBoxString = "未设置尸潮波数！",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "错误",
                            TextBoxString = "非法输入！\n只能输入非负整数!",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        textBox.Text = string.Empty;
                    }
                    break;
                case 4:
                    if (textBox.Text != string.Empty && TotalWaves <= 0)
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "警告",
                            TextBoxString = "未设置尸潮波数！",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, -1, int.MaxValue))
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "错误",
                            TextBoxString = "非法输入！\n只能输入大于等于-1的整数!",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
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

            if (comboBox.SelectedIndex != 0 && TotalWaves <= 0)
            {
                MessageWindow messageWindow = new MessageWindow()
                {
                    Title = "警告",
                    TextBoxString = "未设置尸潮波数！",
                    Owner = Application.Current.MainWindow,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                messageWindow.ShowDialog();
                comboBox.SelectedIndex = 0;
                return;
            }

            StandardDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            if (!IsScriptWindowEnabled)
            {
                ScriptWindow.Text = string.Empty;
                return;
            }

            var ScriptWindowText = string.Empty;

            if ((bool)MSGCheckBox.IsChecked)
                ScriptWindowText += $"Msg(\"{StandardDict["MSG"].Item2}\");\n\n";

            if(TotalWaves > 0)
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
                if (item.Value.Item1 && !CheckBoxBlackList.Contains(item.Key))
                {
                    if (item.Value.Item2 != null)
                        ScriptWindowText += $"\t{item.Key} = {item.Value.Item2}\n";
                    else
                        ScriptWindowText += $"\t{item.Key} = {item.Value.Item1.ToString().ToLower()}\n";
                }
            }

            if (TotalWaves > 0)
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

        private void MouseClick(object sender, RoutedEventArgs e)
        {
            Label label = (Label)sender;
            HintWindow hintWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = Functions.GetButtonString(label.Content.ToString()),
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions",
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            hintWindow.ShowDialog();
        }

        private void PasteToClipboardClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ScriptWindow.Text);
            MessageWindow messageWindow = new MessageWindow()
            {
                Title = "提示",
                TextBoxString = "已复制到粘贴板！",
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            messageWindow.ShowDialog();
        }

        private void SaveAsNutClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "导出脚本文件",
                Filter = "nut文件 (*.nut)|*.nut",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != string.Empty)
            {
                string filePath = saveFileDialog.FileName + (saveFileDialog.FileName.EndsWith(".nut")?string.Empty:".nut");
                File.WriteAllText(filePath, ScriptWindow.Text);
            }
        }
    }
}
