using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using The_Director.Data;

namespace The_Director.Windows
{
    public partial class TotalWaveSettings : MetroWindow
    {
        public int WaveCounts { get; set; }
        public List<string> parName { get; set; }

        public TotalWaveSettings()
        {
            this.DataContext = this;
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < WaveCounts; i++)
            {
                TotalWaveGrid.RowDefinitions.Add(new RowDefinition());
                Label label = new();
                label.Content = parName[i];
                label.FontSize = 20;
                label.FontFamily = new FontFamily("Dengxian");
                label.Margin = new Thickness(0, 0, 0, 5);
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                TotalWaveGrid.Children.Add(label);
            }
        }
    }
}
