using Fatshark_CompanyAnalysis.Data;
using Fatshark_CompanyAnalysis.Models;
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
        public MainWindow()
        {
            InitializeComponent();
            DataHandler = new DataHandler(this);
            LogDataGrid.ItemsSource = logEntries;
            AddLogEntry("Startup complete");
        }

        public void AddLogEntry(string message, LogType type = LogType.Info)
        {
            logEntries.Add(new LogEntry(message, type));
        }

        private void createDatasetFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                DataHandler.CreateCompanySetFromFile(openFileDialog.FileName);
        }
        private void selectDatasetButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
