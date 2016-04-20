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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fuzzy_logic;

namespace lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Scalies _scales;

        private TimeSeries _timeSeries;

        public MainWindow()
        {
            _scales = new Scalies();
            _timeSeries = new TimeSeries();
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            TimeSeriesWindow window = new TimeSeriesWindow();
            window.GetTimeSeriesEventHandler += GetTimeSeriesEventHandler;
            window.Show();
        }

        private void GetTimeSeriesEventHandler(object sender, TimeSeries timeSeries)
        {
            _timeSeries = timeSeries;
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            FuzzyTimeSeriesWindow window = new FuzzyTimeSeriesWindow(_timeSeries, _scales);
            window.Show();
        }

        private void GetScalesEventHandler(object sender, Scalies scales)
        {
            _scales = scales;
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            FuzzyTrendWindow window = new FuzzyTrendWindow();
            window.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddScaleWindow window = new AddScaleWindow(_scales);
            window.GetScalesEventHandler += GetScalesEventHandler;
            window.Show();
        }
    }
}
