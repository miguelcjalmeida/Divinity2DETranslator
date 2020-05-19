using System;
using System.Collections.Generic;
using System.Linq;

namespace Divinity2DETranslator
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IList<T>> SplitIntoListsOfGivenSize<T>(this IEnumerable<T> items, int eachListSize)
        {
            var enumerable = items.ToList();

            for (int i = 0; i < enumerable.Count; i += eachListSize)
                yield return enumerable.GetRange(i, Math.Min(eachListSize, enumerable.Count - i));
        }
    }
}