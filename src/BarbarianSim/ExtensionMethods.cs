namespace BarbarianSim
{
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
    }
}
