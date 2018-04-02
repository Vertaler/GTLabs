using System;

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
