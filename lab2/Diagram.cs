using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace lab2
{
    public static class Diagram
    {
        public static void Draw(Point[] points, Canvas canvas, string[] labels)
        {
            DrawCell(points, canvas);

            double kx, ky;
            //if (canvas.ActualHeight == 0 && canvas.ActualWidth == 0)
            //{
            //    kx = canvas.Width / points.OrderBy(t => t.X).Last().X;
            //    ky = canvas.Height / points.OrderBy(t => t.Y).Last().Y;
            //}
            //else
            //{
                kx = canvas.Width / points.OrderBy(t => t.X).Last().X;
                ky = canvas.Height / points.OrderBy(t => t.Y).Last().Y;
          //  }

            int i = 0;
            foreach (var point in points)
            {
                DrawPoint(point, kx, ky, canvas, Colors.Blue, true, labels?[i++]);
            }
        }

        public static void Draw(Point[] points, Canvas canvas, Color color, string[] labels)
        {
            DrawCell(points, canvas);

            double kx, ky;
            //if (canvas.ActualHeight == 0 && canvas.ActualWidth == 0)
            //{
            //    kx = canvas.Width / points.OrderBy(t => t.X).Last().X;
            //    ky = canvas.Height / points.OrderBy(t => t.Y).Last().Y;
            //}
            //else
            //{
            kx = canvas.Width / points.OrderBy(t => t.X).Last().X;
            ky = canvas.Height / points.OrderBy(t => t.Y).Last().Y;
            //  }

            int i = 0;
            foreach (var point in points)
            {
                DrawPoint(point, kx, ky, canvas, color, true, labels?[i++]);
            }
        }

        private static void DrawCell(Point[] points, Canvas canvas)
        {
            DrawLine(new Point(0, 0), new Point(0, canvas.Height), canvas, Colors.Black);
            DrawLine(new Point(0, 0), new Point(canvas.Width, 0), canvas, Colors.Black);

            int divisionX = (int)canvas.Width / points.Length;
            int divisionY = (int)points.OrderBy(t => t.Y).Last().Y / 10;

            for (int i = 1; i <= divisionX; i++)
                DrawLine(new Point(i * 50, 0), new Point(i * 50, 10), canvas, Colors.Black);

            for (int i = 1; i <= 10; i++)
                DrawLine(new Point(0, divisionY * i), new Point(10, divisionY * i), canvas, Colors.Black);

        }

        private static void DrawPoint(Point p0, double kx, double ky, Canvas canvas, Color color, bool hasSignature, string label)
        {
            Ellipse ellipse = new Ellipse
            {
                Fill = new SolidColorBrush(color),
                Stroke = new SolidColorBrush(color),

                Width = 10,
                Height = 10,

                Margin = new Thickness(p0.X * kx - 5, canvas.Height - p0.Y * ky - 5, 0, 0)
            };

            canvas.Children.Add(ellipse);

            if (label == null)
                label = $"({p0.X}, {p0.Y})";

            if (hasSignature)
            {
                TextBlock text = new TextBlock()
                {
                    Text = label,
                    Margin = new Thickness(p0.X * kx + 10, canvas.Height - p0.Y * ky, 0, 0)
                };

                canvas.Children.Add(text);
            }
        }

        private static void DrawLine(Point point, Point point1, Canvas canvas, Color color)
        {
            Line line = new Line
            {
                X1 = point.X,
                Y1 = canvas.Height - point.Y,

                X2 = point1.X,
                Y2 = canvas.Height - point1.Y,

                StrokeThickness = 3,
                Stroke = new SolidColorBrush(color)
            };

            canvas.Children.Add(line);
        }
    }
}
