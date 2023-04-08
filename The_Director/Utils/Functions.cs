using System;
using System.Collections.Generic;
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
        {
            result += temp.Substring(random.Next(0, temp.Length - 1), 1);
        }

        return result;
    } 

    public static bool IsProperInt(string value, int min, int max)
    {
        if (value == string.Empty)
        {
            return false;
        }

        if (!Regex.IsMatch(value, @"^\-?(\d+)?$"))
        {
            return false;
        }

        if (value == "-")
        {
            value = "-1";
        }

        if (int.Parse(value) <= max && int.Parse(value) >= min)
        {
            return true;
        }

        return false;
    }

    public static bool IsProperFloat(string value, float min, float max)
    {
        if (value == string.Empty)
        {
            return false;
        }

        if (value == ".")
        {
            value = "0.0";
        }

        if (Regex.IsMatch(value, @"^\d+\.$"))
        {
            value = value.Insert(value.Length, "0");
        }

        if (Regex.IsMatch(value, @"^\.\d+$"))
        {
            value = value.Insert(0, "0");
        }

        if (!Regex.IsMatch(value, @"^\d+(\.\d+)?$"))
        {
            return false;
        }

        if (float.Parse(value) <= max && float.Parse(value) >= min)
        {
            return true;
        }

        return false;
    }

    public static bool IsProperString(string value)
    {
        if (value == string.Empty)
        {
            return true;
        }

        if (value.Contains("!") || value.Contains("*") || value.Contains("\""))
        {
            return false;
        }

        return true;
    }

    public static int ConvertToInt(string value)
    {
        if (int.TryParse(value, out _))
        {
            return int.Parse(value);
        }
        else
        {
            return -1;
        }
    }

    public static string TotalWaveToString(int value)
    {
        return value switch
        {
            0 => "PANIC",
            1 => "TANK",
            2 => "DELAY",
            3 => "SCRIPTED",
            _ => "",
        };
    }

    public static int TotalWaveToInt(string value)
    {
        return value switch
        {
            "PANIC" => 0,
            "TANK" => 1,
            "DELAY" => 2,
            "SCRIPTED" => 3,
            _ => -1,
        };
    }

    public static void TryOpenMessageWindow(int type, bool flag = false)
    {
        var Title = string.Empty;
        var TextBoxString = string.Empty;

        if(!flag)
        {
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

    public static bool TryOpenCompileWindow(int num)
    {
        CompileWindow compileWindow = new ()
        {
            Num = num,
            Owner = Application.Current.MainWindow,
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        compileWindow.ShowDialog();
        return true;
    }

    public static int TextBoxIndex(string name)
    {
        return name switch
        {
            "TotalWave" => 0,
            "IntensityRelaxThreshold" => 1,
            "BuildUpMinInterval" or "MobRechargeRate" or "MobSpawnMaxTime" or "MobSpawnMinTime" or "RelaxMaxFlowTravel" or "RelaxMaxInterval" or "RelaxMinInterval" or "SpecialRespawnInterval" or "SustainPeakMaxTime" or "SustainPeakMinTime" or "MinimumStageTime" or "CustomTankKiteDistance" or "GauntletMovementThreshold" or "GauntletMovementTimerLength" or "GauntletMovementBonus" or "GauntletMovementBonusMax" => 2,
            "MusicDynamicMobScanStopSize" or "MusicDynamicMobSpawnSize" or "MusicDynamicMobStopSize" or "BileMobSize" or "MegaMobSize" or "MobMaxSize" or "MobMinSize" or "MobSpawnSize" or "BoomerLimit" or "ChargerLimit" or "CommonLimit" or "DominatorLimit" or "HunterLimit" or "JockeyLimit" or "MaxSpecials" or "SmokerLimit" or "SpitterLimit" or "CansNeeded" or "DelayMin" or "DelayMax" or "DelayPourThre" or "DelayBothThre" or "AbortMin" or "AbortMax" or "CansBothThre" or "BridgeSpan" or "MinSpeed" or "MaxSpeed" or "SpeedPenaltyZAdds" or "CommonLimitMax" or "PreTankMobMax" => 3,
            "MobMaxPending" or "TankLimit" or "WitchLimit" or "HordeEscapeCommonLimit" => 4,
            "info_director" or "trigger_finale" or "ScriptFile" or "DelayScript" => 5,
            "FirstUseDelay" or "UseDelay" => 6,
            _ => -1,
        };
    }

    public static string GetButtonString(string name)
    {
        return name switch
        {
            "BuildUpMinInterval" => "BuildUpMinInterval的值代表节奏中BUILD_UP最短持续秒数。\n\n有效范围为非负整数。\n\n默认值为15。",
            "IntensityRelaxThreshold" => "IntensityRelaxThreshold的值代表所有生还者的紧张度都必须小于多少才能让节奏从SUSTAIN_PEAK切换为RELAX。\n\n有效范围为0-1的浮点数。\n\n默认值为0.9。",
            "LockTempo" => "设置LockTempo为true会使得导演无延迟地生成尸潮。\n\n默认值为false。",
            "MobRechargeRate" => "MobRechargeRate的值代表一次尸潮内生成下一个普通感染者的速度。\n\n有效范围为非负浮点数。\n\n默认值为0.0025。",
            "MobSpawnMaxTime" => "MobSpawnMaxTime的值代表两波尸潮生成的最大时间隔秒数。\n\n有效范围为非负浮点数。\n\n默认值根据难度变化，为180.0-240.0。",
            "MobSpawnMinTime" => "MobSpawnMinTime的值代表两波尸潮生成的最小时间隔秒数。\n\n有效范围为非负浮点数。\n\n默认值根据难度变化，为90.0-120.0。",
            "MusicDynamicMobScanStopSize" => "MusicDynamicMobScanStopSize的值代表尸潮的大小不足此数时会停止背景音乐。\n\n有效范围为非负整数。\n\n默认值为3。",
            "MusicDynamicMobSpawnSize" => "MusicDynamicMobSpawnSize的值代表尸潮的大小达到此数时会开始播放背景音乐。\n\n有效范围为非负整数。\n\n默认值为25。",
            "MusicDynamicMobStopSize" => "MusicDynamicMobStopSize的值代表尸潮的大小达到此数时会停止背景音乐。\n\n有效范围为非负整数。\n\n默认值为8。",
            "PreferredMobDirection" => "PreferredMobDirection的值代表尸潮生成的方位。\n\n有效范围为-1到10的整数。\n\n默认值为SPAWN_NO_PREFERENCE。",
            "PreferredSpecialDirection" => "PreferredSpecialDirection的值代表特感生成的方位。\n\n有效范围为-1到10的整数。\n\n默认值为SPAWN_NO_PREFERENCE。",
            "RelaxMaxFlowTravel" => "RelaxMaxFlowTravel的值代表生还者最远能前进多少距离就会让节奏从RELAX切换到BUILD_UP。\n\n有效范围为非负浮点数。\n\n默认值为3000。",
            "RelaxMaxInterval" => "RelaxMaxInterval的值代表节奏中RELAX最长持续秒数。\n\n有效范围为非负浮点数。\n\n默认值为45。",
            "RelaxMinInterval" => "RelaxMinInterval的值代表节奏中RELAX最短持续秒数。\n\n有效范围为非负浮点数。\n\n默认值为30。",
            "MinimumStageTime" => "MinimumStageTime的值代表救援的脚本阶段在结束前最少可持续运行秒数。\n\n对脚本尸潮无效。\n\n有效范围为非负浮点数。\n\n默认值为1.0。",
            "ProhibitBosses" => "设置ProhibitBosses为true会防止Tank和Witch生成。\n\n默认值为false。",
            "ShouldAllowMobsWithTank" => "设置ShouldAllowMobsWithTank为true会允许在Tank在场时生成小僵尸。\n\nBoomer和胆汁炸弹引起的尸潮不受影响。\n\n仅适用于战役模式。\n\n默认值为false。",
            "ShouldAllowSpecialsWithTank" => "设置ShouldAllowSpecialsWithTank为true会允许在Tank在场时生成特殊感染者。\n\n仅适用于战役模式。\n\n默认值为false。",
            "EscapeSpawnTanks" => "设置EscapeSpawnTanks为true会允许Tank在救援的逃离阶段无限生成。\n\n默认值为true。",
            "SpecialRespawnInterval" => "SpecialRespawnInterval的值代表特殊感染者重生所需要的秒数。\n\n有效范围为非负浮点数。\n\n默认值为：战役模式为45，对抗模式为20。",
            "SustainPeakMaxTime" => "SustainPeakMaxTime的值代表节奏中SUSTAIN_PEAK的最长持续分钟数。\n\n有效范围为非负浮点数。\n\n默认值为5。",
            "SustainPeakMinTime" => "SustainPeakMinTime的值代表节奏中SUSTAIN_PEAK的最短持续分钟数。\n\n有效范围为非负浮点数。\n\n默认值为3。",
            "BileMobSize" => "BileMobSize的值代表Boomer和胆汁炸弹引起的普通感染者数量最大值。\n\n有效范围为非负整数。\n\n无默认值。",
            "BoomerLimit" => "BoomerLimit的值代表在场的Boomer最大数量。\n\n有效范围为非负整数。\n\n默认值为1。",
            "ChargerLimit" => "ChargerLimit的值代表在场的Charger最大数量。\n\n有效范围为非负整数。\n\n默认值为1。",
            "CommonLimit" => "CommonLimit的值代表在场的普通感染者最大数量。\n\n有效范围为非负整数。\n\n默认值为30。",
            "DominatorLimit" => "DominatorLimit的值代表在场的控制型特殊感染者(Hunter, Jockey, Charger, Smoker)最大数量。\n\n有效范围为非负整数。\n\n无默认值。",
            "HunterLimit" => "HunterLimit的值代表在场的Hunter最大数量。\n\n有效范围为非负整数。\n\n默认值为1。",
            "JockeyLimit" => "JockeyLimit的值代表在场的Jockey最大数量。\n\n有效范围为非负整数。\n\n默认值为1。",
            "MaxSpecials" => "MaxSpecials的值代表在场的特殊感染者最大数量。\n\n有效范围为非负整数。\n\n默认值为2。",
            "MegaMobSize" => "MegaMobSize的值代表一次尸潮能生成的普通感染者最大数量。\n\n有效范围为非负整数。\n\n无默认值。",
            "MobMaxPending" => "MobMaxPending的值代表当尸潮的普通感染者数量超过CommonLimit时最多有多少普通感染者可以暂时等待生成。\n\n有效范围为整数。\n\n默认值为-1。",
            "MobMaxSize" => "MobMaxSize的值代表一次尸潮生成普通感染者的最大数量。\n\n有效范围为非负整数。\n\n默认值为30。",
            "MobMinSize" => "MobMinSize的值代表一次尸潮生成普通感染者的最小数量。\n\n有效范围为非负整数。\n\n默认值为10。",
            "MobSpawnSize" => "MobSpawnSize的值代表一次尸潮生成普通感染者的数量。\n\n覆盖MobMaxSize与MobMinSize。\n\n有效范围为非负整数。\n\n无默认值。",
            "SmokerLimit" => "SmokerLimit的值代表代表在场的Smoker最大数量。\n\n有效范围为非负整数。\n\n默认值为1。",
            "SpitterLimit" => "SpitterLimit的值代表代表在场的Spitter最大数量。\n\n有效范围为非负整数。\n\n默认值为1。",
            "TankLimit" => "TankLimit的值代表代表在场的Tank最大数量。\n\n有效范围为大于等于-1的整数。\n\n小于0则代表无限制。\n\n默认值为-1。",
            "WitchLimit" => "WitchLimit的值代表代表在场的Witch最大数量。\n\n有效范围为大于等于-1的整数。\n\n小于0则代表无限制。\n\n默认值为-1。",
            "HordeEscapeCommonLimit" => "HordeEscapeCommonLimit的值代表救援的逃离阶段可生成的普通感染者最大数量。\n\n有效范围为大于等于-1的整数。\n\n默认值为-1。",
            "CansNeeded" => "CansNeeded的值代表灌油救援需要灌入发动机的总油桶数量。\n\n有效范围为非负整数。\n\n默认值为12。",
            "DelayMin" => "DelayMin的值代表灌油救援中SCRIPTED阶段自动结束前最少得经过的秒数。\n\n有效范围为非负整数。\n\n默认值为10。",
            "DelayMax" => "DelayMax的值代表灌油救援中SCRIPTED阶段自动结束前最多经过的秒数。\n\n有效范围为非负整数。\n\n默认值为20。",
            "DelayPourThre" => "DelayPourThre的值代表灌油救援中SCRIPTED阶段被强制中断前生还者最多能灌入发动机的油桶数量。\n\n有效范围为非负整数。\n\n默认值为1。",
            "DelayBothThre" => "DelayBothThre的值代表灌油救援中SCRIPTED阶段被强制中断前生还者最多能灌入发动机的油桶数量和捡起来过的油桶数量之和。\n\n有效范围为非负整数。\n\n默认值为2。",
            "AbortMin" => "AbortMin的值代表强制中断救援阶段的SCRIPTED阶段后在进入下一个阶段前最少得经过的秒数。\n\n有效范围为非负整数。\n\n默认值为1。",
            "AbortMax" => "AbortMax的值代表强制中断救援阶段的SCRIPTED阶段后在进入下一个阶段前最多经过的秒数。\n\n有效范围为非负整数。\n\n默认值为3。",
            "CansBothThre" => "当生还者灌入发动机的油桶数量和捡起来过的油桶数量之和可以被CansBothThre的值整除时，会强行中断当前的SCRIPTED阶段并立刻进入下一阶段。\n\n有效范围为非负整数。\n\n默认值为4。",
            "PanicForever" => "设置PanicForever为true会使得尸潮无限生成。\n\n默认值为false。",
            "PausePanicWhenRelaxing" => "设置PausePanicWhenRelaxing为true会使得尸潮在RELAX节奏时暂停生成。\n\n默认值为false。",
            "GauntletMovementThreshold" => "GauntletMovementThreshold的值代表前进奖励、前进计时器和当前奖励被重置成默认值时生还者从此时的位置到起点的Nav流距离之差。\n\n有效范围为非负浮点数。\n\n默认值为500.0。",
            "GauntletMovementTimerLength" => "GauntletMovementTimerLength的值代表前进奖励每次提升的时间间隔秒数。\n\n有效范围为非负浮点数。\n\n默认值为5.0。",
            "GauntletMovementBonus" => "GauntletMovementBonus的值代表前进奖励每次提升的数值以及前进奖励的初始值。\n\n有效范围为非负浮点数。\n\n默认值为2.0。",
            "GauntletMovementBonusMax" => "GauntletMovementBonusMax的值代表前进奖励能达到的最大值。\n\n有效范围为非负浮点数。\n\n默认值为30.0。",
            "CustomTankKiteDistance" => "CustomTankKiteDistance的值代表跑图救援中进入GAUNTLET_BOSS_INCOMING阶段强制生成Tank前生还者最远能前进的Nav流距离。\n\n有效范围为非负浮点数。\n\n默认值为3000.0。",
            "PreTankMobMax" => "PreTankMobMax的值代表跑图救援中进入GAUNTLET_BOSS_INCOMING阶段时普通感染者的数量上限。\n\n有效范围为非负整数。\n\n默认值为50。",
            "BridgeSpan" => "BridgeSpan是个自定义变量。在动态更新普通感染者数量上限时用来计算生还者前进的总距离百分比。\n\n有效范围为非负整数。\n\n默认值为20000。",
            "MinSpeed" => "MinSpeed是个自定义变量。在动态更新普通感染者数量上限时用来计算生还者前进的速度百分比。\n\n有效范围为非负整数。\n\n默认值为50。",
            "MaxSpeed" => "MaxSpeed是个自定义变量。在动态更新普通感染者数量上限时用来计算生还者前进的速度百分比。\n\n有效范围为非负整数。\n\n默认值为200。",
            "SpeedPenaltyZAdds" => "SpeedPenaltyZAdds是个自定义变量。在动态更新普通感染者数量上限时用来计算生成数量的增量。\n\n有效范围为非负整数。\n\n默认值为15。",
            "CommonLimitMax" => "CommonLimitMax是个自定义变量。在动态更新普通感染者数量上限时用来限制上限超过设定的最大值。\n\n有效范围为非负整数。\n\n默认值为30。",
            "info__director" => "info_director是一个可以通过使用输入输出控制部分导演行为的点实体。\n\n这个文本框的值将决定info_director的名字(targetname)。",
            "trigger__finale" => "trigger_finale是一个可以触发当前地图的救援的点实体。\n\n这个文本框的值将决定trigger_finale的名字(targetname)。",
            "Script File" => "ScriptFile是trigger_finale的一个键值。\n\n这个文本框的值会决定如果trigger_finale使用Custom救援类型后将会使用的救援脚本名字(带后缀名.nut)。",
            "First Use Delay" => "First Use Delay是trigger_finale的一个键值。\n\n这个文本框的值会决定第一次触发trigger_finale后需要经过多少秒后才能再次触发。",
            "Use Delay" => "Use Delay是trigger_finale的一个键值。\n\n若First Use Delay的值为0，则这个文本框的值会决定触发trigger_finale后需要经过多少秒后才开始救援。\n\n若First Use Delay的值不为0，则这个文本框的值会决定第二次触发trigger_finale后需要经过多少秒后才开始救援。",
            "DelayScript" => "DelayScript是灌油救援中的延迟部分脚本的名字。\n\n这个文本框的值会决定写在救援阶段中SCRIPTED阶段的脚本名字。\n\n此脚本可通过按下\"切换至副脚本\"按钮切换查看。",
            _ => "",
        };
    }

    public static string GetButtonHyperlinkUri(string name)
    {
        return name switch
        {
            "info__director" => "https://developer.valvesoftware.com/wiki/Info_director",
            "trigger__finale" => "https://developer.valvesoftware.com/wiki/Trigger_finale",
            "ScriptFile" => "https://developer.valvesoftware.com/wiki/Trigger_finale#Keyvalues",
            "DelayScript" => "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts",
            "CansNeeded" or "DelayPourThre" or "DelayBothThre" or "CansBothThre" => "https://developer.valvesoftware.com/wiki/Weapon_scavenge_item_spawn",
            "DelayMin" or "DelayMax" or "AbortMin" or "AbortMax" => "https://developer.valvesoftware.com/wiki/Logic_timer",
            "CustomTankKiteDistance" or "GauntletMovementThreshold" or "GauntletMovementTimerLength" or "GauntletMovementBonus" or "GauntletMovementBonusMax" => "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#Gauntlet_Specific/Related",
            _ => "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions",
        };
    }

    public static string GetProcessInput(int num)
    {
        string path = num switch
        {
            0 => Globals.L4D2StandardFinalePath,
            1 => Globals.L4D2ScavengeFinalePath,
            2 => Globals.L4D2GauntletFinalePath,
            _ => string.Empty
        };

        return $"\"{Globals.L4D2VBSPPath}\" -game \"{Globals.L4D2GameInfoPath}\" \"{path}.vmf\"" + $"&\"{Globals.L4D2VVISPath}\" -game \"{Globals.L4D2GameInfoPath}\" \"{path}.bsp\"" + $"&\"{Globals.L4D2VRADPath}\" -game \"{Globals.L4D2GameInfoPath}\" -both -StaticPropLighting -StaticPropPolys \"{path}.bsp\"&exit";
    }

    public static void RunL4D2Game(int type)
    {
        string command = type switch
        { 
            0 => "-steam -novid +sv_cheats 1 +director_debug 1 +map standard_finale",
            1 => "-steam -novid +sv_cheats 1 +director_debug 1 +map scavenge_finale",
            2 => "-steam -novid +sv_cheats 1 +director_debug 1 +map gauntlet_finale",
            _ => string.Empty
        };

        Process process = new();
        process.StartInfo.FileName = $"{Globals.L4D2RootPath}\\left4dead2.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.Arguments = command;
        process.Start();
        process.WaitForExit();
        process.Close();
    }

    public static string GetOffcialStandardScriptFile(int index)
    {
        return index switch
        {
            0 => Properties.Resources.c2m5_concert_finale,
            1 => Properties.Resources.c3m4_plantation_finale,
            2 => Properties.Resources.c4m5_milltown_escape_finale,
            3 => Properties.Resources.c8m5_rooftop_finale,
            4 => Properties.Resources.c9m2_lots_finale,
            5 => Properties.Resources.c10m5_houseboat_finale,
            6 => Properties.Resources.c11m5_runway_finale,
            7 => Properties.Resources.c12m5_cornfield_finale,
            _ => string.Empty,
        };
    }

    public static string GetOffcialScavengeScriptFile(int index)
    {
        return index switch
        {
            0 => Properties.Resources.c1m4_atrium_finale,
            1 => Properties.Resources.c1m4_delay,
            2 => Properties.Resources.c7m3_port_finale,
            3 => Properties.Resources.c14m2_lighthouse_finale,
            _ => string.Empty,
        };
    }

    public static string GetOffcialGauntletScriptFile()
    {
        return Properties.Resources.director_gauntlet;
    }

    public static void SaveNavToPath(string saveFilePath, int type)
    {
        string navFile = type switch
        {
            0 => Properties.Resources.FinaleStandardScriptNav,
            1 => Properties.Resources.FinaleScavengeScriptNav,
            2 => Properties.Resources.FinaleGauntletScriptNav,
            _ => string.Empty
        };

        string filePath = saveFilePath + (saveFilePath.EndsWith(".nav") ? string.Empty : ".nav");
        try
        {
            using MemoryStream memoryStream = new(Convert.FromBase64String(navFile));
            using FileStream fileStream = new(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            byte[] bytes = memoryStream.ToArray();
            fileStream.Write(bytes, 0, bytes.Length);
        }
        catch
        {
            TryOpenMessageWindow(8);
            return;
        }
    }

    public static void SaveNutToPath(string saveFilePath, string text)
    {
        string filePath = saveFilePath + (saveFilePath.EndsWith(".nut") ? string.Empty : ".nut");
        File.WriteAllText(filePath, text);
    }

    public static void SaveVmfToPath(string saveFilePath, List<string> VmfValuesList, int type)
    {
        string file = type switch
        {
            0 => Properties.Resources.FinaleStandardScriptVmf,
            1 => Properties.Resources.FinaleScavengeScriptVmf,
            2 => Properties.Resources.FinaleGauntletScriptVmf,
            _ => string.Empty
        };

        file = file.Replace("\"targetname\" \"director\"", $"\"targetname\" \"{VmfValuesList[0]}\"");
        file = file.Replace("\"director\x1b", $"\"{VmfValuesList[0]}\x1b");

        if (type == 0)
        {
            string replacedFile = type switch
            {
                0 => "standard_finale",
                1 => "scavenge_finale",
                2 => "gauntlet_finale",
                _ => string.Empty
            };

            file = file.Replace("\"targetname\" \"finale_radio\"", $"\"targetname\" \"{VmfValuesList[1]}\"");
            file = file.Replace("\"finale_radio\x1b", $"\"{VmfValuesList[1]}\x1b");
            file = file.Replace($"\"ScriptFile\" \"{replacedFile}.nut\"", $"\"ScriptFile\" \"{VmfValuesList[2]}\"");
            file = file.Replace("\"FirstUseDelay\" \"2\"", $"\"FirstUseDelay\" \"{VmfValuesList[3]}\"");
            file = file.Replace("\"UseDelay\" \"1\"", $"\"UseDelay\" \"{VmfValuesList[4]}\"");
        }
        else if (type == 1)
        {
            file = file.Replace("\"targetname\" \"finale_lever\"", $"\"targetname\" \"{VmfValuesList[1]}\"");
            file = file.Replace("\"finale_lever\x1b", $"\"{VmfValuesList[1]}\x1b");
        }

        string filePath = saveFilePath + (saveFilePath.EndsWith(".vmf") ? string.Empty : ".vmf");

        try
        {
            using StreamWriter streamWriter = File.CreateText(filePath);
            streamWriter.Write(file);
        }
        catch
        {
            TryOpenMessageWindow(8);
            return;
        }
    }
}