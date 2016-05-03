using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExstensionMethods
{
    public static class Extensions
    {
        public static void ForEach<TSource>(this TSource source, Action<dynamic> action)
        {
            var seq = source as IEnumerable;
            if (seq != null)
            {
                foreach (var item in seq)
                {
                    action(seq);
                }
            }
        }

        public static int ToInt(this string source)
        {
            return int.Parse(source);
        }

        public static int Max(this int[,] source)
        {
            int max = -9999999;

            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    if (source[i, j] > max)
                        max = source[i, j];
                }
            }

            return max;
        }

        public static int Min(this int[,] source)
        {
            int min = 9999999;

            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    if (source[i, j] < min)
                        min = source[i, j];
                }
            }

            return min;
        }

        public static double[,] Normalize(this int[,] source)
        {
            double max = source.Max();
            double min = source.Min();

            double[,] result = new double[source.GetLength(0), source.GetLength(1)];
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    result[i, j] = (max - source[i,j])/(max - min);
                }
            }

            return result;
        }

        public static double[] Normalize(this int[] source)
        {
            double max = source.OrderBy(t => t).Last();
            double min = source.OrderBy(t => t).First();

            double[] result = new double[source.GetLength(0)];
            for (int i = 0; i < source.GetLength(0); i++)
            {
                  result[i] = (max - source[i]) / (max - min);
            }

            return result;
        }

        public static T[] Half<T>(this T[] source)
        {
            T[] result = new T[source.Length / 2];
            for (int i = 0; i < source.Length / 2; i++)
            {
                result[i] = source[i];
            }

            return result;
        }

        public static T[] Add<T>(this T[] source, T item)
        {
            var list = source.ToList();

            list.Add(item);

            return list.ToArray();
        }
    }
}
