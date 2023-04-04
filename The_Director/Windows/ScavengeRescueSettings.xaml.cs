﻿using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class ScavengeRescueSettings : UserControl
    {
        public bool IsNutConfirmed = new();
        public bool IsNavConfirmed = new();

        public Dictionary<int, string> TextBoxDicts = new();
        public Dictionary<int, int> ComboBoxDicts = new();
        public Dictionary<string, BooleanString> ScavengeDict = new();

        public List<string> VmfValuesList = new() { "director", "finale_radio", "scavenge_finale.nut", "2", "1" };

        public void MSGReceived(string value)
        {
            if (value != null)
            {
                ScavengeDict["MSG"] = (true, value);
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

        public ScavengeRescueSettings()
        {
            InitializeComponent();
            PreferredMobDirectionComboBox.ItemsSource = Globals.PreferredMobDirectionList;
            PreferredSpecialDirectionComboBox.ItemsSource = Globals.PreferredSpecialDirectionList;
            MapSelectionComboBox.ItemsSource = Globals.OffcialMapScavengeRescueList;
            MapSelectionComboBox.SelectedIndex = 0;
            ScavengeDict.Add("MSG", new BooleanString(false, string.Empty));
            ScavengeDict.Add("ShowStage", new BooleanString(false, null));
            ScavengeDict.Add("LockTempo", new BooleanString(false, null));
            ScavengeDict.Add("IntensityRelaxThreshold", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobRechargeRate", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobSpawnMaxTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobSpawnMinTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MusicDynamicMobScanStopSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MusicDynamicMobSpawnSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MusicDynamicMobStopSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("PreferredMobDirection", new BooleanString(false, string.Empty));
            ScavengeDict.Add("RelaxMaxFlowTravel", new BooleanString(false, string.Empty));
            ScavengeDict.Add("RelaxMaxInterval", new BooleanString(false, string.Empty));
            ScavengeDict.Add("RelaxMinInterval", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MinimumStageTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("PreferredSpecialDirection", new BooleanString(false, string.Empty));
            ScavengeDict.Add("ProhibitBosses", new BooleanString(false, null));
            ScavengeDict.Add("ShouldAllowMobsWithTank", new BooleanString(false, null));
            ScavengeDict.Add("ShouldAllowSpecialsWithTank", new BooleanString(false, null));
            ScavengeDict.Add("EscapeSpawnTanks", new BooleanString(true, null));
            ScavengeDict.Add("SpecialRespawnInterval", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SustainPeakMaxTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SustainPeakMinTime", new BooleanString(false, string.Empty));
            ScavengeDict.Add("BileMobSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("BoomerLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("ChargerLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("CommonLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("DominatorLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("HunterLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("JockeyLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MaxSpecials", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MegaMobSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobMaxPending", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobMaxSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobMinSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("MobSpawnSize", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SmokerLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("SpitterLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("TankLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("WitchLimit", new BooleanString(false, string.Empty));
            ScavengeDict.Add("HordeEscapeCommonLimit", new BooleanString(false, string.Empty));

            info_directorTextBox.Text = VmfValuesList[0];
            trigger_finaleTextBox.Text = VmfValuesList[1];
            ScriptFileTextBox.Text = VmfValuesList[2];
            FirstUseDelayTextBox.Text = VmfValuesList[3];
            UseDelayTextBox.Text = VmfValuesList[4];
        }

        private void TryOpenMSGWindow()
        {
            InputNewText inputNewText = new()
            {
                TextBoxText = ScavengeDict["MSG"].Item2,
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
                ScavengeDict[Name] = (IsChecked, null);
            }
            else
            {
                if (!ScavengeDict[Name].Item1 && IsChecked)
                {
                    TryOpenMSGWindow();
                }

                ScavengeDict[Name] = (IsChecked, ScavengeDict[Name].Item2);
            }
            UpdateScriptWindow();
        }

        private void TextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            var Name = textBox.Name.Remove(textBox.Name.Length - 7, 7);
            switch (Functions.TextBoxIndex(Name))
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
                    if (Name == "info_director")
                    {
                        VmfValuesList[0] = textBox.Text;
                    }
                    else if (Name == "trigger_finale")
                    {
                        VmfValuesList[1] = textBox.Text;
                    }
                    else if (Name == "ScriptFile")
                    {
                        VmfValuesList[2] = textBox.Text;
                    }

                    break;
                case 6:
                    if (textBox.Text == string.Empty || !Functions.IsProperInt(textBox.Text, 0, int.MaxValue))
                    {
                        Functions.TryOpenMessageWindow(4);
                        if (Name == "FirstUseDelay" || Name == "UseDelay")
                        {
                            textBox.Text = "1";
                        }

                        return;
                    }
                    if (Name == "FirstUseDelay")
                    {
                        VmfValuesList[3] = textBox.Text;
                    }
                    else if (Name == "UseDelay")
                    {
                        VmfValuesList[4] = textBox.Text;
                    }

                    break;
                default:
                    break;
            }
            if (Name != "TotalWave" || Name != "MSG")
            {
                ScavengeDict[Name] = (textBox.Text != string.Empty, textBox.Text);
            }

            UpdateScriptWindow();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ScavengeDict[comboBox.Name.Remove(comboBox.Name.Length - 8, 8)] = (comboBox.SelectedItem.ToString() != string.Empty, comboBox.SelectedItem.ToString());
            UpdateScriptWindow();
        }

        private void UpdateScriptWindow()
        {
            StringBuilder ScriptWindowText = new();

            if ((bool)MSGCheckBox.IsChecked)
            {
                ScriptWindowText.AppendLine($"Msg(\"{ScavengeDict["MSG"].Item2}\");\n");
            }

            ScriptWindowText.AppendLine("PANIC <- 0\nTANK <- 1\nDELAY <- 2\nSCRIPTED <- 3\n\nDirectorOptions <-\n{");

            ScriptWindowText.AppendLine("\tA_CustomFinale_StageCount = 31\n");

            foreach (var item in ScavengeDict)
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

            if (ScavengeDict["ShowStage"].Item1)
            {
                ScriptWindowText.AppendLine("\nfunction OnBeginCustomFinaleStage(num, type)\n{\n\tprintl(\"Beginning custom finale stage \" + num + \" of type \"+ type);\n}");
            }

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
                TextBoxString = Functions.GetButtonString(label.Content.ToString()),
                HyperlinkUri = Functions.GetButtonHyperlinkUri(label.Content.ToString()),
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
                Functions.SaveVmfToPath(saveFileDialog.FileName, VmfValuesList, 1);
            }

            string fileExtension = VmfValuesList[2].EndsWith(".nut") ? string.Empty : ".nut";

            YesOrNoWindow yesOrNoWindow = new()
            {
                TextBlockString = $"是否一并导出脚本文件至scripts\\vscripts文件夹?\n\n脚本文件名将为\"{VmfValuesList[2]}{fileExtension}\"!",
                SendMessage = SaveToNutReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow.ShowDialog();

            if (IsNutConfirmed)
            {
                Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\{VmfValuesList[2]}", ScriptWindow.Text);
            }

            string NavFileName = saveFileDialog.SafeFileName.Replace(".vmf", ".nav");

            YesOrNoWindow yesOrNoWindow2 = new()
            {
                TextBlockString = $"是否一并导出Nav文件至vmf所在文件夹?\n\nNav文件名将为\"{NavFileName}\"!",
                SendMessage = SaveToNavReceived,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            yesOrNoWindow2.ShowDialog();

            if (IsNavConfirmed)
            {
                Functions.SaveNavToPath(saveFileDialog.FileName.Replace(".vmf", ".nav"));
            }
        }

        private void CompileVmfClick(object sender, RoutedEventArgs e)
        {
            Functions.SaveVmfToPath($"{Globals.L4D2StandardFinalePath}", VmfValuesList, 1);
            Functions.SaveNutToPath($"{Globals.L4D2ScriptsPath}\\standard_finale.nut", ScriptWindow.Text);
            Functions.SaveNavToPath($"{Globals.L4D2MapsPath}\\standard_finale.nav");
            if (Functions.GenerateNewProcess(0))
            {
                if (Functions.GenerateNewProcess(1))
                {
                    if (Functions.GenerateNewProcess(2))
                    {
                        File.Copy($"{Globals.L4D2StandardFinalePath}.bsp", $"{Globals.L4D2MapsPath}\\standard_finale.bsp", true);
                        Functions.RunL4D2Game();
                    }
                }
            }
        }

        private void PreviewOfficalScriptClick(object sender, RoutedEventArgs e)
        {
            PreviewScriptWindow previewScriptWindow = new()
            {
                Title = $"正在预览{MapSelectionComboBox.SelectedItem}的救援脚本",
                TextBoxString = Functions.GetOffcialScavengeScriptFile(MapSelectionComboBox.SelectedIndex),
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            previewScriptWindow.ShowDialog();
        }
    }
}