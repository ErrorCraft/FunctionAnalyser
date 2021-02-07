using CommandParser.Results;
using CommandParser.Results.Arguments;
using System;

namespace CommandParser.Parsers
{
    public class RangeParser<T> where T : struct, IComparable
    {
        public delegate bool Converter<S>(S input, out T result);
        private readonly IStringReader StringReader;

        public RangeParser(IStringReader stringReader)
        {
            StringReader = stringReader;
        }

        public ReadResults Read(Converter<string> function, Func<string, CommandError> invalidNumberErrorProvider, T minimum, T maximum, bool loopable, out Range<T> result)
        {
            result = default;
            if (!StringReader.CanRead())
            {
                return ReadResults.Failure(CommandError.ExpectedValueOrRange().WithContext(StringReader));
            }

            int start = StringReader.GetCursor();
            T? right;
            ReadResults readResults = ReadNumber(function, invalidNumberErrorProvider, out T? left);
            if (!readResults.Successful) return readResults;
            if (StringReader.CanRead(2) && StringReader.Peek() == '.' && StringReader.Peek(1) == '.')
            {
                StringReader.Skip(2);
                readResults = ReadNumber(function, invalidNumberErrorProvider, out right);
                if (!readResults.Successful) return readResults;
            } else
            {
                right = left;
            }

            if (left == null && right == null)
            {
                return ReadResults.Failure(CommandError.ExpectedValueOrRange().WithContext(StringReader));
            }

            if (left == null) left = minimum;
            if (right == null) right = maximum;

            if (!loopable && ((T)left).CompareTo((T)right) > 0)
            {
                StringReader.SetCursor(start);
                return ReadResults.Failure(CommandError.RangeMinBiggerThanMax().WithContext(StringReader));
            }

            result = new Range<T>((T)left, (T)right);
            return ReadResults.Success();
        }

        public ReadResults ReadNumber(Converter<string> function, Func<string, CommandError> invalidNumberErrorProvider, out T? result)
        {
            result = default;
            int start = StringReader.GetCursor();

            while (StringReader.CanRead() && IsAllowedInput(StringReader))
            {
                StringReader.Skip();
            }
            string number = StringReader.GetString()[start..StringReader.GetCursor()];

            if (string.IsNullOrEmpty(number))
            {
                result = null;
                return ReadResults.Success();
            } else if (function.Invoke(number, out T actualResult))
            {
                result = actualResult;
                return ReadResults.Success();
            }

            StringReader.SetCursor(start);
            return ReadResults.Failure(invalidNumberErrorProvider.Invoke(number).WithContext(StringReader));
        }

        private static bool IsAllowedInput(IStringReader reader)
        {
            char c = reader.Peek();
            if (c >= '0' && c <= '9' || c == '-') return true;
            else if (c == '.')
            {
                return !reader.CanRead(2) || reader.Peek(1) != '.';
            }
            else return false;
        }
    }
}
