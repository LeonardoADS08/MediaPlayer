using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlayer.Core.Extensions.Linq
{
    public static class IEnumerableForEach
    {
        public static void ForEach<T>(this IEnumerable<T> col, Action<T> action)
        {
            if (action != null)
            {
                foreach (var item in col)
                {
                    action(item);
                }
            }
        }
    }
}
