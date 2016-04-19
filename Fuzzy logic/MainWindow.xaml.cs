using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fuzzy_logic
{
    public partial class MainWindow : Window
    {
        readonly List<Scale> _scaleList;

        public MainWindow()
        {
            InitializeComponent();

            _scaleList = new List<Scale>();

            var listX = GenerateX(0, 100, 3);
            Random random = new Random();
            Scale scale1 = new Scale()
            {
                A = new Point(0, 0),
                B = new Point(50, 1),
                C = new Point(100, 0),
                Name = "Низкий"
            };

            foreach (var x in listX)
                scale1.AddPoint(x);          

            _scaleList.Add(scale1);

            Refresh();
        }

        private void Refresh()
        {
            Canvas.Children.Clear();
            foreach (var scale in _scaleList)
            {
                DrawScale.Draw(scale, Canvas);
                Canvas.Children.Add(new TextBlock()
                {
                    Text = scale.Name,
                    Margin = new Thickness(scale.B.X - 20, 220, 0, 0),
                    FontSize = 20
                });
            }
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count, a, b, c;
                if (int.TryParse(textBox.Text, out count) && int.TryParse(textBoxA.Text, out a) &&
                    int.TryParse(textBoxB.Text, out b) && int.TryParse(textBoxC.Text, out c))
                {
                    var listX = GenerateX(a, c, count);
                    Scale scale = new Scale()
                    {
                        A = new Point(a, 0),
                        B = new Point(b, 1),
                        C = new Point(c, 0),
                        Name = TextBoxName.Text,
                    };

                    foreach (var x in listX)
                        scale.AddPoint(x);

                    _scaleList.Add(scale);

                    Refresh();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }           
        }

        private List<int> GenerateX(int start, int finish, int count)
        {
            Random rand = new Random();
            var list = new List<int>();
            for (int i = 0; i < count; i++)
            {
                list.Add(rand.Next(start, finish));
            }
            return list;
        }

        private void Canvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
