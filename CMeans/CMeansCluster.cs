using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMeans
{
    public class CMeansCluster
    {
        private readonly int[] _objects;

        private readonly float[] _clusters;

        private float[,] _matrix;

        private readonly float _epsilon;

        private readonly int _iteration;

        private readonly float _coefficient;

        public CMeansCluster(int[] objects, int clusters, float coefficient = 1.6f, float epsilon = 0.0001f, int iter = 10000)
        {
            _objects = objects;
            _epsilon = epsilon;
            _iteration = iter;
            _clusters = new float[clusters];
            _coefficient = coefficient;

            Init();
        }

        public float[] GetCenter()
        {
            return _clusters;
        }

        private void Init()
        {
            _matrix = new float[_clusters.Length, _objects.Length];

            Random random = new Random();
            for (int i = 0; i < _clusters.Length; i++)
            {
                for (int j = 0; j < _objects.Length; j++)
                {
                    _matrix[i, j] = (float)random.NextDouble();
                }
            }
        }

        public float[,] GetResult()
        {
            float e = GetFunctionValue();

            int i = 0;
            while (e > _epsilon && i <_iteration)
            {
                ComputeCenterCluster();

                var centers = (float[])_clusters.Clone();
                OnGettingCenter(centers);

                ComputeMatrix();

                e -= GetFunctionValue();

                i++;
            }

            return _matrix;
        }

        private float GetFunctionValue()
        {
            float sum = 0;

            for (int i = 0; i < _clusters.Length; i++)
            {
                for (int j = 0; j < _objects.Length; j++)
                {
                    sum += (float)Math.Pow(_matrix[i, j], _coefficient)*Math.Abs(_objects[j] - _clusters[i]);
                }
            }

            return sum;
        }

        private void ComputeMatrix()
        {
            for (int i = 0; i < _clusters.Length; i++)
            {
                for (int j = 0; j < _objects.Length; j++)
                {
                    float sum = 0;
                    for (int l = 0; l < _clusters.Length; l++)
                    {

                        sum += (float)Math.Pow(Math.Abs(_objects[j] - _clusters[i])/Math.Abs(_objects[j] - _clusters[l]), 2 / (_coefficient  - 1));
                    }
                    _matrix[i, j] = 1/sum;
                }
            }
        }

        private void ComputeCenterCluster()
        {
            for (int i = 0; i < _clusters.Length; i++)
            {
                float divident = 0;
                float divider = 0;
                for (int j = 0; j < _objects.Length; j++)
                {
                    divident += (float) Math.Pow(_matrix[i, j], _coefficient)*_objects[j];
                    divider += (float)Math.Pow(_matrix[i, j], _coefficient);
                }

                _clusters[i] = divident/divider;
            }
        }

        public EventHandler<float[]> GettingCenter = delegate { };

        private void OnGettingCenter(float[] centers)
        {
            GettingCenter?.Invoke(this, centers);
        } 
    }
}
