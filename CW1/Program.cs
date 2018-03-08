using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW1
{
    class Program
    {
        static double Func(double x, double y)
        {
            return -3 * x * x + 1.5 * y * y + 3.6 * x * y - x * 18.0 / 50 - y * 72.0 / 25;
        }

        static double[][] DiscreteFunction(int N)
        {
            var result = new double[N][];
            for(int i=0; i< N; i++)
            {
                result[i] = new double[N];
                for (int j = 0; j < N; j++)
                {
                    result[i][j] = Func((double)i/N, (double)j/N);
                }
            }
            return result;
        }

        static Tuple<int,int> MaxMin(double[][] matrix)
        {
            var minIndexes = matrix.Select((vector) => Array.IndexOf(vector, vector.Min())).ToArray();
            var minimals = matrix.Select((vector) => vector.Min()).ToArray();
            var maxmin = minimals.Max();
            var i = Array.IndexOf(minimals, maxmin);
            var j = minIndexes[i];
            return new Tuple<int, int>(i, j);
        }

        static void Main(string[] args)
        {
\            for(int i =2; i < 30; i++)
            {
                var matrix = DiscreteFunction(i);
                var result = MaxMin(matrix);
                Console.WriteLine($"N={i,4} x={((double)result.Item1 / i).ToString("F5")} y={((double)result.Item2 / i).ToString("F5")} H(x,y)={matrix[result.Item1][result.Item2]}");
            }
            Console.ReadKey();
        }
    }
}
