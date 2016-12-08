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
namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Timer timer;
        Repository repository;
        string[] axisXData;
        decimal[] axisYData;
        public MainWindow()
        {
            InitializeComponent();
            repository = new Repository();
            timer = new Timer(3000);
            timer.Elapsed += UpdateGraph;

            Graph.ChartAreas.Add(new ChartArea("PlaceForGraph"));
            Graph.Series.Add(new Series("CurrencyGraph"));
            Graph.Series[0].ChartArea = "PlaceForGraph";
            Graph.Series[0].ChartType = SeriesChartType.Line;
            axisXData = new string[] { "a", "b", "c" };
            axisYData = new decimal[] { 0.1m, 1.5m, 1.9m };


        }

        private void UpdateGraph(object sender, ElapsedEventArgs e)
        {
            Graph.Series[0].Points.DataBindXY(axisXData, axisYData);
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SaveButton.IsEnabled = true;
            LoadButton.IsEnabled = true;
            CreditsButton.IsEnabled = true;

            CurrencyChooseComboBox.ItemsSource = await repository.GetAllPairs();
            CurrencyNameChoosePanel.Visibility = Visibility.Visible;
            timer.Enabled = true;
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

        }
    }
}
