using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLabs
{
    static class DoubleExtensions
    {
        public static bool Equals6DigitPrecision(this double left, double right)
        {
            return Math.Abs(left - right) < 0.000001;
        }

    }
}
