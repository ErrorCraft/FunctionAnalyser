using System;

namespace CommandParser.Results.Arguments;

public class Range<T> : IEquatable<Range<T>> where T : struct, INumber<T> {
    public T Minimum { get; }
    public T Maximum { get; }

    public Range(T minimum, T maximum) {
        Minimum = minimum;
        Maximum = maximum;
    }

    public override string ToString() {
        return $"{Minimum}..{Maximum}";
    }

    public override bool Equals(object obj) {
        return Equals(obj as Range<T>);
    }

    public bool Equals(Range<T> other) {
        return other != null && other.Minimum == Minimum && other.Maximum == Maximum;
    }

    public override int GetHashCode() {
        return HashCode.Combine(Minimum, Maximum);
    }
}
