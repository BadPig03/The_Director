using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class StandardRescueSettings : UserControl
    {
        public int? TotalWaveCount = null;
        public bool IsTotalWaveConfirmed = new();
        public bool IsScriptWindowEnabled = new();

        public Dictionary<string, string> TotalWaveDicts = new();
        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, BooleanString> StandardDict = new();

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
            PreferredMobDirectionComboBox.ItemsSource = Globals.PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = Globals.PreferredSpecialDirectionList;
            StandardDict.Add("MSG", new BooleanString(false, string.Empty));
            StandardDict.Add("ShowStage", new BooleanString(false, null));
            StandardDict.Add("LockTempo", new BooleanString(false, null));
            StandardDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
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
            StandardDict.Add("MinimumStageTime", new BooleanString(false, string.Empty));
            StandardDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            StandardDict.Add("ProhibitBosses", new BooleanString(false, null));
            StandardDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            StandardDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            StandardDict.Add("EscapeSpawnTanks", new BooleanString(true, null));
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
            StandardDict.Add("HordeEscapeCommonLimit", new BooleanString(false, string.Empty));
        }

        private void TotalWaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (Functions.IsProperInt(TotalWaveTextbox.Text, 1, int.MaxValue))
            {
                if (TryOpenTotalWaveWindow(Functions.ConvertToInt(TotalWaveTextbox.Text)))
                    TotalWaveTextbox.Text = TotalWaveCount.ToString();
                else
                    TotalWaveCount = Functions.ConvertToInt(TotalWaveTextbox.Text);
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
                    if(textBox.Name.StartsWith("__"))
                        TextBoxDicts[Functions.ConvertToInt(textBox.Name.Remove(0, 2))] += $"{((textBox.Text != string.Empty)?"\x1c":string.Empty)}{textBox.Text}";
                    else
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

            if (IsChecked && (TotalWaveCount <= 0 || TotalWaveCount == null))
            {
                Functions.TryOpenMessageWindow(0);
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
            switch (Functions.TextBoxIndex(Name))
            {
                case 0:
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 1, 99))
                    {
                        Functions.TryOpenMessageWindow(1);
                        textBox.Text = string.Empty;
                        if (TotalWaveCount != null)
                            TotalWaveCount = -1;
                    }
                    else if (textBox.Text == string.Empty)
                    {
                        foreach (var item in StandardDict)
                            if (item.Value.Item1)
                            {
                                Functions.TryOpenMessageWindow(0);
                                IsScriptWindowEnabled = false;
                                break;
                            }
                        TotalWaveButton.IsEnabled = false;
                        if (TotalWaveCount != null)
                            TotalWaveCount = -1;
                    }
                    else
                    {
                        IsScriptWindowEnabled = true;
                        TotalWaveButton.IsEnabled = true;
                        if (textBox.Text != TotalWaveCount.ToString())
                            TotalWaveButtonClick(TotalWaveButton, null);
                    }
                    break;
                case 1:
                    if (textBox.Text != string.Empty && (TotalWaveCount <= 0 || TotalWaveCount == null))
                    {
                        Functions.TryOpenMessageWindow(0);
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, 1))
                    {
                        Functions.TryOpenMessageWindow(2);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 2:
                    if (textBox.Text != string.Empty && (TotalWaveCount <= 0 || TotalWaveCount == null))
                    {
                        Functions.TryOpenMessageWindow(0);
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, float.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(3);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 3:
                    if (textBox.Text != string.Empty && (TotalWaveCount <= 0 || TotalWaveCount == null))
                    {
                        Functions.TryOpenMessageWindow(0);
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(4);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 4:
                    if (textBox.Text != string.Empty && (TotalWaveCount <= 0 || TotalWaveCount == null))
                    {
                        Functions.TryOpenMessageWindow(0);
                        textBox.Text = string.Empty;
                        return;
                    }
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, -1, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(5);
                        textBox.Text = string.Empty;
                    }
                    break;
                default:
                    break;
            }
            if (Name != "TotalWave" || Name != "MSG")
                StandardDict[Name] = (textBox.Text != string.Empty, textBox.Text);

            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex != 0 && (TotalWaveCount <= 0 || TotalWaveCount == null))
            {
                Functions.TryOpenMessageWindow(0);
                comboBox.SelectedIndex = 0;
                return;
            }

            StandardDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            if (!IsScriptWindowEnabled)
                ScriptWindow.Text = string.Empty;
            else
            {
                StringBuilder ScriptWindowText = new();

                if ((bool)MSGCheckBox.IsChecked)
                    ScriptWindowText.AppendLine($"Msg(\"{StandardDict["MSG"].Item2}\");\n\n");

                if (TotalWaveCount > 0)
                    ScriptWindowText.AppendLine("PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\n\nDirectorOptions <-\n{\n");

                if (TotalWaveDicts.Count != 0 && TotalWaveCount > 0)
                {
                    ScriptWindowText.AppendLine($"\tA_CustomFinale_StageCount = {TotalWaveCount}\n\n");
                    foreach (var item in TotalWaveDicts)
                    {
                        ScriptWindowText.AppendLine($"\tA_CustomFinale{item.Key} = {item.Value.Split('\x1b')[0]}\n");
                        if (item.Value.Split('\x1b')[1].Contains("\x1c"))
                        {
                            ScriptWindowText.AppendLine($"\tA_CustomFinaleValue{item.Key} = {item.Value.Split('\x1b')[1].Split('\x1c')[0]}\n");
                            if (item.Value.Split('\x1b')[0] == "TANK")
                                ScriptWindowText.AppendLine($"\tA_CustomFinaleMusic{item.Key} = {item.Value.Split('\x1b')[1].Split('\x1c')[1]}\n");
                        }
                        else
                            ScriptWindowText.AppendLine($"\tA_CustomFinaleValue{item.Key} = {item.Value.Split('\x1b')[1]}\n");
                    }
                    ScriptWindowText.AppendLine("\n");
                }

                foreach (var item in StandardDict)
                {
                    if (item.Value.Item1 && !Globals.CheckBoxBlackList.Contains(item.Key))
                    {
                        if (item.Value.Item2 != null)
                            ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item2}\n");
                        else
                            ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item1.ToString().ToLower()}\n");
                    }
                    else if (!item.Value.Item1 && item.Key == "EscapeSpawnTanks")
                        ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item1.ToString().ToLower()}\n");
                }

                if (TotalWaveCount > 0)
                    ScriptWindowText.AppendLine("}\n");

                if (StandardDict["ShowStage"].Item1)
                    ScriptWindowText.AppendLine("\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \"+ type);\n}\n");

                ScriptWindow.Text = ScriptWindowText.ToString();
            }

            if (ScriptWindow.Text != string.Empty)
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
            Functions.TryOpenMessageWindow(6);
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
