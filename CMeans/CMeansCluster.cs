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
        private int _clusterCount;

        private int[] _objects;

        private float[] _centersClusters;

        private float[,] _matrix;

        private float _epsilon;

        private int _iteration;

        private float _coefficient;

        public CMeansCluster(int[] objects, int clusters, float coefficient = 1.6f, float epsilon = 0.1f, int iter = 10000)
        {
            _objects = objects;
            _clusterCount = clusters;
            _epsilon = epsilon;
            _iteration = iter;
            _centersClusters = new float[_clusterCount];
            _coefficient = coefficient;

            Init();
        }

        private void Init()
        {
            _matrix = new float[_clusterCount, _objects.Length];

            Random random = new Random();
            for (int i = 0; i < _clusterCount; i++)
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
                ComputeMatrix();

                e -= GetFunctionValue();

                i++;
            }

            return _matrix;
        }

        private float GetFunctionValue()
        {
            float sum = 0;

            for (int i = 0; i < _clusterCount; i++)
            {
                for (int j = 0; j < _objects.Length; j++)
                {
                    sum += (float)Math.Pow(_matrix[i, j], _coefficient)*Math.Abs(_objects[j] - _centersClusters[i]);
                }
            }

            return sum;
        }

        private void ComputeMatrix()
        {
            for (int i = 0; i < _clusterCount; i++)
            {
                for (int j = 0; j < _objects.Length; j++)
                {
                    float sum = 0;
                    for (int l = 0; l < _clusterCount; l++)
                    {

                        sum += (float)Math.Pow(Math.Abs(_objects[j] - _centersClusters[i])/Math.Abs(_objects[j] - _centersClusters[l]), 2 / (_coefficient  - 1));
                    }
                    _matrix[i, j] = 1/sum;
                }
            }
        }

        private void ComputeCenterCluster()
        {
            for (int i = 0; i < _clusterCount; i++)
            {
                float divident = 0;
                float divider = 0;
                for (int j = 0; j < _objects.Length; j++)
                {
                    divident += (float) Math.Pow(_matrix[i, j], _coefficient)*_objects[j];
                    divider += (float)Math.Pow(_matrix[i, j], _coefficient);
                }

                _centersClusters[i] = divident/divider;
            }
        }
    }
}
