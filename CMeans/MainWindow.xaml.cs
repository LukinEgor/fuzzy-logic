using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ExstensionMethods;
using Fuzzy_logic;

namespace CMeans
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<float[]> _centers = new List<float[]>();

        private int[] _pointsX;

        private int _currentIndex;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            _pointsX = textBox.Text.Split(' ').Select(int.Parse).ToArray();

            var cluster = new CMeansCluster(_pointsX, int.Parse(textBox1.Text));

            cluster.GettingCenter += (send, centers) =>
            {
                _centers.Add(centers);
            };

            cluster.GetResult();

            DrawNextIteration();
        }

        private void DrawNextIteration()
        {
            try
            {
                float[] centers = GetNextCenters();
                Scalies scales = CreateScales(centers);
                DrawScale(scales);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Последняя итерация");
            }
        }

        private Scalies CreateScales(float[] centers)
        {
            Scalies scalies = new Scalies();
            //centers.ForEach(c => scalies.ScaleList.Add(new Scale()
            //{
            //    A = new Point(c - 50, 0),
            //    B = new Point(c, 1),
            //    C = new Point(c + 50, 0)
            //}));

            //_pointsX.ForEach(p => scalies.Add(p));

            foreach (var center in centers)
            {
                scalies.ScaleList.Add(new Scale()
                {
                    A = new Point(center - 50, 0),
                    B = new Point(center, 1),
                    C = new Point(center + 50, 0)
                });
            }

            foreach (var i in _pointsX)
            {
                scalies.Add(i);
            }

            return scalies;
        }

        private float[] GetNextCenters()
        {
            var center = _centers.ToArray().Reverse().ToArray();

            if (_currentIndex + 1 < _centers.Count)
                return center[_currentIndex++];
            throw new IndexOutOfRangeException();
        }

        private void DrawScale(Scalies scales)
        {
            Canvas.Children.Clear();
            foreach (var scale in scales.ScaleList)
            {
                Fuzzy_logic.DrawScale.Draw(scale, Canvas);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DrawNextIteration();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Children.Clear();

            _centers.Clear();
        }
    }
}
