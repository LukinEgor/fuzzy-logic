using System;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace lab2
{
    /// <summary>
    /// Interaction logic for TimeSeriesWindow.xaml
    /// </summary>
    public partial class TimeSeriesWindow : Window
    {
        public TimeSeriesWindow()
        {
            InitializeComponent();
        }


        private void DrawGraphic(TimeSeries series)
        {
            int i = 1;
            Point[] points = series.Values.Select(v => new Point(i++, v)).ToArray();

            Diagram.Draw(points, Canvas, null);
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.FileOk += (o, args) =>
            {
                var path = ((OpenFileDialog)o).FileName;

                var series = TimeSeries.Load(path);

                GetTimeSeriesEventHandler(this, series);

                DrawGraphic(series);

            };

            dialog.ShowDialog();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public EventHandler<TimeSeries> GetTimeSeriesEventHandler = delegate { };

        public void OnGetTimeSeries(TimeSeries series)
        {
            GetTimeSeriesEventHandler?.Invoke(this, series);
        }
    }
}
