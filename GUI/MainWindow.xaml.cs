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


            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);


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
            Graph.Series[0].IsVisibleInLegend = true;

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var point = allCurrencyPairs[CurrencyChooseComboBox.SelectedIndex];
            repository.UpdateSpecifiedPair(point);
            axisXData.Add(DateTime.Now.ToString());
            axisSellData.Add(point.Sell);
            axisBuyData.Add(point.Buy);
            Graph.ChartAreas[0].AxisY.Maximum = (double)point.High;
            Graph.ChartAreas[0].AxisY.Minimum = (double)point.Low;

            Graph.Series[0].Points.DataBindXY(axisXData, axisSellData);
            Graph.Series[1].Points.DataBindXY(axisXData, axisBuyData);

            CommandManager.InvalidateRequerySuggested();
        }


        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            allCurrencyPairs = await repository.GetAllPairs();

            SaveButton.IsEnabled = true;
            LoadButton.IsEnabled = true;
            CreditsButton.IsEnabled = true;

            CurrencyChooseComboBox.ItemsSource = await repository.GetAllPairs();
            CurrencyNameChoosePanel.Visibility = Visibility.Visible;

            dispatcherTimer.Start();

            CurrencyChooseComboBox.IsEnabled = true;
            StartButton.IsEnabled = false;

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
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "JSON files (*.json)|*.json";
            if (openFile.ShowDialog() == true)
            {
                string openPath = openFile.FileName;
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "JSON files (*.json)|*.json";
            if (saveFile.ShowDialog() == true)
            {
                string savePath = saveFile.FileName;
            }
        }
    }
}
