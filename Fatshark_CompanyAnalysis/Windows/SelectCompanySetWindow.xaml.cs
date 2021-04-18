using Fatshark_CompanyAnalysis.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for SelectCompanySetWindow.xaml
    /// </summary>
    public partial class SelectCompanySetWindow : Window
    {
        MainWindow mainWindow;
        public SelectCompanySetWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            CreateCompanySetButtons();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CompanySetButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as CompanySetButton;
            mainWindow.CompanySet = button.CompanySet;

            Close();
        }

        private void CreateCompanySetButtons()
        {
            var companySets = mainWindow.DataHandler.GetCompanySets();

            StackPanel Sp = FindName("CompanySetStackPanel") as StackPanel;
            foreach (var companySet in companySets)
            {
                var button = new CompanySetButton(companySet);
                button.Click += CompanySetButton_Click;

                Sp.Children.Add(button);

            }
        }

        class CompanySetButton : Button
        {
            public CompanySet CompanySet { get; set; }
            public CompanySetButton(CompanySet companySet)
            {
                CompanySet = companySet;
                Content = companySet.Name;
            }
        }

    }
}
