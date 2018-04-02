using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLabs.MatrixLib
{
    static class LinqExtensions
    {
        public static int ArgMax<T>(this IEnumerable<T> collection)
        {
            var max = collection.Max();
            var argmax = Array.IndexOf(collection.ToArray(), max);
            return argmax; 
        }
        public static int ArgMin<T>(this IEnumerable<T> collection)
        {
            var min = collection.Min();
            var argmin = Array.IndexOf(collection.ToArray(), min);
            return argmin;
        }
    }
}
