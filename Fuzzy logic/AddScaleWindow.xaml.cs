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

namespace Fuzzy_logic
{
    /// <summary>
    /// Interaction logic for AddScaleWindow.xaml
    /// </summary>
    public partial class AddScaleWindow : Window
    {
        private readonly Scales _scales;

        public AddScaleWindow(Scales scales)
        {
            this._scales = scales;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            double a, b, c;
            if (double.TryParse(textBoxA.Text, out a) && double.TryParse(textBoxB.Text, out b) &&
                double.TryParse(textBoxC.Text, out c))
            {
                Scale scale = new Scale()
                {
                    A = new Point(a, 0),
                    B = new Point(b, 1),
                    C = new Point(c, 0),
                    Name = TextBoxName.Text
                };

                _scales.ScaleList.Add(scale);

                OnGetScales(_scales);

                Refresh();
            }
        }

        private void Refresh()
        {
            Canvas.Children.Clear();
            foreach (var scale in _scales.ScaleList)
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public EventHandler<Scales> GetScalesEventHandler = delegate { };

        private void OnGetScales(Scales scales)
        {
            GetScalesEventHandler?.Invoke(this, scales);
        }
    }
}
