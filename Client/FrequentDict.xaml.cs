using System.Collections.Generic;
using System.Windows;
using System.Data;
using System.Linq;
using App;

namespace Client;

public partial class FrequentDict : Window
{
    public FrequentDict(Dictionary<char, double> dataSource)
    {
        InitializeComponent();

        var i = 1;
        dataGridFreqDict.ItemsSource =
            (from pair in dataSource
             orderby pair.Value descending
             select new { Номер = i++, Буква = pair.Key, Частота = pair.Value }).ToList();

        i = 1;
        dataGridPrimaryFreqDict.ItemsSource =
            (from pair in Constants.PrimaryDict
             orderby pair.Value descending
             select new { Номер = i++, Буква = pair.Key, Частота = pair.Value }).ToList();
    }

    private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
}