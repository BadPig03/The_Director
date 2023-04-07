using Steamworks;
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
    public static List<string> StandardDictBlackList = new() { "TotalWave", "MSG", "ShowStage", "TriggerEscapeStage", "info_director", "trigger_finale", "ScriptFile", "FirstUseDelay", "UseDelay" };
    public static List<string> ScavengeDictBlackList = new() { "MSG", "ShowProgress", "info_director", "trigger_finale", "DelayScript", "CansNeeded", "DelayMin", "DelayMax", "DelayPourThre", "DelayBothThre", "AbortMin", "AbortMax", "CansBothThre"};
    public static List<string> GauntletDictBlackList = new() { "" };
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
    public static string L4D2VVISPath = L4D2RootPath + "\\bin\\vvis.exe";
    public static string L4D2VBSPPath = L4D2RootPath + "\\bin\\vbsp.exe";
    public static string L4D2VRADPath = L4D2RootPath + "\\bin\\vrad.exe";
    public static string L4D2MapsPath = L4D2RootPath + "\\left4dead2\\maps";
    public static string L4D2ScriptsPath = L4D2RootPath + "\\left4dead2\\scripts\\vscripts";

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