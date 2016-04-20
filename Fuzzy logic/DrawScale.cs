using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fuzzy_logic
{
    public static class DrawScale
    {
        public static void Draw(Scale scale, Canvas canvas)
        {
            DrawCell(canvas, Colors.Black);

            DrawPoint(IncreaseY(scale.A, canvas), canvas, Colors.Black);
            DrawPoint(IncreaseY(scale.B, canvas), canvas, Colors.Black);
            DrawPoint(IncreaseY(scale.C, canvas), canvas, Colors.Black);
            DrawLine(IncreaseY(scale.A, canvas), IncreaseY(scale.B, canvas), canvas, Colors.Black);
            DrawLine(IncreaseY(scale.B, canvas), IncreaseY(scale.C, canvas), canvas, Colors.Black);

            //DrawLabel(scale, canvas);

            var random = new Random();
            foreach (var point in scale.GetPoint())
            {
                DrawPoint(IncreaseY(point, canvas), canvas, GetRandomColor(random.Next(0, 5)));
            }
        }

        //private static void DrawLabel(Scale scale, Canvas canvas)
        //{
        //    var point = IncreaseY(scale.B, canvas);


        //    var textBox = new TextBox
        //    {
        //        Text = scale.Name,
        //        Margin = new Thickness(point.X - 10, point.Y - 50, 0, 0)
        //    };

        //    textBox.TextChanged += (sender, args) => { scale.Name = ((TextBox) sender).Text; };

        //    canvas.Children.Add(textBox);
        //}

        public static Point IncreaseY(Point point, Canvas canvas)
        {
            return new Point(point.X + 10, point.Y*canvas.Height);
        }

        public static Point DecreaseY(Point point, Canvas canvas)
        {
            return new Point(point.X - 10, point.Y/canvas.Height);
        }

        public static Color GetRandomColor(int i)
        {
            switch (i)
            {
                case 0:
                    return Colors.Yellow;
                case 1:
                    return Colors.Blue;
                case 2:
                    return Colors.SpringGreen;
                case 3:
                    return Colors.Crimson;
                case 4:
                    return Colors.DarkOrange;
                default:
                    return Colors.Cyan;
            }
        }

        public static void DrawPoint(Point p0, Canvas canvas, Color color)
        {
            var ellipse = new Ellipse
            {
                Fill = new SolidColorBrush(color),
                Stroke = new SolidColorBrush(color),
                Width = 10,
                Height = 10,
                Margin = new Thickness(p0.X - 5, canvas.Height - p0.Y - 5, 0, 0)
            };

            var text = new TextBlock();
            ellipse.MouseDown += (obj, e) =>
            {
                text = new TextBlock
                {
                    Text = $"({DecreaseY(p0, canvas).X:f2}, {DecreaseY(p0, canvas).Y:f2})",
                    Margin = new Thickness(p0.X + 10, canvas.Height - p0.Y, 0, 0)
                };

                canvas.Children.Add(text);
            };

            ellipse.MouseUp += (obj, e) =>
            {
                if (canvas.Children.Contains(text))
                    canvas.Children.Remove(text);
            };

            canvas.Children.Add(ellipse);
        }

        private static void DrawLine(Point point, Point point1, Canvas canvas, Color color)
        {
            var line = new Line
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

        private static void DrawCell(Canvas canvas, Color color)
        {
            DrawLine(new Point(0, 0), new Point(0, canvas.Height), canvas, color);
            DrawLine(new Point(0, 0), new Point(canvas.Width, 0), canvas, color);

            var divisionX = (int) canvas.Width/50;
            var divisionY = (int) canvas.Height/10;

            for (var i = 1; i <= divisionX; i++)
                DrawLine(new Point(i*50, 0), new Point(i*50, 10), canvas, color);

            for (var i = 1; i <= 10; i++)
                DrawLine(new Point(0, divisionY*i), new Point(10, divisionY*i), canvas, color);
        }

        public static double GetAccesory(int count, Scale scale, Canvas canvas)
        {
            Point max, min;
            if (count > scale.A.X && count < scale.B.X)
            {
                max = scale.B;
                min = scale.A;
            }
            else if (count > scale.B.X && count < scale.C.X)
            {
                max = scale.C;
                min = scale.B;
            }
            else
            {
                throw new Exception("Точка выходит за пределы шкалы");
            }

            return (max.Y - min.Y)*(count - min.X)/(max.X - min.X) + max.Y;
        }
    }
}