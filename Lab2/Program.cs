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
        static void PrintNashParetoPoints(Matrix<int> first, Matrix<int> second, string message)
        {
            Console.WriteLine(message);
            var nashSolver = new NashSolver(first, second);
            var paretoSolver = new ParetoSolver(first, second);
            var nashPoints = nashSolver.Solve();
            var paretoPoints = paretoSolver.Solve();
            Console.Write("Равновесные по Нешу ситуации: {");
            nashPoints.ToList().ForEach(p => Console.Write($"{p} "));
            Console.Write("}\nОптимальные по Парето ситуации: {");
            paretoPoints.ToList().ForEach(p => Console.Write($"{p} "));
            Console.Write("}\nПересечение: {");
            paretoPoints.Intersect(nashPoints).ToList().ForEach(p => Console.Write($"{p} "));
            Console.WriteLine("}");
        }

        static Matrix<int> RandomMatrix(Random random ,int rows, int columns)
        {
            var result = new Matrix<int>(rows, columns);
            for(int i =0; i< result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    result[i, j] = random.Next(10);
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            Matrix<int> firstPrisoners = new Matrix<int>( new[,]{ { 1, 10 }, { 0, 5 } });
            Matrix<int> secondPrisoners = new Matrix<int>(new[,] { {1, 0 }, { 10, 5 } });
            PrintNashParetoPoints(firstPrisoners, secondPrisoners, "Диллема заключённого");

            Matrix<int> firstSexes = new Matrix<int>(new[,] { { 1, 0 }, { 0, 4 } });
            Matrix<int> secondSexes = new Matrix<int>(new[,] { { 4, 0 }, { 0, 1 } });
            PrintNashParetoPoints(firstSexes, secondSexes, "\nСемейный спор");

            var random = new Random();
            Matrix<int> firstRandom = RandomMatrix(random, 10, 10);
            Matrix<int> secondRandom = RandomMatrix(random, 10, 10);
            var messageForRandom = $"\nСлучайные матрицы\n\nПервая матрица\n{firstRandom}\nВторая матрица\n{secondRandom}";
            PrintNashParetoPoints(firstRandom, secondRandom, messageForRandom);

            Console.ReadKey();

        }
    }
}
