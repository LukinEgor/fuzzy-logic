using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Fuzzy_logic
{
    public class Scale
    {
        public Point A { get; set; }

        public Point B { get; set; }

        public Point C { get; set; }

        public string Name { get; set; } = "Untitle";

        private readonly List<double> _pointsX;
        //private Point _a = new Point(0, 0);
        //private Point _b = new Point(500, 1);
        //private Point _c = new Point(700, 0);

        public Scale()
        {
            _pointsX = new List<double>();
        }

        public void AddPoint(Point point)
        {
            _pointsX.Add(point.X);
        }

        public void AddPoint(int x)
        {
            _pointsX.Add(x);
        }

        private double CalculateY(double x)
        {
            if (x < A.X || x > C.X)
                throw new ArgumentOutOfRangeException();

            Point max, min;
            if (x >= A.X && x <= B.X)
            {
                max = B;
                min = A;
            }
            else
            {
                max = C;
                min = B;
            }

            return (max.Y - min.Y) * (x - min.X) / (max.X - min.X) + min.Y;
        }

        public List<Point> GetPoint()
        {
            List<Point> points = new List<Point>();

            foreach (var point in _pointsX)
            {
                Point p;
                try
                {
                    p = new Point(point, CalculateY(point));
                }
                catch (ArgumentOutOfRangeException)
                {
                    p = new Point(0, 0);
                }

                points.Add(p);
            }
            return points;
        }
    }
}
