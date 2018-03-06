using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix(3, 3);
            A[0, 0] = 1; A[0, 1] = 17; A[0, 2] = 18;
            A[1, 0] = 14; A[1, 1] = 6; A[1, 2] = 16;
            A[2, 0] = 14; A[2, 1] = 14; A[2, 2] = 13;

            //var B = A.Inverse();
            var analiticalSolution = AnalyticalMethod.Solve(A);
            var brownRobinson = new BrownRobinson(0.01);
            var brownRobinsonSolution = brownRobinson.Solve(A);
            Console.Write("\n\n");
            Console.WriteLine($"Analitical solution: {analiticalSolution}");
            Console.WriteLine($"Brown Robinson solution:{brownRobinsonSolution}");
            Console.ReadKey();
        }
    }
}
