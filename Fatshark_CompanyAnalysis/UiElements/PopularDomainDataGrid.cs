using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fatshark_CompanyAnalysis.UiElements
{
    class PopularDomainDataGrid : DataGrid
    {
        public PopularDomainDataGrid(Dictionary<string, int> popularDomains)
        {
            AutoGeneratingColumn += ModifyHeaders;
            ItemsSource = popularDomains;
        }

        private void ModifyHeaders(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "Key":
                    e.Column.Header = $"Domain";
                    break;
                case "Value":
                    e.Column.Header = "Count";
                    break;
            }
        }
    }
}
