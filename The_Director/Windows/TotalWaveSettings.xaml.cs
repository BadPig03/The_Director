using MahApps.Metro.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using The_Director.Utils;

namespace The_Director.Windows
{
    public partial class TotalWaveSettings : MetroWindow
    {
        public int WaveCounts { get; set; }

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
                RowDefinition row = new();
                row.Height = new GridLength(40);
                TotalWaveGrid.RowDefinitions.Add(row);

                Label label = new()
                {
                    Content = $"阶段{i + 1}",
                    FontSize = 20,
                    FontFamily = new FontFamily("Dengxian"),
                    Margin = new Thickness(10, 5, 0, 5),
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                TotalWaveGrid.Children.Add(label);

                ComboBox comboBox = new()
                {
                    Name = $"_{i}",
                    SelectedIndex = 0,
                    ItemsSource = Globals.StageTypeList,
                    FontSize = 20,
                    FontFamily = new FontFamily("Dengxian"),
                    Margin = new Thickness(10, 5, 0, 5),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 105
                };
                comboBox.SelectionChanged += new SelectionChangedEventHandler(TypeSelectionChanged);

                Grid.SetRow(comboBox, i);
                Grid.SetColumn(comboBox, 1);
                TotalWaveGrid.Children.Add(comboBox);

                TextBox textBox = new()
                {
                    Name = $"_{i}",
                    FontSize = 17,
                    FontFamily = new FontFamily("Bahnschrift"),
                    Margin = new Thickness(0, 5, 10, 5),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 220
                };

                TextBox musicTextBox = new()
                {
                    Name = $"__{i}",
                    FontSize = 17,
                    FontFamily = new FontFamily("Bahnschrift"),
                    Margin = new Thickness(0, 5, 10, 5),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Width = 220,
                    Visibility = Visibility.Hidden
                };

                Grid.SetRow(textBox, i);
                Grid.SetColumn(textBox, 2);
                Grid.SetRow(musicTextBox, i);
                Grid.SetColumn(musicTextBox, 3);

                TotalWaveGrid.Children.Add(textBox);
                TotalWaveGrid.Children.Add(musicTextBox);

                if (TotalWaveDicts.ContainsKey($"{i + 1}"))
                    comboBox.SelectedIndex = Functions.TotalWaveToInt(TotalWaveDicts[$"{i + 1}"].Split('\x1b')[0]);

                if (TotalWaveDicts.ContainsKey($"{i + 1}"))
                {
                    if (TotalWaveDicts[$"{i + 1}"].Split('\x1b')[1].Contains("\x1c"))
                    {
                        textBox.Text = TotalWaveDicts[$"{i + 1}"].Split('\x1b')[1].Split('\x1c')[0];
                        musicTextBox.Text = TotalWaveDicts[$"{i + 1}"].Split('\x1b')[1].Split('\x1c')[1];
                    }
                    else
                    {
                        textBox.Text = TotalWaveDicts[$"{i + 1}"].Split('\x1b')[1];
                        musicTextBox.Text = string.Empty;
                    }
                }

            }
        }

        private void TypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            ComboBox comboBox = (ComboBox)sender;
            var Index = comboBox.Name.Remove(0, 1);

            if (HasAnyTank())
            {
                MusicLabel.Visibility = Visibility.Visible;
                Width = 720;
                TextBox textBox = (TextBox)GetMusicTextBox(Index);
                textBox.Visibility = comboBox.SelectedIndex == 1 ? Visibility.Visible : Visibility.Hidden;
            }
            else
            {
                MusicLabel.Visibility = Visibility.Hidden;
                Width = 480;
            }
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in TotalWaveGrid.Children)
                if(item is TextBox)
                {
                    TextBox textBox = (TextBox)item;
                    if (!textBox.Name.StartsWith("__") && string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        Functions.TryOpenMessageWindow(Functions.ConvertToInt(textBox.Name.Remove(0, 1)), true);
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

        private bool HasAnyTank()
        {
            foreach (var item in TotalWaveGrid.Children)
                if(item is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)item;
                    if(comboBox.SelectedIndex == 1)
                        return true;
                }
            return false;
        }

        private object GetMusicTextBox(string CompareName)
        {
            foreach (var item in TotalWaveGrid.Children)
                if (item is TextBox)
                {
                    TextBox textBox = (TextBox)item;
                    if (textBox.Name.StartsWith("__"))
                        if (textBox.Name.Remove(0, 2) == CompareName)
                            return textBox;
                }
            return null;
        }
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ConfirmButtonClick(null, null);
            else if (e.Key == Key.Escape)
                CancleButtonClick(null, null);
        }
    }
}