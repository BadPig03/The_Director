using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using The_Director.Utils;

public class SoundCacheProcessor
{
    public string OldFolderPath { get; set; }
    public string FilePath { get; set; }
    public virtual BackgroundWorker Worker { get; set; }
    public void StartProcess()
    {
        if (Directory.Exists(Globals.L4D2CustomAudioPath))
        {
            Directory.Delete(Globals.L4D2CustomAudioPath, true);
        }

        Directory.CreateDirectory(Globals.L4D2CustomAudioPath);
        
        Task.Run(() => { Functions.DirectoryCopy(OldFolderPath, Globals.L4D2CustomAudioPath, true); });

        while (Directory.GetDirectories(OldFolderPath).Length != Directory.GetDirectories(Globals.L4D2CustomAudioPath).Length)
        {
            Worker.ReportProgress(100 * Directory.GetDirectories(Globals.L4D2CustomAudioPath).Length / Directory.GetDirectories(OldFolderPath).Length / 2);
        }

        FileInfo fileInfo = new(Globals.L4D2CustomAudioPath + "\\sound.cache");
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        Process process = new();
        process.StartInfo.FileName = $"{Globals.L4D2RootPath}\\left4dead2.exe";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.Arguments = $"-steam -insecure -novid -hidden -noborder -x 4096 -y 2160 +snd_buildsoundcachefordirectory {Globals.L4D2CustomAudioPath}";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.Start();

        Worker.ReportProgress(-1);

        while (!new FileInfo(Globals.L4D2CustomAudioPath + "\\sound.cache").Exists)
        {
            Task.Delay(100);
        }

        process.Kill();
        process.Close();

        File.Copy(Globals.L4D2CustomAudioPath + "\\sound.cache", Path.GetDirectoryName(FilePath) + "\\sound.cache", true);
        Directory.Delete(Globals.L4D2CustomAudioPath, true);

        Worker.ReportProgress(100);
    }
}