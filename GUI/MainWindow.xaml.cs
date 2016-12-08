using Logic;
using Logic.Model;
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
        public MainWindow()
        {
            InitializeComponent();
            repository = new Repository();
            timer = new Timer(3000);
            timer.Elapsed += UpdateGraph;
            
        }

        private void UpdateGraph(object sender, ElapsedEventArgs e)
        {
        
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
           // if (CurrencyChooseComboBox.SelectedItem != null)
               // Graph.Title = ((ICurrencyPair)CurrencyChooseComboBox.SelectedItem).FullName;

        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            CurrencyChooseComboBox.DisplayMemberPath = "ShortName";
          //  if (CurrencyChooseComboBox.SelectedItem != null)
              //  Graph.Title = ((ICurrencyPair)CurrencyChooseComboBox.SelectedItem).ShortName;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CurrencyChooseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ICurrencyPair temp = (ICurrencyPair)CurrencyChooseComboBox.SelectedItem;
           // Graph.Title = RadioButtonSN.IsChecked.Value ? temp.ShortName : temp.FullName;

        }
    }
}
