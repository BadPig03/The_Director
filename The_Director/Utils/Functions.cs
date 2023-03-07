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
}