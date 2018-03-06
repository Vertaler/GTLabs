using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLabs
{
    class AnalyticalMethod
    {
        public static GameSolution Solve(Matrix gameMatrix)
        {         
            var solution = new GameSolution();
            var gameMatrixInv = gameMatrix.Inverse();
            var unitRow = Matrix.UnitRow(gameMatrix.Rows);
            var unitColumn = Matrix.UnitColumn(gameMatrix.Columns);
            double gamePrice = 1 / (unitRow * gameMatrixInv * unitColumn)[0,0];
            var firstStrategy = gamePrice * (unitRow * gameMatrixInv);
            var secondStrategy = gamePrice* gameMatrixInv *unitColumn;
            solution.FirstPlayerStrategy = firstStrategy.Row(0);
            solution.SecondPlayerStrategy = secondStrategy.Column(0);
            return solution;
        }
    }
}
