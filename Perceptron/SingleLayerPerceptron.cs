using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using ExstensionMethods;

namespace Perceptron
{
    public class SingleLayerPerceptron
    {
        double[] _weights;

        double[,] _x;

        double[] _y;

        private double _epsilon = 0.01;

        private double _h = 0.3;

        public void Train(double[,] x, double[] y)
        {
            InitWeight(x.GetLength(1));

            _x = x;
            _y = y;

            
            double globalEpsilon = 20;
            double lastGlobalEpsilon = 25;

            int k = 0;
            while (lastGlobalEpsilon - globalEpsilon > _epsilon)
            {
                lastGlobalEpsilon = globalEpsilon;
                globalEpsilon = 0;

                for (int i = 0; i < _x.GetLength(0); i++)
                {
                    double s = 0;
                    double localEpsilon = 0;
                    for (int j = 0; j < _x.GetLength(1); j++)
                    {
                        s += _weights[j] * _x[i, j];
                    }

                    localEpsilon += _y[i] - ActivationFunction(s);

                    for (int j = 0; j < _x.GetLength(1); j++)
                    {
                        _weights[j] = _weights[j] + _h*localEpsilon*_x[i, j];
                    }

                    globalEpsilon += Math.Abs(localEpsilon);
                }
            }
        }

        private double Normalize(int x, double max, double min)
        {
            return (max - x)/(max - min);
        }

        private static double ActivationFunction(double s)
        {
            return 1.0/(1.0 + Math.Pow(Math.E, -s));
        }

        private void InitWeight(int length)
        {
            _weights = new double[length];
        }

        public double Predict(double[] x)
        {
            double s = 0;
            for (int i = 0; i < x.Length; i++)
            {
                s += _weights[i]*x[i];
            }

            return ActivationFunction(s);
        }
    }
}
