using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using The_Director.Utils;

public class CubemapBuilder
{
    public string FilePath { get; set; }
    public string FileName { get; set; }
    public virtual BackgroundWorker Worker { get; set; }

    public void StartProcess()
    {
        if(FilePath != $"{Globals.L4D2MapsPath}\\{FileName}")
        {
            File.Copy(FilePath, $"{Globals.L4D2MapsPath}\\{FileName}", true);
        }

        string mapName = FileName.Replace(".bsp", "");

        Worker.ReportProgress(33);

        Process process = new();
        process.StartInfo.FileName = $"{Globals.L4D2RootPath}\\left4dead2.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.Arguments = $"-steam -insecure -novid -hidden -nosound -noborder -x 4096 -y 2160 +map {mapName} -buildcubemaps";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();

        Worker.ReportProgress(-1);

        process.WaitForExit();
        process.Close();

        Worker.ReportProgress(100);
    }
}