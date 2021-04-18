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
        public CompanySet CompanySet { 
            get { return companySet; } 
            set
            {
                companySet = value;
                CompanySetNameDisplay.Text = $"Selected Company Set: {companySet.Name}";
            }
        }
        CompanySet companySet;
        public MainWindow()
        {
            InitializeComponent();
            LogDataGrid.ItemsSource = logEntries;

            DataHandler = new DataHandler(this);
            CompanySet = DataHandler.GetFirstCompanySet();

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
                DataHandler.CreateCompanySetFromFile(openFileDialog.FileName);
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

    }
}
