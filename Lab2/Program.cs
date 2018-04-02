using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTLabs.MatrixLib;

namespace GTLabs.Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix<int> first = new Matrix<int>( new[,]{ { 1, 10 }, { 0, 5 } });
            Matrix<int> second = new Matrix<int>(new[,] { {1, 0 }, { 10, 5 } });

            var nashSolver = new NashSolver(first, second);
            var nashPoints = nashSolver.Solve().ToArray();
            Console.WriteLine("End");

        }
    }
}
