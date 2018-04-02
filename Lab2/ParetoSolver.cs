using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTLabs.MatrixLib;

namespace GTLabs.Lab2
{
    class ParetoSolver : BimatrixGameSolver
    {
        private enum ParetoCompareResult { Above, Before, Incomparable}

        private HashSet<(int, int)> _currentParetoSet;

        private ParetoCompareResult _ParetoCompare((int, int) leftPoint, (int, int) rightPoint)
        {
            int i1 = leftPoint.Item1;
            int j1 = leftPoint.Item2;
            int i2 = rightPoint.Item1;
            int j2 = rightPoint.Item2;

            bool aboveCondition1 = (FirstMatrix[i1, j1] > FirstMatrix[i2, j2]) && (SecondMatrix[i1, j1] >= SecondMatrix[i2, j2]);
            bool aboveCondition2 = (FirstMatrix[i1, j1] >= FirstMatrix[i2, j2]) && (SecondMatrix[i1, j1] > SecondMatrix[i2, j2]);
            bool beforeCondition1 = (FirstMatrix[i1, j1] < FirstMatrix[i2, j2]) && (SecondMatrix[i1, j1] <= SecondMatrix[i2, j2]);
            bool beforeCondition2 = (FirstMatrix[i1, j1] <= FirstMatrix[i2, j2]) && (SecondMatrix[i1, j1] < SecondMatrix[i2, j2]);

            if (aboveCondition1 || aboveCondition2) return ParetoCompareResult.Above;
            else if (beforeCondition1 || beforeCondition2) return ParetoCompareResult.Before;
            else return ParetoCompareResult.Incomparable;
        }

        private void _UpdateParetoSet((int,int) paretoCandidate)
        {
            var isIncomparableWithAll = true;
            var isAboveThanOther = false;
            var endLoop = false;
            var setToRemove = new HashSet<(int, int)>();
            foreach(var paretoPoint in _currentParetoSet)
            {
                switch (_ParetoCompare(paretoCandidate, paretoPoint))
                {
                    case ParetoCompareResult.Above:
                        setToRemove.Add(paretoPoint);
                        isAboveThanOther = true;
                        isIncomparableWithAll = false;
                        break;
                    case ParetoCompareResult.Before:
                        endLoop = true;
                        isIncomparableWithAll = false;
                        break;
                    case ParetoCompareResult.Incomparable:                       
                        break;
                }
                if (endLoop) break;
            }
            _currentParetoSet.ExceptWith(setToRemove);
            if(isIncomparableWithAll || isAboveThanOther) _currentParetoSet.Add(paretoCandidate);
        }

        public override IEnumerable<(int, int)> Solve()
        {
            _currentParetoSet = new HashSet<(int, int)>();
            _currentParetoSet.Add((0, 0));
            for(int i=0; i< FirstMatrix.Rows; i++)
            {
                for(int j=(i==0)?1:0; j < FirstMatrix.Columns; j++)
                {
                    _UpdateParetoSet((i, j));
                }
            }
            return _currentParetoSet;
        }

        public ParetoSolver(Matrix<int> firstMatrix, Matrix<int> secondMatrix) : base(firstMatrix, secondMatrix){}

    }
}
