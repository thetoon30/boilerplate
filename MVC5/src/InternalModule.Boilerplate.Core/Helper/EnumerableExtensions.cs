using System;
using System.Collections.Generic;
using System.Linq;

namespace InternalModule.Boilerplate.Core.Helper
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random rng)
        {
            T[] elements = source.ToArray();
            // Note i > 0 to avoid final pointless iteration
            for (int i = elements.Length - 1; i > 0; i--)
            {
                // Swap element "i" with a random earlier element it (or itself)
                int swapIndex = rng.Next(i + 1);
                T tmp = elements[i];
                elements[i] = elements[swapIndex];
                elements[swapIndex] = tmp;
            }
            // Lazily yield (avoiding aliasing issues etc)
            foreach (T element in elements)
            {
                yield return element;
            }
        }

        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }

        //private IEnumerable<SomeList> MergeData(IEnumerable<SomeList1> source, IEnumerable<SomeList1> destination)
        //{
        //    var newDes = from a in destination
        //                 select new SomeList
        //                 {
        //                     Id = a.Id,
        //                     Name = a.Name
        //                 };

        //    return source.Union(newDes).GroupBy(x => new { x.Id, x.Name }).Select(x => x.First());
        //}
    }
}
