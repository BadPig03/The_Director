using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class GauntletRescueSettings : UserControl
    {
        public Dictionary<string, BooleanString> RescueCheckButtons = new();


        public GauntletRescueSettings()
        {
            InitializeComponent();

            RescueCheckButtons.Add("msg", new BooleanString(false, ""));
            RescueCheckButtons.Add("prohibitboss", new BooleanString(false, null));
            RescueCheckButtons.Add("showstage", new BooleanString(false, null));
            RescueCheckButtons.Add("locktempo", new BooleanString(false, null));
            RescueCheckButtons.Add("nomobspawns", new BooleanString(false, null));
            RescueCheckButtons.Add("shouldallowmobswithtank", new BooleanString(false, null));
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
        private void LockTempoButtonClick(object sender, RoutedEventArgs e)
        {
            HintWindow metroWindow = new HintWindow
            {
                Width = 480,
                Height = 320,
                TextBlockString = "设置LockTempo = true会无延迟地生成尸潮。",
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
                TextBlockString = "设置NoMobSpawns = true会停止新的僵尸生成。\n\n原有暂时等待生成的僵尸仍会继续生成。\n\n不会重置生成计时器。",
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
                TextBlockString = "设置ShouldAllowMobsWithTank = true会允许在Tank在场时自然生成小僵尸。\n\nBoomer和胆汁炸弹引起的尸潮不受影响。\n\n仅适用于战役模式。",
                HyperlinkUri = "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions"
            };
            metroWindow.ShowDialog();
        }

        private void UpdateScriptWindow()
        {
            var ScriptWindowText = "";
            if (RescueCheckButtons["msg"].Item1)
                ScriptWindowText += $"Msg(\"{RescueCheckButtons["msg"].Item2}\");\n\n";

            ScriptWindowText += "DirectorOptions <-\n{\n";

            if (RescueCheckButtons["prohibitboss"].Item1)
                ScriptWindowText += "\tProhibitBosses = true\n\n";

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
    }
}