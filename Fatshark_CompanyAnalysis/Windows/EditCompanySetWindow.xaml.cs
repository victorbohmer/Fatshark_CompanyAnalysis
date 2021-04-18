using Fatshark_CompanyAnalysis.Data;
using Fatshark_CompanyAnalysis.Models;
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
using System.Windows.Shapes;

namespace Fatshark_CompanyAnalysis.Windows
{
    /// <summary>
    /// Interaction logic for EditCompanySetWindow.xaml
    /// </summary>
    public partial class EditCompanySetWindow : Window
    {
        DataContext context;
        MainWindow mainWindow;
        public EditCompanySetWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            context = new DataContext();
            this.mainWindow = mainWindow;
            LoadData();
        }

        private void LoadData()
        {
            var companyArray = context.Companies.Where(c => c.CompanySetId == mainWindow.CompanySet.CompanySetId).ToArray();
            CompaniesDataGrid.ItemsSource = new ObservableCollection<Company>(companyArray);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await context.SaveChangesAsync();
            mainWindow.AddLogEntry("Successfully saved changes to database");
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
