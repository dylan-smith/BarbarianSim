using System;
using System.Collections.Generic;
using System.Linq;

namespace BarbarianSim
{
    public static class ExtensionMethods
    {
        public static double Floor(this double value) => Math.Floor(value + 0.00000001);

        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> range) => range.ForEach(x => list.Add(x));

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }

        public static void RemoveAll<T>(this ICollection<T> list, Func<T, bool> predicate)
        {
            list.Where(predicate).ToList().ForEach(x => list.Remove(x));
        }

        public static string ShaveLeft(this string a, int characters) => a.Substring(characters);

        public static string ShaveLeft(this string a, string shave)
        {
            var result = a;

            while (result.StartsWith(shave))
            {
                result = result.Substring(shave.Length);
            }

            return result;
        }

        public static string ShaveRight(this string a, int characters) => a.Substring(0, a.Length - characters);

        public static string ShaveRight(this string a, string shave)
        {
            var result = a;

            while (result.EndsWith(shave))
            {
                result = result.Substring(0, result.Length - shave.Length);
            }

            return result;
        }

        public static string Shave(this string a, int characters) => a.Substring(characters, a.Length - (characters * 2));

        public static string Shave(this string a, string shave)
        {
            var result = a;

            while (result.StartsWith(shave))
            {
                result = result.Substring(shave.Length);
            }

            while (result.EndsWith(shave))
            {
                result = result.Substring(0, result.Length - shave.Length);
            }

            return result;
        }
    }
}
