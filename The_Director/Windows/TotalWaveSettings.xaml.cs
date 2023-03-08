using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class TotalWaveSettings : MetroWindow
    {
        public int WaveCounts { get; set; }

        public List<string> ComboBoxList = new() { "尸潮/波", "Tank/个", "延迟/秒", "脚本" };

        public Dictionary<string, string> TotalWaveDicts = new();

        public delegate void _SendMessage(string value);
        public _SendMessage SendMessage;

        public TotalWaveSettings()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < WaveCounts; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(40);
                TotalWaveGrid.RowDefinitions.Add(row);
                Label label = new();
                label.Content = $"第{i + 1}波";
                label.FontSize = 20;
                label.FontFamily = new FontFamily("Dengxian");
                label.Margin = new Thickness(10, 5, 0, 5);
                label.HorizontalAlignment = HorizontalAlignment.Left;
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                TotalWaveGrid.Children.Add(label);

                ComboBox comboBox = new();
                comboBox.Name = $"_{i}";
                comboBox.SelectedIndex = 0;
                comboBox.ItemsSource = ComboBoxList;
                comboBox.FontSize = 20;
                comboBox.FontFamily = new FontFamily("Dengxian");
                comboBox.Margin = new Thickness(0, 5, 0, 5);
                comboBox.HorizontalAlignment = HorizontalAlignment.Center;
                comboBox.Width = 110;

                if (TotalWaveDicts.ContainsKey($"{i + 1}"))
                    comboBox.SelectedIndex = Functions.TotalWaveToInt(TotalWaveDicts[$"{i + 1}"].Split('\x1b')[0]);

                Grid.SetRow(comboBox, i);
                Grid.SetColumn(comboBox, 1);
                TotalWaveGrid.Children.Add(comboBox);

                TextBox textBox = new();
                textBox.Name = $"_{i}";
                textBox.FontSize = 20;
                textBox.FontFamily = new FontFamily("Bahnschrift");
                textBox.Margin = new Thickness(0, 5, 10, 5);
                textBox.HorizontalAlignment = HorizontalAlignment.Right;
                textBox.Width = 220;

                if (TotalWaveDicts.ContainsKey($"{i + 1}"))
                    textBox.Text = TotalWaveDicts[$"{i + 1}"].Split('\x1b')[1];

                Grid.SetRow(textBox, i);
                Grid.SetColumn(textBox, 2);
                TotalWaveGrid.Children.Add(textBox);
            }
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in TotalWaveGrid.Children)
                if(item is TextBox)
                {
                    TextBox textBox = (TextBox)item;
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        MessageWindow messageWindow = new MessageWindow()
                        {
                            Title = "错误",
                            TextBoxString= $"第{Functions.ConvertToInt(textBox.Name.Remove(0, 1)) + 1}波未指定数据！",
                            Owner = Application.Current.MainWindow,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        messageWindow.ShowDialog();
                        return;
                    }
                }
            SendMessage("Confirmed");
            DialogResult = true;
            Close();
        }

        private void CancleButtonClick(object sender, RoutedEventArgs e)
        {
            SendMessage(null);
            DialogResult = true;
            Close();
        }
    }
}