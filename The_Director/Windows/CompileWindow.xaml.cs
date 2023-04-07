using System;
using System.Diagnostics;
using System.Windows;
using MahApps.Metro.Controls;
using The_Director.Utils;

namespace The_Director.Windows
{
    public delegate void DelegateReadStandardOutput(string result);

    public partial class CompileWindow : MetroWindow
    {
        public int Num { get; set; }

        public Process process = new();

        public event DelegateReadStandardOutput ReadStandardOutput;

        public CompileWindow()
        {
            InitializeComponent();
            ReadStandardOutput += new DelegateReadStandardOutput(ReadStandardOutputAction);
        }

        private void StartNewProcess()
        {
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.OutputDataReceived += new DataReceivedEventHandler(ProcessOutputHandler);
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(ProcessExited);
            process.Start();
            process.BeginOutputReadLine();
        }

        private void ProcessOutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Dispatcher.Invoke(ReadStandardOutput, new object[] { e.Data });
            }
        }

        private void ProcessExited(object sender, EventArgs e)
        {
            MessageBox.Show("地图编译完成!\n请关闭编译窗口以启动游戏!", "提示", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            process.Close();
        }

        private void ProcessCommand()
        {
            process.StandardInput.WriteLine(Functions.GetProcessInput(Num));
            process.StandardInput.AutoFlush = true;
        }

        private void ReadStandardOutputAction(string result)
        {
            MessageTextBox.AppendText($"{result}\r\n");
            ScrollBar.ScrollToEnd();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            StartNewProcess();
            ProcessCommand();
        }
    }
}
