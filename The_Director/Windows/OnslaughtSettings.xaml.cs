using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class OnslaughtSettings : UserControl
    {
        public bool IsNutConfirmed = new();

        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, BooleanString> OnslaughtDict = new();

        public void MSGReceived(string value)
        {
            if (value != null)
            {
                OnslaughtDict["MSG"] = (true, value);
            }
            else
            {
                MSGCheckBox.IsChecked = false;
            }
        }

        public void SaveToNutReceived(string value)
        {
            IsNutConfirmed = value != null;
        }

        public OnslaughtSettings()
        {
            InitializeComponent();
            PreferredMobDirectionComboBox.ItemsSource = Globals.PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = Globals.PreferredSpecialDirectionList;
            MapSelectionComboBox.ItemsSource = Globals.OffcialMapOnslaughtList;
            MapSelectionComboBox.SelectedIndex = 0;
            OnslaughtDict.Add("MSG", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("ShowStage", new BooleanString(false, null));
            OnslaughtDict.Add("LockTempo", new BooleanString(false, null));
            OnslaughtDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobRechargeRate", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobSpawnMaxTime", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobSpawnMinTime", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MusicDynamicMobScanStopSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MusicDynamicMobSpawnSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MusicDynamicMobStopSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("PreferredMobDirection", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("RelaxMaxFlowTravel", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("RelaxMaxInterval", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("RelaxMinInterval", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("ProhibitBosses", new BooleanString(false, null));
            OnslaughtDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            OnslaughtDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            OnslaughtDict.Add("EscapeSpawnTanks", new BooleanString(true, null));
            OnslaughtDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("BileMobSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("BoomerLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("ChargerLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("CommonLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("DominatorLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("HunterLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("JockeyLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MaxSpecials", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MegaMobSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobMaxPending", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobMaxSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobMinSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobSpawnSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("SmokerLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("SpitterLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("TankLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("WitchLimit", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("HordeEscapeCommonLimit", new BooleanString(false, string.Empty));
            EscapeSpawnTanksCheckBox.IsChecked = true;
        }

        private void TryOpenMSGWindow()
        {
            InputNewText inputNewText = new()
            {
                TextBoxText = OnslaughtDict["MSG"].Item2,
                SendMessage = MSGReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            inputNewText.ShowDialog();
            UpdateScriptWindow();
        }

        private void CheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            var Name = checkBox.Name.Remove(checkBox.Name.Length - 8, 8);
            var IsChecked = (bool)checkBox.IsChecked;

            if (Name != "MSG")
            {
                OnslaughtDict[Name] = (IsChecked, null);
            }
            else
            {
                if (!OnslaughtDict[Name].Item1 && IsChecked)
                {
                    TryOpenMSGWindow();
                }

                OnslaughtDict[Name] = (IsChecked, OnslaughtDict[Name].Item2);
            }
            UpdateScriptWindow();
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            var Name = textBox.Name.Remove(textBox.Name.Length - 7, 7);
            switch (Globals.TextBoxIndex(Name))
            {
                case 1:
                    if (textBox.Text != string.Empty)
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
                    if (textBox.Text != string.Empty)
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
                    if (textBox.Text != string.Empty && !Name.Contains(" "))
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
                    if (textBox.Text != string.Empty)
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
                case 5:
                    if (textBox.Text == string.Empty || !Functions.IsProperString(textBox.Text))
                    {
                        Functions.TryOpenMessageWindow(6);
                        if (Name == "info_director")
                        {
                            textBox.Text = "director";
                        }
                        else if (Name == "trigger_finale")
                        {
                            textBox.Text = "finale_radio";
                        }
                        else if (Name == "ScriptFile")
                        {
                            textBox.Text = "standard_finale.nut";
                        }
                        return;
                    }
                    break;
                default:
                    break;
            }
            if (Name != "TotalWave" || Name != "MSG")
            {
                OnslaughtDict[Name] = (textBox.Text != string.Empty, textBox.Text);
            }

            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (comboBox.SelectedIndex != 0)
            {
                Functions.TryOpenMessageWindow(0);
                comboBox.SelectedIndex = 0;
                return;
            }

            OnslaughtDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            StringBuilder ScriptWindowText = new();

            if ((bool)MSGCheckBox.IsChecked)
            {
                ScriptWindowText.AppendLine($"Msg(\"{OnslaughtDict["MSG"].Item2}\");\n");
            }

            ScriptWindowText.AppendLine("PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\n\nDirectorOptions <-\n{");

            foreach (var item in OnslaughtDict)
            {
                if (item.Value.Item1 && !Globals.StandardDictBlackList.Contains(item.Key))
                {
                    if (item.Value.Item2 != null)
                    {
                        ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item2}");
                    }
                    else
                    {
                        ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item1.ToString().ToLower()}");
                    }
                }
                else if (!item.Value.Item1 && item.Key == "EscapeSpawnTanks")
                {
                    ScriptWindowText.AppendLine($"\t{item.Key} = {item.Value.Item1.ToString().ToLower()}");
                }
            }

            ScriptWindowText.AppendLine("}");

            if (OnslaughtDict["ShowStage"].Item1)
            {
                ScriptWindowText.AppendLine("\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \"+ type);\n}");
            }

            ScriptWindow.Text = ScriptWindowText.ToString();
            
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
            HintWindow hintWindow = new()
            {
                TextBoxString = Globals.GetButtonString(label.Content.ToString()),
                HyperlinkUri = Globals.GetButtonHyperlinkUri(label.Content.ToString()),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            hintWindow.ShowDialog();
        }

        private void PasteToClipboardClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ScriptWindow.Text);
            Functions.TryOpenMessageWindow(7);
        }

        private void SaveAsNutClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "导出脚本文件",
                Filter = "nut文件 (*.nut)|*.nut",
                InitialDirectory = Globals.L4D2ScriptsPath
            };
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != string.Empty)
            {
                Functions.SaveNutToPath(saveFileDialog.FileName, ScriptWindow.Text);
            }
        }

        private void PreviewOfficalScriptClick(object sender, RoutedEventArgs e)
        {
            PreviewScriptWindow previewScriptWindow = new()
            {
                Title = $"正在预览{MapSelectionComboBox.SelectedItem}的救援脚本",
                TextBoxString = Functions.GetOffcialOnslaughtScriptFile(MapSelectionComboBox.SelectedIndex),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            previewScriptWindow.ShowDialog();
        }
    }
}