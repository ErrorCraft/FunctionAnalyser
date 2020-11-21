using System;

namespace CommandParser.Context
{
    public struct Range
    {
        public int Start { get; }
        public int End { get; }

        public Range(int start, int end)
        {
            Start = start;
            End = end;
        }

        public bool IsEmpty()
        {
            return Start == End;
        }

        public static Range At(int start)
        {
            return new Range(start, start);
        }

        public static Range Encompassing(Range left, Range right)
        {
            return new Range(Math.Min(left.Start, right.Start), Math.Max(left.End, right.End));
        }

        public override bool Equals(object obj)
        {
            return obj is Range range &&
                   Start == range.Start &&
                   End == range.End;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }
    }
}
