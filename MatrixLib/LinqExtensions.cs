using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLabs.MatrixLib
{
    static class LinqExtensions
    {
        public static IEnumerable<int> ArgMax<T>(this IEnumerable<T> collection)
        {
            var arrayCollection = collection.ToArray();
            var max = collection.Max();
            var argmax = Enumerable.Range(0,arrayCollection.Length).Where((i) => arrayCollection[i].Equals(max));
            return argmax; 
        }
        public static IEnumerable<int> ArgMin<T>(this IEnumerable<T> collection)
        {
            var arrayCollection = collection.ToArray();
            var min = collection.Min();
            var argmin = Enumerable.Range(0, arrayCollection.Length).Where((i) => arrayCollection[i].Equals(min));
            return argmin;
        }
    }
}
