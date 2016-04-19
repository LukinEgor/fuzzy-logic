using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExstensionMethods
{
    public static class Extensions
    {
        public static void ForEach<TSource>(this TSource source, Action<dynamic> action)
        {
            var seq = source as IEnumerable;
            if (seq != null)
                foreach (var item in seq)
                {
                    action(seq);
                }
        }
    }
}
