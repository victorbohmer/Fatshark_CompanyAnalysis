using Fatshark_CompanyAnalysis.API;
using Fatshark_CompanyAnalysis.Data;
using Fatshark_CompanyAnalysis.Models;
using Fatshark_CompanyAnalysis.UiElements;
using Fatshark_CompanyAnalysis.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fatshark_CompanyAnalysis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataHandler DataHandler;
        ObservableCollection<LogEntry> logEntries = new ObservableCollection<LogEntry>();
        public CompanySet CompanySet
        {
            get { return companySet; }
            set
            {
                companySet = value;
                CompanySetNameDisplay.Text = $"Selected company set: {companySet.Name}";
            }
        }
        CompanySet companySet;
        public MainWindow()
        {
            InitializeComponent();
            LogDataGrid.ItemsSource = logEntries;

            DataHandler = new DataHandler(this);
            DataHandler.SetLatestCompanySetAsActive();

            AddLogEntry("Startup complete");
        }

        public void AddLogEntry(string message, LogType type = LogType.Info)
        {
            logEntries.Add(new LogEntry(message, type));
        }

        private void CreateDatasetFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                DataHandler.CreateCompanySetFromFile(openFileDialog.FileName);
                DataHandler.SetLatestCompanySetAsActive();
            }
                
        }
        private void SelectCompanySetButton_Click(object sender, RoutedEventArgs e)
        {
            var subWindow = new SelectCompanySetWindow(this);
            subWindow.Show();
        }

        private void DisplayPopularDomainsButton_Click(object sender, RoutedEventArgs e)
        {
            var popularDomains = DataHandler.GetPopularDomains();

            var headerDictionary = new Dictionary<string, string>()
            {
                {"Key", "Domain" },
                {"Value", "Count" }
            };

            DataDisplayStackPanel.Children.Clear();
            DataDisplayStackPanel.Children.Add(
                new DictionaryDataGrid(popularDomains, headerDictionary)
                );
        }

        private async void DisplayCompanyClustersButton_Click(object sender, RoutedEventArgs e)
        {
            var scaleInMeters = 20000;
            var companyClusters = await DataHandler.GetCompanyClusters(scaleInMeters);

            var headerDictionary = new Dictionary<string, string>()
            {
                {"Key", $"Northings, Eastings (scale: {scaleInMeters} meters)" },
                {"Value", "Number of people" }
            };

            DataDisplayStackPanel.Children.Clear();
            DataDisplayStackPanel.Children.Add(
                new DictionaryDataGrid(companyClusters, headerDictionary)
                );
        }

        private async void DisplayCountryDistributionButton_Click(object sender, RoutedEventArgs e)
        {
            var countryDistribution = await DataHandler.GetCountryDistribution();

            var headerDictionary = new Dictionary<string, string>()
            {
                {"Key", $"Country" },
                {"Value", "Percentage of people" }
            };

            DataDisplayStackPanel.Children.Clear();
            DataDisplayStackPanel.Children.Add(
                new DictionaryDataGrid(countryDistribution, headerDictionary)
                );
        }

        private void EditCompanySetButton_Click(object sender, RoutedEventArgs e)
        {
            var subWindow = new EditCompanySetWindow(this);
            subWindow.Show();
        }

    }
}
