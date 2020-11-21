using System;

namespace CommandParser.Results.Arguments
{
    public class Range<T> where T : struct, IComparable
    {
        public T Minimum { get; }
        public T Maximum { get; }

        public Range(T minimum, T maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public override string ToString()
        {
            return $"{Minimum}..{Maximum}";
        }
    }
}
