using Steamworks;
using System.Collections.Generic;

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
    public static List<string> StandardObjectList = new() { "SaveAsNutButton", "PasteToClipboardButton", "MSGCheckBox", "TotalWaveLabel", "TotalWaveButton", "TotalWaveTextBox", "ShowStageCheckBox", "info_directorTextBox", "trigger_finaleTextBox", "ScriptFileTextBox", "SaveAsVmfButton", "CompileVmfButton", "LockTempoCheckBox", "BuildUpMinIntervalTextBox", "IntensityRelaxThresholdTextBox", "MobRechargeRateTextBox", "MobSpawnMaxTimeTextBox", "MobSpawnMinTimeTextBox", "MusicDynamicMobScanStopSizeTextBox", "MusicDynamicMobSpawnSizeTextBox", "MusicDynamicMobStopSizeTextBox", "PreferredMobDirectionComboBox", "RelaxMaxFlowTravelTextBox", "RelaxMaxIntervalTextBox", "RelaxMinIntervalTextBox", "MinimumStageTimeTextBox", "PreferredSpecialDirectionComboBox", "ProhibitBossesCheckBox", "ShouldAllowMobsWithTankCheckBox", "ShouldAllowSpecialsWithTankCheckBox", "EscapeSpawnTanksCheckBox", "SpecialRespawnIntervalTextBox", "SustainPeakMaxTimeTextBox", "SustainPeakMinTimeTextBox", "BileMobSizeTextBox", "BoomerLimitTextBox", "ChargerLimitTextBox", "CommonLimitTextBox", "DominatorLimitTextBox", "HunterLimitTextBox", "JockeyLimitTextBox", "MaxSpecialsTextBox", "MegaMobSizeTextBox", "MobMaxPendingTextBox", "MobMaxSizeTextBox", "MobMinSizeTextBox", "MobSpawnSizeTextBox", "SmokerLimitTextBox", "SpitterLimitTextBox", "TankLimitTextBox", "WitchLimitTextBox", "HordeEscapeCommonLimitTextBox", "LockTempoLabel", "BuildUpMinIntervalLabel", "IntensityRelaxThresholdLabel", "MobRechargeRateLabel", "MobSpawnMaxTimeLabel", "MobSpawnMinTimeLabel", "MusicDynamicMobScanStopSizeLabel", "MusicDynamicMobSpawnSizeLabel", "MusicDynamicMobStopSizeLabel", "PreferredMobDirectionLabel", "RelaxMaxFlowTravelLabel", "RelaxMaxIntervalLabel", "RelaxMinIntervalLabel", "MinimumStageTimeLabel", "PreferredSpecialDirectionLabel", "ProhibitBossesLabel", "ShouldAllowMobsWithTankLabel", "ShouldAllowSpecialsWithTankLabel", "EscapeSpawnTanksLabel", "SpecialRespawnIntervalLabel", "SustainPeakMaxTimeLabel", "SustainPeakMinTimeLabel", "BileMobSizeLabel", "BoomerLimitLabel", "ChargerLimitLabel", "CommonLimitLabel", "DominatorLimitLabel", "HunterLimitLabel", "JockeyLimitLabel", "MaxSpecialsLabel", "MegaMobSizeLabel", "MobMaxPendingLabel", "MobMaxSizeLabel", "MobMinSizeLabel", "MobSpawnSizeLabel", "SmokerLimitLabel", "SpitterLimitLabel", "TankLimitLabel", "WitchLimitLabel", "HordeEscapeCommonLimitLabel" };
    public static List<string> OffcialMapStandardRescueList = new() { "c2m5_concert_finale.nut" };

    public static string L4D2RootPath = SteamApps.AppInstallDir(L4D2AppID);
    public static string L4D2ScriptsPath = L4D2RootPath + "\\left4dead2\\scripts\\vscripts";
}