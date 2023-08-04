namespace BarbarianSim;

public static class ExtensionMethods
{
    public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> range) => range.ForEach(x => list.Add(x));

    public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
    {
        if (list == null)
        {
            throw new ArgumentNullException(nameof(list));
        }

        if (action == null)
        {
            throw new ArgumentNullException(nameof(action));
        }

        foreach (var item in list)
        {
            action(item);
        }
    }

    public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dict)
    {
        while (dict.Any())
        {
            dict?.Remove(dict.First().Key);
        }
    }

    public static void RemoveAll<T>(this IList<T> list, Func<T, bool> filter)
    {
        while (list.Any(filter))
        {
            list?.Remove(list.First(filter));
        }
    }

    public static void Times(this int count, Action action)
    {
        for (var i = 0; i < count; i++)
        {
            action();
        }
    }

    public static void Times(this int count, Action<int> action)
    {
        for (var i = 0; i < count; i++)
        {
            action(i);
        }
    }

    public static double Multiply(this IEnumerable<double> list)
    {
        return list.Aggregate(1.0, (acc, x) => acc * x);
    }

    public static double Multiply<T>(this IEnumerable<T> list, Func<T, double> projection)
    {
        return list.Aggregate(1.0, (acc, x) => acc * projection(x));
    }
}
