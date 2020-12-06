using System;
using System.Collections.Generic;

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

        public override bool Equals(object obj)
        {
            return obj is Range<T> range &&
                   EqualityComparer<T>.Default.Equals(Minimum, range.Minimum) &&
                   EqualityComparer<T>.Default.Equals(Maximum, range.Maximum);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Minimum, Maximum);
        }
    }
}
