using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTLabs.MatrixLib;

namespace GTLabs.Lab2
{
    abstract class BimatrixGameSolver
    {
        public Matrix<int> FirstMatrix { get; set; }
        public Matrix<int> SecondMatrix { get; set; }

        abstract public IEnumerable<(int, int)> Solve();

        public BimatrixGameSolver(Matrix<int> firstMatrix, Matrix<int> secondMatrix)
        {
            FirstMatrix = firstMatrix;
            SecondMatrix = secondMatrix;
        }
    }
}
