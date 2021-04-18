using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fatshark_CompanyAnalysis.UiElements
{
    class DictionaryDataGrid : DataGrid
    {
        public Dictionary<string, string> headerDictionary;
        public DictionaryDataGrid(Dictionary<string, int> dataDictionary, Dictionary<string, string> headerDictionary)
        {
            AutoGeneratingColumn += ModifyHeaders;
            ItemsSource = dataDictionary;
            this.headerDictionary = headerDictionary;
        }

        private void ModifyHeaders(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var header = e.Column.Header.ToString();
            if (headerDictionary != null && headerDictionary.ContainsKey(header))
                e.Column.Header = headerDictionary[header];
        }

    }
}
