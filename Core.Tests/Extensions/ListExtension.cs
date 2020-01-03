using System;
using System.Collections.Generic;

namespace Core.Tests.Extensions
{
    public static class ListExtension
    {
        private static readonly Random Rng = new Random();  

        public static List<T> Shuffle<T>(this List<T> source)
        {
            var n = source.Count;  
            while (n > 1) {  
                n--;  
                var k = Rng.Next(n + 1);  
                var value = source[k];  
                source[k] = source[n];  
                source[n] = value;  
            }

            return source;
        }
    }
}