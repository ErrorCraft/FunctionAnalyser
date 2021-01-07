using System.Collections.Generic;

namespace FunctionAnalyser
{
    public static class Extensions
    {
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> collection)
        {
            foreach (KeyValuePair<TKey, TValue> pair in collection)
            {
                source[pair.Key] = pair.Value;
            }
        }
    }
}
