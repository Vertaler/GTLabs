using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTLabs.MatrixLib;

namespace GTLabs.Lab2
{
    class NashSolver:BimatrixGameSolver
    {
        public IEnumerable<(int, int)> Solve()
        {            
            return FirstMatrix.ArgmaxColumns.Intersect(SecondMatrix.ArgmaxRows);
        }
        public NashSolver(Matrix<int> firstMatrix, Matrix<int> secondMatrix):base(firstMatrix, secondMatrix) { }

    }
}
