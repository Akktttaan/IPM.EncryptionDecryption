using System.Collections.Generic;
using System.Linq;
using System.Windows;
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
        public Graphics(Dictionary<char, double> originalArr, Dictionary<char, double> encryptArr)
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Зашифрованный текст",
                    Values = new ChartValues<double>(encryptArr.Values.OrderByDescending(x => x).ToArray())
                },
                 new LineSeries
                {
                    Title = "Оригинальный текст",
                    Values = new ChartValues<double>(originalArr.Values.OrderByDescending(x => x).ToArray())
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
