using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Utils
{
    public static class Utils
    {
        public static T RandomElement<T>(this IEnumerable<T> collection)
        {
            var list = collection.ToList();

            return list.ElementAt(Random.Range(0, list.Count - 1));
        }

    }
}