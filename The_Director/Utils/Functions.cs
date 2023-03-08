using System.Text.RegularExpressions;

namespace The_Director.Utils;

public record struct BooleanString(bool Item1, string Item2)
{
    public static implicit operator (bool, string)(BooleanString value)
    {
        return (value.Item1, value.Item2);
    }

    public static implicit operator BooleanString((bool, string) value)
    {
        return new BooleanString(value.Item1, value.Item2);
    }
}

public static class Functions
{
    public static bool IsProperInt(string value, int min, int max)
    {
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
                return 2;
            case "MusicDynamicMobScanStopSize":
            case "MusicDynamicMobSpawnSize":
            case "MusicDynamicMobStopSize":
            case "BileMobSize":
            case "MegaMobSize":
            case "MobMaxSize":
            case "MobMinSize":
            case "MobSpawnSize":
                return 3;
            case "BoomerLimit":
            case "ChargerLimit":
            case "CommonLimit":
            case "DominatorLimit":
            case "HunterLimit":
            case "JockeyLimit":
            case "MaxSpecials":
            case "MobMaxPending":
            case "SmokerLimit":
            case "SpitterLimit":
            case "TankLimit":
            case "WitchLimit":
                return 4;
            default:
                return -1;
        }
    }

    public static string GetButtonString(string name)
    {
        switch(name)
        {
            case "BuildUpMinInterval":
                return "BuildUpMinInterval的值代表节奏中BUILD_UP最短持续秒数\n\n有效范围为非负整数。\n\n默认值为15。";
            case "IntensityRelaxThreshold":
                return "IntensityRelaxThreshold的值代表所有生还者的紧张度都必须小于多少才能让节奏从SUSTAIN_PEAK切换为RELAX。\n\n有效范围为0-1的浮点数。\n\n默认值为0.9。";
            case "LockTempo":
                return "设置LockTempo为true会无延迟地生成尸潮。\n\n默认值为false。";
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
            case "ProhibitBosses":
                return "设置ProhibitBosses为true会防止Tank和Witch生成。\n\n默认值为false。";
            case "ShouldAllowMobsWithTank":
                return "设置ShouldAllowMobsWithTank为true会允许在Tank在场时生成小僵尸。\n\nBoomer和胆汁炸弹引起的尸潮不受影响。\n\n仅适用于战役模式。\n\n默认为false。";
            case "ShouldAllowSpecialsWithTank":
                return "设置ShouldAllowSpecialsWithTank为true会允许在Tank在场时生成特殊感染者。\n\n仅适用于战役模式。\n\n默认为false。";
            case "SpecialRespawnInterval":
                return "SpecialRespawnInterval的值代表特殊感染者重生所需要的秒数。\n\n有效范围为非负浮点数。\n\n默认值为：战役模式为45，对抗模式为20。";
            case "SustainPeakMaxTime":
                return "SustainPeakMaxTime的值代表节奏中SUSTAIN_PEAK的最长持续分钟数。\n\n有效范围为非负浮点数。\n\n默认值为5。";
            case "SustainPeakMinTime":
                return "SustainPeakMinTime的值代表节奏中SUSTAIN_PEAK的最短持续分钟数。\n\n有效范围为非负浮点数。\n\n默认值为3。";
            case "BileMobSize":
                return "BileMobSize的值代表Boomer和胆汁炸弹引起的普通感染者数量最大值。\n\n有效范围为非负整数。\n\n无默认值。";
            case "BoomerLimit":
                return "BoomerLimit的值代表在场的Boomer最大数量。\n\n有效范围为整数。\n\n默认值为1。";
            case "ChargerLimit":
                return "ChargerLimit的值代表在场的Charger最大数量。\n\n有效范围为整数。\n\n默认值为1。";
            case "CommonLimit":
                return "CommonLimit的值代表在场的普通感染者最大数量。\n\n有效范围为整数。\n\n默认值为30。";
            case "DominatorLimit":
                return "DominatorLimit的值代表在场的控制型特殊感染者(Hunter, Jockey, Charger, Smoker)最大数量。\n\n有效范围为整数。\n\n无默认值。";
            case "HunterLimit":
                return "HunterLimit的值代表在场的Hunter最大数量。\n\n有效范围为整数。\n\n默认值为1。";
            case "JockeyLimit":
                return "JockeyLimit的值代表在场的Jockey最大数量。\n\n有效范围为整数。\n\n默认值为1。";
            case "MaxSpecials":
                return "MaxSpecials的值代表在场的特殊感染者最大数量。\n\n有效范围为整数。\n\n默认值为2。";
            case "MegaMobSize":
                return "MegaMobSize的值代表一次尸潮能生成的普通感染者最大数量。\n\n有效范围为非负整数。\n\n无默认值。";
            case "MobMaxPending":
                return "MobMaxPending的值代表当尸潮的普通感染者数量超过CommonLimit时最多有多少普通感染者可以暂时等待生成。\n\n有效范围为非负整数。\n\n无默认值。";
            case "MobMaxSize":
                return "MobMaxSize的值代表一次尸潮生成普通感染者的最大数量。\n\n有效范围为非负整数。\n\n默认值为30。";
            case "MobMinSize":
                return "MobMinSize的值代表一次尸潮生成普通感染者的最小数量。\n\n有效范围为非负整数。\n\n默认值为10。";
            case "MobSpawnSize":
                return "MobSpawnSize的值代表一次尸潮生成普通感染者的数量。\n\n覆盖MobMaxSize与MobMinSize。\n\n有效范围为非负整数。\n\n无默认值。";
            case "SmokerLimit":
                return "SmokerLimit的值代表代表在场的Smoker最大数量。\n\n有效范围为整数。\n\n默认值为1。";
            case "SpitterLimit":
                return "SpitterLimit的值代表代表在场的Spitter最大数量。\n\n有效范围为整数。\n\n默认值为1。";
            case "TankLimit":
                return "TankLimit的值代表代表在场的Tank最大数量。\n\n有效范围为整数。\n\n小于0则代表无限制。\n\n默认值为-1。";
            case "WitchLimit":
                return "WitchLimit的值代表代表在场的Witch最大数量。\n\n有效范围为整数。\n\n小于0则代表无限制。\n\n默认值为-1。";
            default:
                return "";
        } 
    }
}