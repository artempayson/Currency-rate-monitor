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
        List<ICurrencyPair> list;
        List<string> axisXData;
        List<decimal> axisYData;
        public MainWindow()
        {
            InitializeComponent();
            repository = new Repository();
           

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);


            Graph.ChartAreas.Add(new ChartArea("PlaceForGraph"));
            Graph.Series.Add(new Series("CurrencyGraph"));
            Graph.Series[0].ChartArea = "PlaceForGraph";
            Graph.Series[0].ChartType = SeriesChartType.Line;
            
            


            axisXData = new List<string> { };
            axisYData = new List<decimal> { };


        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var b = list[CurrencyChooseComboBox.SelectedIndex];
            repository.UpdateSpecifiedPair(b);
            axisXData.Add(DateTime.Now.ToString());
            axisYData.Add(b.Sell);
            Graph.ChartAreas[0].AxisY.Maximum = (double)b.High;
            Graph.ChartAreas[0].AxisY.Minimum = (double)b.Low;
            Graph.Series[0].Points.DataBindXY(axisXData, axisYData);

            CommandManager.InvalidateRequerySuggested();
        }


        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            list =await repository.GetAllPairs();

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
            Title t = new Title(RadioButtonSN.IsChecked.Value ? temp.ShortName : temp.FullName);
            t.Font = new Font("Segoe UI", 15);
            Graph.Titles.Add(t);

            axisXData.Clear();
            axisYData.Clear();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                string json = openFile.FileName;
            }
                
        }
    }
}
