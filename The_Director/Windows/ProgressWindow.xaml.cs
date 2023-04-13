using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace The_Director.Windows
{
    public partial class ProgressWindow : MetroWindow
    {
        BackgroundWorker worker = null;
        public string TextBoxString { get; set; }
        public string FilePath { get; set; }

        public ProgressWindow()
        {
            InitializeComponent();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
            DialogResult = true;
            Close();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MessageTextBox.Text = TextBoxString;

            worker = new()
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            worker.ProgressChanged += WorkerProgressChanged;
            worker.DoWork += WorkerDoWork;
            worker.RunWorkerCompleted += WorkerCompleted;
            worker.RunWorkerAsync();
        }
        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            VmfReader vmfReader = new()
            {
                VmfPath = FilePath
            };

            vmfReader.BeginReading();

            if (worker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            backgroundWorker.DoWork -= WorkerDoWork;
            backgroundWorker.RunWorkerCompleted -= WorkerCompleted;
            backgroundWorker = null;
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                CancelButtonClick(null, null);
            }
        }
    }
}
