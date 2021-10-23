using CommandParser.Results;
using CommandParser.Results.Arguments;
using System;
using System.Globalization;

namespace CommandParser.Parsers;

public class RangeParser<T> where T : struct, INumber<T>, IMinMaxValue<T> {
    private readonly T Minimum;
    private readonly T Maximum;
    private readonly bool Loopable;

    public RangeParser() : this(false, T.MinValue, T.MaxValue) { }

    public RangeParser(bool loopable) : this(loopable, T.MinValue, T.MaxValue) { }

    public RangeParser(bool loopable, T minimum, T maximum) {
        Loopable = loopable;
        Minimum = minimum;
        Maximum = maximum;
    }

    public ReadResults Read(IStringReader reader, Func<string, CommandError> invalidNumberErrorProvider, out Range<T> result) {
        result = default;
        if (!reader.CanRead()) {
            return ReadResults.Failure(CommandError.ExpectedValueOrRange().WithContext(reader));
        }

        int start = reader.GetCursor();
        ReadResults readResults = ReadNumber(reader, invalidNumberErrorProvider, out T? left);
        if (!readResults.Successful) {
            return readResults;
        }

        T? right;
        if (reader.CanRead(2) && reader.Peek() == '.' && reader.Peek(1) == '.') {
            reader.Skip(2);
            readResults = ReadNumber(reader, invalidNumberErrorProvider, out right);
            if (!readResults.Successful) {
                return readResults;
            }
        } else {
            right = left;
        }

        if (left == null && right == null) {
            return ReadResults.Failure(CommandError.ExpectedValueOrRange().WithContext(reader));
        }

        if (left == null) {
            left = Minimum;
        }
        if (right == null) {
            right = Maximum;
        }

        if (!Loopable && left > right) {
            reader.SetCursor(start);
            return ReadResults.Failure(CommandError.RangeMinBiggerThanMax().WithContext(reader));
        }

        result = new Range<T>(left!.Value, right!.Value);
        return ReadResults.Success();
    }

    private ReadResults ReadNumber(IStringReader reader, Func<string, CommandError> invalidNumberErrorProvider, out T? result) {
        result = default;
        int start = reader.GetCursor();

        while (reader.CanRead() && IsAllowedInput(reader)) {
            reader.Skip();
        }
        string numberString = reader.GetString()[start..reader.GetCursor()];

        if (string.IsNullOrEmpty(numberString)) {
            result = null;
            return ReadResults.Success();
        }
        if (T.TryParse(numberString, CultureInfo.InvariantCulture, out T number)) {
            result = number;
            return ReadResults.Success();
        }

        reader.SetCursor(start);
        return ReadResults.Failure(invalidNumberErrorProvider(numberString).WithContext(reader));
    }

    private static bool IsAllowedInput(IStringReader reader) {
        char c = reader.Peek();
        if (c >= '0' && c <= '9' || c == '-') {
            return true;
        }
        if (c == '.') {
            return !reader.CanRead(2) || reader.Peek(1) != '.';
        }
        return false;
    }
}
