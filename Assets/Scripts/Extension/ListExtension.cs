using System.Collections.Generic;
using System;

namespace CardMatch
{
    public static class ListExtension
    {
        private static Random rng = new Random();

        public static void ShuffleCards<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}