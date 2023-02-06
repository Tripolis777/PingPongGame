using System;
using System.Collections.Generic;

namespace Source.Core
{
    public static class ArrayExtensions
    {
        public static int IndexOf<T>(this IList<T> array, Func<T, bool> action)
        {
            if (array == null)
                return -1;
            
            for (var index = 0; index < array.Count; index++)
            {
                if (action(array[index]))
                    return index;
            }

            return -1;
        }
    }
}