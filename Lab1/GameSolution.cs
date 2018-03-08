using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLabs
{
    struct GameSolution
    {
        public double[] FirstPlayerStrategy;
        public double[] SecondPlayerStrategy;
        public override string ToString()
        {
            var resultBuilder = new StringBuilder();
            resultBuilder.Append("A: ( ");
            foreach(double x in FirstPlayerStrategy)
            {
                resultBuilder.Append(x.ToString("F3") +" ");
            }
            resultBuilder.Append(") ");

            resultBuilder.Append("B: ( ");
            foreach (double y in SecondPlayerStrategy)
            {
                resultBuilder.Append(y.ToString("F3") + " ");
            }
            resultBuilder.Append(")");

            return resultBuilder.ToString();
        }

    }
}
