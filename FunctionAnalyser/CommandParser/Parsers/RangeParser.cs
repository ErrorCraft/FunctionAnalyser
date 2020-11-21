using CommandParser.Results;
using CommandParser.Results.Arguments;
using System;

namespace CommandParser.Parsers
{
    public class RangeParser<T> where T : struct, IComparable
    {
        public delegate bool Converter<S>(S input, out T result);
        private readonly StringReader StringReader;

        public RangeParser(StringReader stringReader)
        {
            StringReader = stringReader;
        }

        public ReadResults Read(Converter<string> function, Func<string, CommandError> invalidNumberErrorProvider, T minimum, T maximum, bool loopable, out Range<T> result)
        {
            result = default;
            if (!StringReader.CanRead())
            {
                return new ReadResults(false, CommandError.ExpectedValueOrRange().WithContext(StringReader));
            }

            int start = StringReader.Cursor;
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
                return new ReadResults(false, CommandError.ExpectedValueOrRange().WithContext(StringReader));
            }

            if (left == null) left = minimum;
            if (right == null) right = maximum;

            if (!loopable && ((T)left).CompareTo((T)right) > 0)
            {
                StringReader.Cursor = start;
                return new ReadResults(false, CommandError.RangeMinBiggerThanMax().WithContext(StringReader));
            }

            result = new Range<T>((T)left, (T)right);
            return new ReadResults(true, null);
        }

        public ReadResults ReadNumber(Converter<string> function, Func<string, CommandError> invalidNumberErrorProvider, out T? result)
        {
            result = default;
            int start = StringReader.Cursor;

            while (StringReader.CanRead() && IsAllowedInput(StringReader))
            {
                StringReader.Skip();
            }
            string number = StringReader.Command[start..StringReader.Cursor];

            if (string.IsNullOrEmpty(number))
            {
                result = null;
                return new ReadResults(true, null);
            } else if (function.Invoke(number, out T actualResult))
            {
                result = actualResult;
                return new ReadResults(true, null);
            }

            StringReader.Cursor = start;
            return new ReadResults(false, invalidNumberErrorProvider.Invoke(number).WithContext(StringReader));
        }

        private static bool IsAllowedInput(StringReader reader)
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
