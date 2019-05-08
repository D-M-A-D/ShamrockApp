using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shamrock
{
    public static class CombinationHelper
    {
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }
    }
    class EqualityArrayInt : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x.SequenceEqual(y))
                return true;
            else
                return false;
            // Consider all even numbers the same, and all odd the same.
            //return GetHashCode(x) == GetHashCode(y);
        }

        public int GetHashCode(int[] obj)
        {
            return string.Join(", ", obj).GetHashCode();
        }
    }
}
