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
using App;
using LiveCharts;
using LiveCharts.Wpf;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для Graphics.xaml
    /// </summary>
    public partial class Graphics : Window
    {
        public SeriesCollection SeriesCollection { get; set; }
        public Graphics(Dictionary<char, double> encryptArr)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Зашифрованный текст",
                    Values = new ChartValues<double>(encryptArr.Values.OrderByDescending(x => x).ToArray()),
                },
                new LineSeries
                {
                    Title = "Русский алфавит",
                    Values = new ChartValues<double>(Constants.PrimaryDict.Values.ToArray())
                }
            };


            DataContext = this;
        }
    }
}
