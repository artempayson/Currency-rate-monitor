using Logic;
using Logic.Model;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static DispatcherTimer dispatcherTimer;
        Repository repository;
        List<ICurrencyPair> allCurrencyPairs;
        List<string> axisXData;
        List<decimal> axisSellData;
        List<decimal> axisBuyData;

        public MainWindow()
        {
            InitializeComponent();
            repository = new Repository();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);

            Graph.ChartAreas.Add(new ChartArea("PlaceForGraph"));

            Graph.Series.Add(new Series("SellGraph"));
            Graph.Series.Add(new Series("BuyGraph"));

            Graph.Series["SellGraph"].ChartArea = "PlaceForGraph";
            Graph.Series["SellGraph"].ChartType = SeriesChartType.Line;
            Graph.Series["BuyGraph"].ChartArea = "PlaceForGraph";
            Graph.Series["BuyGraph"].ChartType = SeriesChartType.Line;

            axisXData = new List<string>();
            axisSellData = new List<decimal>();
            axisBuyData = new List<decimal>();

            Graph.Legends.Add(new Legend("Legend"));

            Graph.Legends["Legend"].DockedToChartArea = "PlaceForGraph";
            Graph.Series[0].Legend = "Legend";
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (allCurrencyPairs == null) return;
            
                var point = allCurrencyPairs[CurrencyChooseComboBox.SelectedIndex];
                repository.UpdateSpecifiedPair(point);

                Graph.ChartAreas[0].AxisX.MajorGrid.Enabled = HorizontalLinesCheckBox.IsChecked.Value;
                Graph.ChartAreas[0].AxisY.MajorGrid.Enabled = VerticalLinesCheckBox.IsChecked.Value;

                axisXData.Add(DateTime.Now.ToString());
                axisSellData.Add(point.Sell);
                axisBuyData.Add(point.Buy);

                Graph.ChartAreas[0].AxisY.Maximum = (double)point.High;
                Graph.ChartAreas[0].AxisY.Minimum = (double)point.Low;

                Graph.Series[0].Points.DataBindXY(axisXData, axisSellData);
                Graph.Series[1].Points.DataBindXY(axisXData, axisBuyData);

                if (decimal.Parse(SellPriceLabel.Content.ToString()) > point.Sell)
                    SellPriceLabel.Foreground = System.Windows.Media.Brushes.MediumVioletRed;
                else SellPriceLabel.Foreground = System.Windows.Media.Brushes.LightGreen;
                if (decimal.Parse(BuyPriceLabel.Content.ToString()) > point.Buy)
                    BuyPriceLabel.Foreground = System.Windows.Media.Brushes.MediumVioletRed;
                else BuyPriceLabel.Foreground = System.Windows.Media.Brushes.LightGreen;
                SellPriceLabel.Content = point.Sell;
                BuyPriceLabel.Content = point.Buy;
            
            CommandManager.InvalidateRequerySuggested();
        }


        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                allCurrencyPairs = await repository.GetAllPairs();
            }
            catch (Exception)
            {
                MessageBox.Show("Exception during connection attempt. Application will be closed soon. We are terribly sorry. Goodbye", "Critical error",MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
            
            CurrencyChooseComboBox.ItemsSource = allCurrencyPairs;
            CurrencyNameChoosePanel.Visibility = Visibility.Visible;

            dispatcherTimer.Start();

            CurrencyChooseComboBox.IsEnabled = true;
            StartButton.IsEnabled = false;
            SaveButton.IsEnabled = true;
            LoadButton.IsEnabled = true;

            BuyTitleLabel.Visibility = Visibility.Visible;
            SellTitleLabel.Visibility = Visibility.Visible;

            HorizontalLinesCheckBox.Visibility = Visibility.Visible;
            VerticalLinesCheckBox.Visibility = Visibility.Visible;

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            CurrencyChooseComboBox.DisplayMemberPath = "FullName";
            if (CurrencyChooseComboBox.SelectedItem != null)
            {
                Graph.Titles.Clear();                
                Title t = new Title(((ICurrencyPair)CurrencyChooseComboBox.SelectedItem).FullName);
                t.Font = new Font("Segoe UI", 15);
                Graph.Titles.Add(t);
            }
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            CurrencyChooseComboBox.DisplayMemberPath = "ShortName";
            if (CurrencyChooseComboBox.SelectedItem != null)
            {
                Graph.Titles.Clear();
                Title t = new Title(((ICurrencyPair)CurrencyChooseComboBox.SelectedItem).ShortName);
                t.Font = new Font("Segoe UI", 15);
                Graph.Titles.Add(t);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CurrencyChooseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICurrencyPair temp = (ICurrencyPair)CurrencyChooseComboBox.SelectedItem;
            Graph.Titles.Clear();
            if (temp != null)
            {
                Title t = new Title(RadioButtonSN.IsChecked.Value ? temp.ShortName : temp.FullName);
                t.Font = new Font("Segoe UI", 15);
                Graph.Titles.Add(t);
            }
            axisXData.Clear();
            axisBuyData.Clear();
            axisSellData.Clear();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "JSON files (*.json)|*.json";
                if (openFile.ShowDialog() == true)
                {
                    string openPath = openFile.FileName;
                    string jsonLoad = File.ReadAllText(openPath);
                    var RawData = JsonConvert.DeserializeObject<StatisticData>(jsonLoad);
                    CurrencyChooseComboBox.SelectedIndex = RawData.CurrencyPairID;
                    axisXData.InsertRange(0, RawData.axisXData);
                    axisSellData.InsertRange(0, RawData.axisSellData);
                    axisBuyData.InsertRange(0, RawData.axisBuyData);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error during loading");
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "JSON files (*.json)|*.json";
                if (saveFile.ShowDialog() == true)
                {
                    string savePath = saveFile.FileName;
                    string jsonSave = JsonConvert.SerializeObject(
                        new StatisticData
                        (
                            CurrencyChooseComboBox.SelectedIndex,
                            ((ICurrencyPair)CurrencyChooseComboBox.SelectedItem).ShortName,
                            axisXData,
                            axisSellData,
                            axisBuyData
                        ));
                    File.WriteAllText(savePath, jsonSave);
                    MessageBox.Show("Your file is at " + savePath, "Cохранение увенчалось успехом");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error during saving");
            }
        }

        private void CreditsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Made by Artem Payson and Ekaterina Fofina", "Credits", MessageBoxButton.YesNoCancel);
        }

    }
}
