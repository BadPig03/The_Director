﻿using Steamworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace The_Director.Utils;

public static class Globals
{
    public const int L4D2AppID = 550;
    public const int L4D2ATAppID = 563;

    public static List<string> StageTypeList = new() { "尸潮/波", "Tank/个", "延迟/秒", "脚本" };
    public static List<string> RescueTypeList = new() { "防守救援", "灌油救援", "跑图救援", "牺牲救援", "尸潮脚本" };
    public static List<string> PreferredMobDirectionList = new() { string.Empty, "SPAWN_ABOVE_SURVIVORS", "SPAWN_ANYWHERE", "SPAWN_BEHIND_SURVIVORS", "SPAWN_FAR_AWAY_FROM_SURVIVORS", "SPAWN_IN_FRONT_OF_SURVIVORS", "SPAWN_LARGE_VOLUME", "SPAWN_NEAR_IT_VICTIM", "SPAWN_NO_PREFERENCE" };
    public static List<string> PreferredSpecialDirectionList = new() { string.Empty, "SPAWN_ABOVE_SURVIVORS", "SPAWN_SPECIALS_ANYWHERE", "SPAWN_BEHIND_SURVIVORS", "SPAWN_FAR_AWAY_FROM_SURVIVORS", "SPAWN_SPECIALS_IN_FRONT_OF_SURVIVORS", "SPAWN_LARGE_VOLUME", "SPAWN_NEAR_IT_VICTIM", "SPAWN_NO_PREFERENCE" };
    public static List<string> StandardDictBlackList = new() { "TotalWave", "MSG", "ShowStage", "TriggerEscapeStage", "info_director", "trigger_finale", "ScriptFile"};
    public static List<string> ScavengeDictBlackList = new() { "MSG", "ShowProgress", "info_director", "trigger_finale", "DelayScript", "GasCansNeeded", "DelayMin", "DelayMax", "DelayPouredThreshold", "DelayPouredOrTouchedThreshold", "AbortDelayMin", "AbortDelayMax", "GasCansDividend" };
    public static List<string> GauntletDictBlackList = new() { "MSG", "info_director", "trigger_finale" };
    public static List<string> SacrificeDictBlackList = new() { "MSG", "info_director", "trigger_finale" };
    public static List<string> StandardObjectList = new() { "SaveAsNutButton", "PasteToClipboardButton", "MSGCheckBox", "TotalWaveLabel", "TotalWaveButton", "TotalWaveTextBox", "ShowStageCheckBox", "SaveAsVmfButton", "CompileVmfButton", "LockTempoCheckBox", "BuildUpMinIntervalTextBox", "IntensityRelaxThresholdTextBox", "MobRechargeRateTextBox", "MobSpawnMaxTimeTextBox", "MobSpawnMinTimeTextBox", "MusicDynamicMobScanStopSizeTextBox", "MusicDynamicMobSpawnSizeTextBox", "MusicDynamicMobStopSizeTextBox", "PreferredMobDirectionComboBox", "RelaxMaxFlowTravelTextBox", "RelaxMaxIntervalTextBox", "RelaxMinIntervalTextBox", "MinimumStageTimeTextBox", "PreferredSpecialDirectionComboBox", "ProhibitBossesCheckBox", "ShouldAllowMobsWithTankCheckBox", "ShouldAllowSpecialsWithTankCheckBox", "EscapeSpawnTanksCheckBox", "SpecialRespawnIntervalTextBox", "SustainPeakMaxTimeTextBox", "SustainPeakMinTimeTextBox", "BileMobSizeTextBox", "BoomerLimitTextBox", "ChargerLimitTextBox", "CommonLimitTextBox", "DominatorLimitTextBox", "HunterLimitTextBox", "JockeyLimitTextBox", "MaxSpecialsTextBox", "MegaMobSizeTextBox", "MobMaxPendingTextBox", "MobMaxSizeTextBox", "MobMinSizeTextBox", "MobSpawnSizeTextBox", "SmokerLimitTextBox", "SpitterLimitTextBox", "TankLimitTextBox", "WitchLimitTextBox", "HordeEscapeCommonLimitTextBox", "LockTempoLabel", "BuildUpMinIntervalLabel", "IntensityRelaxThresholdLabel", "MobRechargeRateLabel", "MobSpawnMaxTimeLabel", "MobSpawnMinTimeLabel", "MusicDynamicMobScanStopSizeLabel", "MusicDynamicMobSpawnSizeLabel", "MusicDynamicMobStopSizeLabel", "PreferredMobDirectionLabel", "RelaxMaxFlowTravelLabel", "RelaxMaxIntervalLabel", "RelaxMinIntervalLabel", "MinimumStageTimeLabel", "PreferredSpecialDirectionLabel", "ProhibitBossesLabel", "ShouldAllowMobsWithTankLabel", "ShouldAllowSpecialsWithTankLabel", "EscapeSpawnTanksLabel", "SpecialRespawnIntervalLabel", "SustainPeakMaxTimeLabel", "SustainPeakMinTimeLabel", "BileMobSizeLabel", "BoomerLimitLabel", "ChargerLimitLabel", "CommonLimitLabel", "DominatorLimitLabel", "HunterLimitLabel", "JockeyLimitLabel", "MaxSpecialsLabel", "MegaMobSizeLabel", "MobMaxPendingLabel", "MobMaxSizeLabel", "MobMinSizeLabel", "MobSpawnSizeLabel", "SmokerLimitLabel", "SpitterLimitLabel", "TankLimitLabel", "WitchLimitLabel", "HordeEscapeCommonLimitLabel" };
    public static List<string> OffcialMapStandardRescueList = new() { "c2m5_concert_finale.nut", "c3m4_plantation_finale.nut", "c4m5_milltown_escape_finale.nut", "c8m5_rooftop_finale.nut", "c9m2_lots_finale.nut", "c10m5_houseboat_finale.nut", "c11m5_runway_finale.nut", "c12m5_cornfield_finale.nut" };
    public static List<string> OffcialMapScavengeRescueList = new() { "c1m4_atrium_finale.nut", "c1m4_delay.nut", "c6m3_port_finale.nut", "c14m2_lighthouse_finale.nut" };
    public static List<string> OffcialMapGauntletRescueList = new() { "director_gauntlet.nut" };
    public static List<string> OffcialMapSacrificeRescueList = new() { "c7m3_port_finale.nut" };
    public static List<int> TankIndexList = new() { 6, 14, 22, 32 };


    public static string L4D2RootPath = SteamApps.AppInstallDir(L4D2AppID);
    public static string L4D2TempPath = Path.GetTempPath();
    public static string L4D2GameInfoPath = L4D2RootPath + "\\left4dead2";
    public static string L4D2StandardFinalePath = Path.GetTempPath() + "standard_finale";
    public static string L4D2ScavengeFinalePath = Path.GetTempPath() + "scavenge_finale";
    public static string L4D2GauntletFinalePath = Path.GetTempPath() + "gauntlet_finale";
    public static string L4D2SacrificeFinalePath = Path.GetTempPath() + "sacrifice_finale";
    public static string L4D2VVISPath = L4D2RootPath + "\\bin\\vvis.exe";
    public static string L4D2VBSPPath = L4D2RootPath + "\\bin\\vbsp.exe";
    public static string L4D2VRADPath = L4D2RootPath + "\\bin\\vrad.exe";
    public static string L4D2MapsPath = L4D2RootPath + "\\left4dead2\\maps";
    public static string L4D2ScriptsPath = L4D2RootPath + "\\left4dead2\\scripts\\vscripts";

    public static int TextBoxIndex(string name)
    {
        return name switch
        {
            "TotalWave" => 0,
            "IntensityRelaxThreshold" => 1,
            "BuildUpMinInterval" or "MobRechargeRate" or "MobSpawnMaxTime" or "MobSpawnMinTime" or "RelaxMaxFlowTravel" or "RelaxMaxInterval" or "RelaxMinInterval" or "SpecialRespawnInterval" or "SustainPeakMaxTime" or "SustainPeakMinTime" or "MinimumStageTime" or "CustomTankKiteDistance" or "GauntletMovementThreshold" or "GauntletMovementTimerLength" or "GauntletMovementBonus" or "GauntletMovementBonusMax" => 2,
            "MusicDynamicMobScanStopSize" or "MusicDynamicMobSpawnSize" or "MusicDynamicMobStopSize" or "BileMobSize" or "MegaMobSize" or "MobMaxSize" or "MobMinSize" or "MobSpawnSize" or "BoomerLimit" or "ChargerLimit" or "CommonLimit" or "DominatorLimit" or "HunterLimit" or "JockeyLimit" or "MaxSpecials" or "SmokerLimit" or "SpitterLimit" or "PreTankMobMax" => 3,
            "MobMaxPending" or "TankLimit" or "WitchLimit" or "HordeEscapeCommonLimit" => 4,
            "info_director" or "trigger_finale" or "ScriptFile" or "DelayScript" => 5,
            "GasCansNeeded" or "DelayMin" or "DelayMax" or "DelayPouredThreshold" or "DelayPouredOrTouchedThreshold" or "AbortDelayMin" or "AbortDelayMax" or "GasCansDividend" or "BridgeSpan" or "MinSpeed" or "MaxSpeed" or "SpeedPenaltyZAdds" or "CommonLimitMax" => 6,
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
            "GasCansNeeded" => "GasCansNeeded是个自定义变量。在灌油救援需要灌入发动机的总油桶数量。\n\n有效范围为非负整数。\n\n默认值为12。",
            "DelayMin" => "DelayMin的值代表灌油救援中SCRIPTED阶段自动结束前最少得经过的秒数。\n\n有效范围为非负整数。\n\n默认值为10。",
            "DelayMax" => "DelayMax的值代表灌油救援中SCRIPTED阶段自动结束前最多经过的秒数。\n\n有效范围为非负整数。\n\n默认值为20。",
            "DelayPouredThreshold" => "DelayPouredThreshold的值代表灌油救援中SCRIPTED阶段被强制中断前生还者最多能灌入发动机的油桶数量。\n\n有效范围为非负整数。\n\n默认值为1。",
            "DelayPouredOrTouchedThreshold" => "DelayPouredOrTouchedThreshold的值代表灌油救援中SCRIPTED阶段被强制中断前生还者最多能灌入发动机的油桶数量和捡起来过的油桶数量之和。\n\n有效范围为非负整数。\n\n默认值为2。",
            "AbortDelayMin" => "AbortDelayMin的值代表强制中断救援阶段的SCRIPTED阶段后在进入下一个阶段前最少得经过的秒数。\n\n有效范围为非负整数。\n\n默认值为1。",
            "AbortDelayMax" => "AbortDelayMax的值代表强制中断救援阶段的SCRIPTED阶段后在进入下一个阶段前最多经过的秒数。\n\n有效范围为非负整数。\n\n默认值为3。",
            "GasCansDividend" => "当生还者灌入发动机的油桶数量和捡起来过的油桶数量之和可以被GasCansDividend的值整除时，会强行中断当前的SCRIPTED阶段并立刻进入下一阶段。\n\n有效范围为非负整数。\n\n默认值为4。",
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
            "GasCansNeeded" or "DelayPouredThreshold" or "DelayPouredOrTouchedThreshold" or "GasCansDividend" => "https://developer.valvesoftware.com/wiki/Weapon_scavenge_item_spawn",
            "DelayMin" or "DelayMax" or "AbortDelayMin" or "AbortDelayMax" => "https://developer.valvesoftware.com/wiki/Logic_timer",
            "CustomTankKiteDistance" or "GauntletMovementThreshold" or "GauntletMovementTimerLength" or "GauntletMovementBonus" or "GauntletMovementBonusMax" => "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#Gauntlet_Specific/Related",
            _ => "https://developer.valvesoftware.com/wiki/L4D2_Director_Scripts#DirectorOptions",
        };
    }

    public static void FileToBase64String()
    {
        string data = "";
        using (MemoryStream msReader = new())
        {
            using (FileStream fs = new("", FileMode.Open))
            {
                byte[] buffer = new byte[1024];
                int readLen = 0;
                while ((readLen = fs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    msReader.Write(buffer, 0, readLen);
                }
            }
            data = Convert.ToBase64String(msReader.ToArray());
        }
        Debug.WriteLine(data);
    }
}