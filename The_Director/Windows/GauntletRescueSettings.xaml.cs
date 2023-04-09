using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Policy;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class GauntletRescueSettings : UserControl
    {
        public bool IsNutConfirmed = new();
        public bool IsNavConfirmed = new();

        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, BooleanString> GauntletDict = new();

        public void MSGReceived(string value)
        {
            if (value != null)
            {
                GauntletDict["MSG"] = (true, value);
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

        public void SaveToNavReceived(string value)
        {
            IsNavConfirmed = value != null;
        }

        public GauntletRescueSettings()
        {
            InitializeComponent();
            PreferredMobDirectionComboBox.ItemsSource = Globals.PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = Globals.PreferredSpecialDirectionList;
            MapSelectionComboBox.ItemsSource = Globals.OffcialMapGauntletRescueList;
            MapSelectionComboBox.SelectedIndex = 0;
            GauntletDict.Add("MSG", new BooleanString(false, string.Empty));
            GauntletDict.Add("PanicForever", new BooleanString(true, null));
            GauntletDict.Add("PausePanicWhenRelaxing", new BooleanString(true, null));
            GauntletDict.Add("GauntletMovementThreshold", new BooleanString(false, string.Empty));
            GauntletDict.Add("GauntletMovementTimerLength", new BooleanString(false, string.Empty));
            GauntletDict.Add("GauntletMovementBonus", new BooleanString(false, string.Empty));
            GauntletDict.Add("GauntletMovementBonusMax", new BooleanString(false, string.Empty));
            GauntletDict.Add("CustomTankKiteDistance", new BooleanString(false, string.Empty));
            GauntletDict.Add("BridgeSpan", new BooleanString(false, string.Empty));
            GauntletDict.Add("MinSpeed", new BooleanString(false, string.Empty));
            GauntletDict.Add("MaxSpeed", new BooleanString(false, string.Empty));
            GauntletDict.Add("SpeedPenaltyZAdds", new BooleanString(false, string.Empty));
            GauntletDict.Add("CommonLimitMax", new BooleanString(false, string.Empty));
            GauntletDict.Add("LockTempo", new BooleanString(false, null));
            GauntletDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
            GauntletDict.Add("MobRechargeRate", new BooleanString(false, string.Empty));
            GauntletDict.Add("MobSpawnMaxTime", new BooleanString(false, string.Empty));
            GauntletDict.Add("MobSpawnMinTime", new BooleanString(false, string.Empty));
            GauntletDict.Add("MusicDynamicMobScanStopSize", new BooleanString(false, string.Empty));
            GauntletDict.Add("MusicDynamicMobSpawnSize", new BooleanString(false, string.Empty));
            GauntletDict.Add("MusicDynamicMobStopSize", new BooleanString(false, string.Empty));
            GauntletDict.Add("PreferredMobDirection", new BooleanString(false, string.Empty));
            GauntletDict.Add("RelaxMaxFlowTravel", new BooleanString(false, string.Empty));
            GauntletDict.Add("RelaxMaxInterval", new BooleanString(false, string.Empty));
            GauntletDict.Add("RelaxMinInterval", new BooleanString(false, string.Empty));
            GauntletDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            GauntletDict.Add("ProhibitBosses", new BooleanString(false, null));
            GauntletDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            GauntletDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            GauntletDict.Add("EscapeSpawnTanks", new BooleanString(true, null));
            GauntletDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
            GauntletDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            GauntletDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
            GauntletDict.Add("BileMobSize", new BooleanString(false, string.Empty));
            GauntletDict.Add("BoomerLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("ChargerLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("CommonLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("DominatorLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("HunterLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("JockeyLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("MaxSpecials", new BooleanString(false, string.Empty));
            GauntletDict.Add("MegaMobSize", new BooleanString(false, string.Empty));
            GauntletDict.Add("MobMaxPending", new BooleanString(false, string.Empty));
            GauntletDict.Add("MobMaxSize", new BooleanString(false, string.Empty));
            GauntletDict.Add("MobMinSize", new BooleanString(false, string.Empty));
            GauntletDict.Add("MobSpawnSize", new BooleanString(false, string.Empty));
            GauntletDict.Add("SmokerLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("SpitterLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("TankLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("WitchLimit", new BooleanString(false, string.Empty));
            GauntletDict.Add("PreTankMobMax", new BooleanString(false, string.Empty));
            info_directorTextBox.Text = "director";
            trigger_finaleTextBox.Text = "finale_radio";
            BridgeSpanTextBox.Text = "20000";
            MinSpeedTextBox.Text = "50";
            MaxSpeedTextBox.Text = "200";
            SpeedPenaltyZAddsTextBox.Text = "15";
            CommonLimitMaxTextBox.Text = "30";
            PanicForeverCheckBox.IsChecked = true;
            PausePanicWhenRelaxingCheckBox.IsChecked = true;
            EscapeSpawnTanksCheckBox.IsChecked = true;
        }

        private void TryOpenMSGWindow()
        {
            InputNewText inputNewText = new()
            {
                TextBoxText = GauntletDict["MSG"].Item2,
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
                GauntletDict[Name] = (IsChecked, null);
            }
            else
            {
                if (!GauntletDict[Name].Item1 && IsChecked)
                {
                    TryOpenMSGWindow();
                }

                GauntletDict[Name] = (IsChecked, GauntletDict[Name].Item2);
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
                        return;
                    }
                    break;
                case 6:
                    if (textBox.Text == string.Empty || !Functions.IsProperString(textBox.Text))
                    {
                        Functions.TryOpenMessageWindow(6);
                        
                    }
                    else if (textBox.Text != string.Empty && !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(4);
                    }
                    else
                    {
                        break;
                    }
                    if (Name == "BridgeSpan")
                    {
                        textBox.Text = "20000";
                    }
                    else if (Name == "MinSpeed")
                    {
                        textBox.Text = "50";
                    }
                    else if (Name == "MaxSpeed")
                    {
                        textBox.Text = "200";
                    }
                    else if (Name == "SpeedPenaltyZAdds")
                    {
                        textBox.Text = "15";
                    }
                    else if (Name == "CommonLimitMax")
                    {
                        textBox.Text = "30";
                    }
                    break;
                default:
                    break;
            }
            if (Name != "MSG")
            {
                GauntletDict[Name] = (textBox.Text != string.Empty, textBox.Text);
            }

            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            GauntletDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            StringBuilder ScriptWindowText = new();

            if ((bool)MSGCheckBox.IsChecked)
            {
                ScriptWindowText.AppendLine($"Msg(\"{GauntletDict["MSG"].Item2}\");\n");
            }

            ScriptWindowText.AppendLine("DirectorOptions <-\n{");

            foreach (var item in GauntletDict)
            {
                if (item.Value.Item1 && !Globals.GauntletDictBlackList.Contains(item.Key))
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

            ScriptWindowText.AppendLine("\n\tfunction RecalculateLimits()\n\t{\n\t\tlocal progressPct = Director.GetFurthestSurvivorFlow() / BridgeSpan;\n\t\tlocal speedPct = (Director.GetAveragedSurvivorSpeed() - MinSpeed) / (MaxSpeed - MinSpeed);\n\t\tif (progressPct < 0.0)\n\t\t\tprogressPct = 0.0;\n\t\tif (progressPct > 1.0)\n\t\t\tprogressPct = 1.0;\n\t\tMobSpawnSize = MobSpawnSizeMin + progressPct * (MobSpawnSizeMax - MobSpawnSizeMin);\n\t\tif (speedPct < 0.0)\n\t\t\tspeedPct = 0.0;\n\t\tif (speedPct > 1.0)\n\t\t\tspeedPct = 1.0;\n\t\tMobSpawnSize = MobSpawnSize + speedPct * SpeedPenaltyZAdds;\n\t\tCommonLimit = MobSpawnSize * 1.5;\n\t\tif (CommonLimit > CommonLimitMax)\n\t\t\tCommonLimit = CommonLimitMax;\n\t}\n}\nfunction Update()\n{\n\tDirectorOptions.RecalculateLimits();\n}");

            ScriptWindow.Text = ScriptWindowText.ToString();

            if (ScriptWindow.Text != string.Empty)
            {
                PasteToClipboardButton.IsEnabled = true;
                SaveAsNutButton.IsEnabled = true;
                SaveAsVmfButton.IsEnabled = true;
                CompileVmfButton.IsEnabled = true;
            }
            else
            {
                PasteToClipboardButton.IsEnabled = false;
                SaveAsNutButton.IsEnabled = false;
                SaveAsVmfButton.IsEnabled = false;
                CompileVmfButton.IsEnabled = false;
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

        private void SaveAsVmfClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Title = "导出vmf文件",
                Filter = "vmf文件 (*.vmf)|*.vmf",
                InitialDirectory = Globals.L4D2RootPath
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName == string.Empty)
            {
                return;
            }
            else
            {
                Functions.SaveVmfToPath(saveFileDialog.FileName, new List<string> { GauntletDict["info_director"].Item2, GauntletDict["trigger_finale"].Item2 }, 2);
            }

            YesOrNoWindow yesOrNoWindow = new()
            {
                TextBlockString = $"是否一并导出脚本文件至scripts\\vscripts文件夹?\n\n脚本文件名将为\"director_gauntlet.nut\"!",
                SendMessage = SaveToNutReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow.ShowDialog();

            if (IsNutConfirmed)
            {
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\director_gauntlet.nut", ScriptWindow.Text);
            }

            string navFileName = saveFileDialog.SafeFileName.Replace(".vmf", ".nav");

            YesOrNoWindow yesOrNoWindow2 = new()
            {
                TextBlockString = $"是否一并导出Nav文件至vmf所在文件夹?\n\nNav文件名将为\"{navFileName}\"!",
                SendMessage = SaveToNavReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow2.ShowDialog();

            if (IsNavConfirmed)
            {
                Functions.SaveNavToPath(saveFileDialog.FileName.Replace(".vmf", ".nav"), 2);
            }
        }

        private void CompileVmfClick(object sender, RoutedEventArgs e)
        {
            Functions.SaveVmfToPath($"{Globals.L4D2GauntletFinalePath}", new List<string> { GauntletDict["info_director"].Item2, GauntletDict["trigger_finale"].Item2 }, 2);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\gauntlet_finale.nut", ScriptWindow.Text);
            Functions.SaveNavToPath($"{Globals.L4D2MapsPath}\\gauntlet_finale.nav", 2);
            if (Functions.TryOpenCompileWindow(2))
            {
                File.Copy($"{Globals.L4D2GauntletFinalePath}.bsp", $"{Globals.L4D2MapsPath}\\gauntlet_finale.bsp", true);
                Functions.RunL4D2Game(2);
            }
        }

        private void PreviewOfficalScriptClick(object sender, RoutedEventArgs e)
        {
            PreviewScriptWindow previewScriptWindow = new()
            {
                Title = $"正在预览{MapSelectionComboBox.SelectedItem}的救援脚本",
                TextBoxString = Functions.GetOffcialGauntletScriptFile(),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            previewScriptWindow.ShowDialog();
        }
    }
}