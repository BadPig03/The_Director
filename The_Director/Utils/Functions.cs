using Steamworks;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using The_Director.Windows;

namespace The_Director.Utils;

public static class Functions
{
    public static string GetRandomString()
    {
        byte[] b = new byte[4];
        new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
        Random random = new(BitConverter.ToInt32(b, 0));
        string result = null, temp = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int i = 0; i < 16; i++)
            result += temp.Substring(random.Next(0, temp.Length - 1), 1);
        return result;
    } 

    public static bool IsProperInt(string value, int min, int max)
    {
        if (value == string.Empty)
            return false;
        if (!Regex.IsMatch(value, @"^\-?(\d+)?$"))
            return false;
        if (value == "-")
            value = "-1";
        if (int.Parse(value) <= max && int.Parse(value) >= min)
            return true;
        return false;
    }

    public static bool IsProperFloat(string value, float min, float max)
    {
        if (value == string.Empty)
            return false;
        if (value == ".")
            value = "0.0";
        if (Regex.IsMatch(value, @"^\d+\.$"))
            value = value.Insert(value.Length, "0");
        if (Regex.IsMatch(value, @"^\.\d+$"))
            value = value.Insert(0, "0");
        if (!Regex.IsMatch(value, @"^\d+(\.\d+)?$"))
            return false;
        if (float.Parse(value) <= max && float.Parse(value) >= min)
            return true;
        return false;
    }

    public static bool IsProperString(string value)
    {
        if (value == string.Empty)
            return true;
        if (value.Contains("!") || value.Contains("*") || value.Contains("\""))
            return false;
        return true;
    }

    public static int ConvertToInt(string value)
    {
        if (int.TryParse(value, out _))
            return int.Parse(value);
        else
            return -1;
    }

    public static string TotalWaveToString(int value)
    {
        switch(value)
        {
            case 0:
                return "PANIC";
            case 1:
                return "TANK";
            case 2:
                return "DELAY";
            case 3:
                return "SCRIPTED";
            default:
                return "";
        }
    }

    public static int TotalWaveToInt(string value)
    {
        switch (value)
        {
            case "PANIC":
                return 0;
            case "TANK":
                return 1;
            case "DELAY":
                return 2;
            case "SCRIPTED":
                return 3;
            default:
                return -1;
        }
    }

    public static void TryOpenMessageWindow(int type, bool flag = false)
    {
        var Title = string.Empty;
        var TextBoxString = string.Empty;

        if(!flag)
            switch (type)
            { 
                case -1:
                    Title = "错误";
                    TextBoxString = "未运行Steam!\n请运行Steam后启动本软件!";
                    break;
                case 0:
                    Title = "警告";
                    TextBoxString = "未设置尸潮阶段数!";
                    break;
                case 1:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入1到99的整数!";
                    break;
                case 2:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入0到1的浮点数!";
                    break;
                case 3:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入非负浮点数!";
                    break;
                case 4:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入非负整数!";
                    break;
                case 5:
                    Title = "错误";
                    TextBoxString = "非法输入!\n只能输入大于等于-1的整数!";
                    break;
                case 6:
                    Title = "错误";
                    TextBoxString = "非法输入!\n不能输入!、*或\"符号或为空!";
                    break;
                case 7:
                    Title = "提示";
                    TextBoxString = "已成功复制至粘贴板!";
                    break;
                case 8:
                    Title = "错误";
                    TextBoxString = "导出时出错!";
                    break;
                default:
                    break;
            }
        else
        {
            Title = "错误";
            TextBoxString = $"阶段{type + 1}未指定对应数据!";
        }

        MessageWindow messageWindow = new()
        {
            Title = Title,
            TextBoxString = TextBoxString,
            Owner = Application.Current.MainWindow,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        messageWindow.ShowDialog();
    }

    public static int TextBoxIndex(string name)
    {
        switch(name)
        {
            case "TotalWave":
                return 0;
            case "IntensityRelaxThreshold":
                return 1;
            case "BuildUpMinInterval":
            case "MobRechargeRate":
            case "MobSpawnMaxTime":
            case "MobSpawnMinTime":
            case "RelaxMaxFlowTravel":
            case "RelaxMaxInterval":
            case "RelaxMinInterval":
            case "SpecialRespawnInterval":
            case "SustainPeakMaxTime":
            case "SustainPeakMinTime":
            case "MinimumStageTime":
                return 2;
            case "MusicDynamicMobScanStopSize":
            case "MusicDynamicMobSpawnSize":
            case "MusicDynamicMobStopSize":
            case "BileMobSize":
            case "MegaMobSize":
            case "MobMaxSize":
            case "MobMinSize":
            case "MobSpawnSize":
            case "BoomerLimit":
            case "ChargerLimit":
            case "CommonLimit":
            case "DominatorLimit":
            case "HunterLimit":
            case "JockeyLimit":
            case "MaxSpecials":
            case "SmokerLimit":
            case "SpitterLimit":
                return 3;
            case "MobMaxPending":
            case "TankLimit":
            case "WitchLimit":
            case "HordeEscapeCommonLimit":
                return 4;
            case "info_director":
            case "trigger_finale":
            case "ScriptFile":
                return 5;
            case "FirstUseDelay":
            case "UseDelay":
                return 6;
            default:
                return -1;
        }
    }

    public static string GetButtonString(string name)
    {
        switch(name)
        {
            case "BuildUpMinInterval":
                return "BuildUpMinInterval的值代表节奏中BUILD_UP最短持续秒数。\n\n有效范围为非负整数。\n\n默认值为15。";
            case "IntensityRelaxThreshold":
                return "IntensityRelaxThreshold的值代表所有生还者的紧张度都必须小于多少才能让节奏从SUSTAIN_PEAK切换为RELAX。\n\n有效范围为0-1的浮点数。\n\n默认值为0.9。";
            case "LockTempo":
                return "设置LockTempo为true会使得导演无延迟地生成尸潮。\n\n默认值为false。";
            case "MobRechargeRate":
                return "MobRechargeRate的值代表一次尸潮内生成下一个普通感染者的速度。\n\n有效范围为非负浮点数。\n\n默认值为0.0025。";
            case "MobSpawnMaxTime":
                return "MobSpawnMaxTime的值代表两波尸潮生成的最大时间隔秒数。\n\n有效范围为非负浮点数。\n\n默认值根据难度变化，为180.0-240.0。";
            case "MobSpawnMinTime":
                return "MobSpawnMinTime的值代表两波尸潮生成的最小时间隔秒数。\n\n有效范围为非负浮点数。\n\n默认值根据难度变化，为90.0-120.0。";
            case "MusicDynamicMobScanStopSize":
                return "MusicDynamicMobScanStopSize的值代表尸潮的大小不足此数时会停止背景音乐。\n\n有效范围为非负整数。\n\n默认值为3。";
            case "MusicDynamicMobSpawnSize":
                return "MusicDynamicMobSpawnSize的值代表尸潮的大小达到此数时会开始播放背景音乐。\n\n有效范围为非负整数。\n\n默认值为25。";
            case "MusicDynamicMobStopSize":
                return "MusicDynamicMobStopSize的值代表尸潮的大小达到此数时会停止背景音乐。\n\n有效范围为非负整数。\n\n默认值为8。";
            case "PreferredMobDirection":
                return "PreferredMobDirection的值代表尸潮生成的方位。\n\n有效范围为-1到10的整数。\n\n默认值为SPAWN_NO_PREFERENCE。";
            case "PreferredSpecialDirection":
                return "PreferredSpecialDirection的值代表特感生成的方位。\n\n有效范围为-1到10的整数。\n\n默认值为SPAWN_NO_PREFERENCE。";
            case "RelaxMaxFlowTravel":
                return "RelaxMaxFlowTravel的值代表生还者最远能前进多少距离就会让节奏从RELAX切换到BUILD_UP。\n\n有效范围为非负浮点数。\n\n默认值为3000。";
            case "RelaxMaxInterval":
                return "RelaxMaxInterval的值代表节奏中RELAX最长持续秒数。\n\n有效范围为非负浮点数。\n\n默认值为45。";
            case "RelaxMinInterval":
                return "RelaxMinInterval的值代表节奏中RELAX最短持续秒数。\n\n有效范围为非负浮点数。\n\n默认值为30。";
            case "MinimumStageTime":
                return "MinimumStageTime的值代表救援的脚本阶段在结束前最少可持续运行秒数。\n\n对脚本尸潮无效。\n\n有效范围为非负浮点数。\n\n默认值为1.0。";
            case "ProhibitBosses":
                return "设置ProhibitBosses为true会防止Tank和Witch生成。\n\n默认值为false。";
            case "ShouldAllowMobsWithTank":
                return "设置ShouldAllowMobsWithTank为true会允许在Tank在场时生成小僵尸。\n\nBoomer和胆汁炸弹引起的尸潮不受影响。\n\n仅适用于战役模式。\n\n默认值为false。";
            case "ShouldAllowSpecialsWithTank":
                return "设置ShouldAllowSpecialsWithTank为true会允许在Tank在场时生成特殊感染者。\n\n仅适用于战役模式。\n\n默认值为false。";
            case "EscapeSpawnTanks":
                return "设置EscapeSpawnTanks为true会允许Tank在救援的逃离阶段无限生成。\n\n默认值为true。";
            case "SpecialRespawnInterval":
                return "SpecialRespawnInterval的值代表特殊感染者重生所需要的秒数。\n\n有效范围为非负浮点数。\n\n默认值为：战役模式为45，对抗模式为20。";
            case "SustainPeakMaxTime":
                return "SustainPeakMaxTime的值代表节奏中SUSTAIN_PEAK的最长持续分钟数。\n\n有效范围为非负浮点数。\n\n默认值为5。";
            case "SustainPeakMinTime":
                return "SustainPeakMinTime的值代表节奏中SUSTAIN_PEAK的最短持续分钟数。\n\n有效范围为非负浮点数。\n\n默认值为3。";
            case "BileMobSize":
                return "BileMobSize的值代表Boomer和胆汁炸弹引起的普通感染者数量最大值。\n\n有效范围为非负整数。\n\n无默认值。";
            case "BoomerLimit":
                return "BoomerLimit的值代表在场的Boomer最大数量。\n\n有效范围为非负整数。\n\n默认值为1。";
            case "ChargerLimit":
                return "ChargerLimit的值代表在场的Charger最大数量。\n\n有效范围为非负整数。\n\n默认值为1。";
            case "CommonLimit":
                return "CommonLimit的值代表在场的普通感染者最大数量。\n\n有效范围为非负整数。\n\n默认值为30。";
            case "DominatorLimit":
                return "DominatorLimit的值代表在场的控制型特殊感染者(Hunter, Jockey, Charger, Smoker)最大数量。\n\n有效范围为非负整数。\n\n无默认值。";
            case "HunterLimit":
                return "HunterLimit的值代表在场的Hunter最大数量。\n\n有效范围为非负整数。\n\n默认值为1。";
            case "JockeyLimit":
                return "JockeyLimit的值代表在场的Jockey最大数量。\n\n有效范围为非负整数。\n\n默认值为1。";
            case "MaxSpecials":
                return "MaxSpecials的值代表在场的特殊感染者最大数量。\n\n有效范围为非负整数。\n\n默认值为2。";
            case "MegaMobSize":
                return "MegaMobSize的值代表一次尸潮能生成的普通感染者最大数量。\n\n有效范围为非负整数。\n\n无默认值。";
            case "MobMaxPending":
                return "MobMaxPending的值代表当尸潮的普通感染者数量超过CommonLimit时最多有多少普通感染者可以暂时等待生成。\n\n有效范围为整数。\n\n默认值为-1。";
            case "MobMaxSize":
                return "MobMaxSize的值代表一次尸潮生成普通感染者的最大数量。\n\n有效范围为非负整数。\n\n默认值为30。";
            case "MobMinSize":
                return "MobMinSize的值代表一次尸潮生成普通感染者的最小数量。\n\n有效范围为非负整数。\n\n默认值为10。";
            case "MobSpawnSize":
                return "MobSpawnSize的值代表一次尸潮生成普通感染者的数量。\n\n覆盖MobMaxSize与MobMinSize。\n\n有效范围为非负整数。\n\n无默认值。";
            case "SmokerLimit":
                return "SmokerLimit的值代表代表在场的Smoker最大数量。\n\n有效范围为非负整数。\n\n默认值为1。";
            case "SpitterLimit":
                return "SpitterLimit的值代表代表在场的Spitter最大数量。\n\n有效范围为非负整数。\n\n默认值为1。";
            case "TankLimit":
                return "TankLimit的值代表代表在场的Tank最大数量。\n\n有效范围为大于等于-1的整数。\n\n小于0则代表无限制。\n\n默认值为-1。";
            case "WitchLimit":
                return "WitchLimit的值代表代表在场的Witch最大数量。\n\n有效范围为大于等于-1的整数。\n\n小于0则代表无限制。\n\n默认值为-1。";
            case "HordeEscapeCommonLimit":
                return "HordeEscapeCommonLimit的值代表救援的逃离阶段可生成的普通感染者最大数量。\n\n有效范围为大于等于-1的整数。\n\n默认值为-1。";
            case "info__director":
                return "info_director是一个可以通过使用输入输出控制部分导演行为的点实体。\n\n这个文本框的值将决定info_director的名字(targetname)。";
            case "trigger__finale":
                return "trigger_finale是一个可以触发当前地图的救援的点实体。\n\n这个文本框的值将决定trigger_finale的名字(targetname)。";
            case "Script File":
                return "ScriptFile是trigger_finale的一个键值。\n\n这个文本框的值会决定如果trigger_finale使用Custom救援类型后将会使用的救援脚本名字(带后缀名.nut)。";
            case "First Use Delay":
                return "First Use Delay是trigger_finale的一个键值。\n\n这个文本框的值会决定第一次触发trigger_finale后需要经过多少秒后才能再次触发。";
            case "Use Delay":
                return "Use Delay是trigger_finale的一个键值。\n\n若First Use Delay的值为0，则这个文本框的值会决定触发trigger_finale后需要经过多少秒后才开始救援。\n\n若First Use Delay的值不为0，则这个文本框的值会决定第二次触发trigger_finale后需要经过多少秒后才开始救援。";
            default:
                return "";
        } 
    }

    public static string GetButtonHyperlinkUri(string name)
    {
        switch(name)
        {
            case "info__director":
                return "https://developer.valvesoftware.com/wiki/Info_director";
            case "trigger__finale":
                return "https://developer.valvesoftware.com/wiki/Trigger_finale";
            case "ScriptFile":
                return "https://developer.valvesoftware.com/wiki/Trigger_finale#Keyvalues";
            default:
                return "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions";
        }
    }

    public static string GetProcessInput(int type)
    {
        switch(type)
        {
            case 0:
                return $"\"{Globals.L4D2VBSPPath}\" -game \"{Globals.L4D2GameInfoPath}\" \"{Globals.L4D2StandardFinalePath}.vmf\"&exit";
            case 1:
                return $"\"{Globals.L4D2VVISPath}\" -game \"{Globals.L4D2GameInfoPath}\" \"{Globals.L4D2StandardFinalePath}.bsp\"&exit";
            case 2:
                return $"\"{Globals.L4D2VRADPath}\" -game \"{Globals.L4D2GameInfoPath}\" -hdr -StaticPropLighting -StaticPropPolys \"{Globals.L4D2StandardFinalePath}.bsp\"&exit";
            default:
                return "&exit";
        }
    }

    public static bool GenerateNewProcess(int type)
    {
        Process process = new();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardInput = true;
        process.Start();
        process.StandardInput.WriteLine(GetProcessInput(type));
        process.StandardInput.AutoFlush = true;
        process.WaitForExit();
        process.Close();
        return true;
    }

    public static void RunL4D2Game()
    {
        Process process = new();
        process.StartInfo.FileName = $"{Globals.L4D2RootPath}\\left4dead2.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Arguments = "-steam -novid +sv_cheats 1 +director_debug 1 +map standard_finale";
        process.Start();
        process.WaitForExit();
        process.Close();
    }

    public static string GetOffcialScriptFile(int index)
    {
        switch(index)
        {
            case 0:
                return Properties.Resources.c2m5_concert_finale;
            case 1:
                return Properties.Resources.c3m4_plantation_finale;
            case 2:
                return Properties.Resources.c4m5_milltown_escape_finale;
            case 3:
                return Properties.Resources.c8m5_rooftop_finale;
            case 4:
                return Properties.Resources.c9m2_lots_finale;
            case 5:
                return Properties.Resources.c10m5_houseboat_finale;
            case 6:
                return Properties.Resources.c11m5_runway_finale;
            case 7:
                return Properties.Resources.c12m5_cornfield_finale;
            default:
                return string.Empty;
        }
    }
}