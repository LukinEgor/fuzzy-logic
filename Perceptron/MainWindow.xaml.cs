using System.Windows;
using System.Windows.Media;
using ExstensionMethods;
using lab2;

namespace Perceptron
{
    public partial class MainWindow : Window
    {
        double[,] _x;

        double[] _y;

        int[] _data = { 120, 125, 124, 119, 132, 131, 125, 128, 122, 128, 124, 120, 136, 121, 123, 123 };

        int accurancy = 3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var data = _data.Normalize().Half();
            while (data.Length != _data.Length)
            {
                PrepareData(data, accurancy);

                var perceptron = new SingleLayerPerceptron();
                perceptron.Train(_x, _y);

                var lastSample = GetLastSample(data, accurancy);

                var y = perceptron.Predict(lastSample);

                data = data.Add(y);
            }

            Point[] points = GetPoints(data);

            Diagram.Draw(points, Canvas, Colors.Red, null);
        }

        private double[] GetLastSample(double[] data, int count)
        {
            var result = new double[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = data[data.Length - count - i - 1];
            }

            return result;
        }

        private void PrepareData(double[] data, int accurancy)
        {
           int count = data.Length - accurancy;
            _x = new double[count, accurancy];
            _y = new double[count];

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < accurancy; j++)
                {
                    _x[i, j] = data[i + j];
                }

                _y[i] = data[i + accurancy];
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Point[] points = GetPoints(_data.Normalize());

            Diagram.Draw(points, Canvas, Colors.Blue, null);
        }

        private Point[] GetPoints(double[] data)
        {
            Point[] points = new Point[data.Length];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point(i, data[i]);
            }

            return points;
        }
    }
}
