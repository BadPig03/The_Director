using System.Diagnostics;

namespace The_Director.Utils;

public static class Clients
{    public static string GetSteamPath()
    {
        if (Process.GetProcessesByName("steam").Length > 0)
        {
            var steam = Process.GetProcessesByName("steam")[0];
            return steam.MainModule.FileName;
        }
        return string.Empty;
    }

    public static string GetL4D2Path()
    {
        //return SteamApps.AppInstallDir(Globals.L4D2AppID);
        return "";
    }
}