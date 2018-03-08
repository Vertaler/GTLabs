using System;
using System.Linq;

namespace CW1
{
    class Program
    {
        static double Func(double x, double y)
        {
            return -3 * x * x + 1.5 * y * y + 3.6 * x * y - x * (18.0 / 50) - y * (72.0 / 25);
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

        static (int,int, double) MaxMin(double[][] matrix)
        {
            var minIndexes = matrix.Select((vector) => Array.IndexOf(vector, vector.Min())).ToArray();
            var minimals = matrix.Select((vector) => vector.Min()).ToArray();
            var maxmin = minimals.Max();
            var i = Array.IndexOf(minimals, maxmin);
            var j = minIndexes[i];
            return (i,j, maxmin);
        }

        static void Main(string[] args)
        {
            var x = 3.0 / 10;
            var y = 3.0 / 5;
            Console.WriteLine($"Analytical solution: x*={x.ToString("F3")}, y*={y.ToString("F3")}, H(x*,y*)={Func(x,y)}");
            for(int N =1; N <= 10; N++)
            {
                var matrix = DiscreteFunction(N);
                (int i, int j, double H) = MaxMin(matrix);
                Console.WriteLine($"N={N,-4} x={(double)i/N:F3} y={(double)j / N:F3} H(x,y)={H:F3}");
            }
            Console.ReadKey();
        }
    }
}
