using Microsoft.Win32;
using System.Collections.Generic;
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
            DisallowThreatTypeComboBox.ItemsSource = Globals.DisallowThreatTypeList;
            MapSelectionComboBox.ItemsSource = Globals.OfficialMapOnslaughtList;
            MapSelectionComboBox.SelectedIndex = 0;
            OnslaughtDict.Add("MSG", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("PlayMegaMobWarningSounds", new BooleanString(false, null));
            OnslaughtDict.Add("ResetMobTimer", new BooleanString(false, null));
            OnslaughtDict.Add("ResetSpecialTimers", new BooleanString(false, null));
            OnslaughtDict.Add("AlwaysAllowWanderers", new BooleanString(false, null));
            OnslaughtDict.Add("LockTempo", new BooleanString(false, null));
            OnslaughtDict.Add("BuildUpMinInterval", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobRechargeRate", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobSpawnMaxTime", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MobSpawnMinTime", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MusicDynamicMobScanStopSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MusicDynamicMobSpawnSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("MusicDynamicMobStopSize", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("NumReservedWanderers", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("PreferredMobDirection", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("RelaxMaxFlowTravel", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("RelaxMaxInterval", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("RelaxMinInterval", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("ZombieSpawnInFog", new BooleanString(false, null));
            OnslaughtDict.Add("ZombieSpawnRange", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("DisallowThreatType", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            OnslaughtDict.Add("ProhibitBosses", new BooleanString(false, null));
            OnslaughtDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            OnslaughtDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            OnslaughtDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
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
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, 1))
                    {
                        Functions.TryOpenMessageWindow(2);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 2:
                    if (textBox.Text != string.Empty && !Functions.IsProperFloat(textBox.Text, 0, float.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(3);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 3:
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(4);
                        textBox.Text = string.Empty;
                    }
                    break;
                case 4:
                    if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, -1, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(5);
                        textBox.Text = string.Empty;
                    }
                    break;
                default:
                    break;
            }
            if (Name != "MSG")
            {
                OnslaughtDict[Name] = (textBox.Text != string.Empty, textBox.Text);
            }

            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
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

            ScriptWindowText.AppendLine("DirectorOptions <-\n{");

            foreach (var item in OnslaughtDict)
            {
                if (item.Value.Item1 && !Globals.OnslaughtBlackList.Contains(item.Key))
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
            }

            ScriptWindowText.AppendLine("}\n");

            if(OnslaughtDict["PlayMegaMobWarningSounds"].Item1)
            {
                ScriptWindowText.AppendLine("Director.PlayMegaMobWarningSounds();");
            }
            if (OnslaughtDict["ResetMobTimer"].Item1)
            {
                ScriptWindowText.AppendLine("Director.ResetMobTimer();");
            }
            if (OnslaughtDict["ResetSpecialTimers"].Item1)
            {
                ScriptWindowText.AppendLine("Director.ResetSpecialTimers();");
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

        private void PreviewOfficialScriptClick(object sender, RoutedEventArgs e)
        {
            PreviewScriptWindow previewScriptWindow = new()
            {
                Title = $"正在预览{MapSelectionComboBox.SelectedItem}的脚本",
                TextBoxString = Functions.GetOfficialOnslaughtScriptFile(MapSelectionComboBox.SelectedIndex),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            previewScriptWindow.ShowDialog();
        }
    }
}