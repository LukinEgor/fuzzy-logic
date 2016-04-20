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
using ExstensionMethods;
using Fuzzy_logic;

namespace lab2
{
    /// <summary>
    /// Interaction logic for FuzzyTimeSeriesWindow.xaml
    /// </summary>
    public partial class FuzzyTimeSeriesWindow : Window
    {
        private readonly TimeSeries _series;

        private readonly Scalies _scales;

        public FuzzyTimeSeriesWindow(TimeSeries series, Scalies scales)
        {
            _series = series;
            _scales = scales;

            InitializeComponent();

            DrawGraphic();
        }

        private void DrawGraphic()
        {
            foreach (var timeSeries in _series.Values)
            {
                _scales.Add(timeSeries);
            }

            OnGetScales(_scales);

            List<Point> p = new List<Point>();
            List<string> labels = new List<string>();

            
            foreach (var scale in _scales.ScaleList)
            {
                int i = 0;
                p.AddRange(scale.GetPoint().Select(point =>
                {
                    labels.Add($"({point.X}, {point.Y}) - {scale.Name}");

                    return new Point(i++, point.Y);
                }));
            }


            Diagram.Draw(p.ToArray(), Canvas, labels.ToArray());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public EventHandler<Scalies> GetScalesEventHandler = delegate { };

        public void OnGetScales(Scalies scales)
        {
            GetScalesEventHandler?.Invoke(this, scales);
        }
    }
}
