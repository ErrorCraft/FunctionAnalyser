using static CommandParser.Providers.NumberProvider;
using CommandParser.Results;

namespace CommandParser
{
    public class StringReader
    {
        public string Command { get; }
        public int Cursor { get; set; }

        public StringReader(string command)
            : this(command, 0) { }

        private StringReader(string command, int cursor)
        {
            Command = command;
            Cursor = cursor;
        }

        public StringReader Copy()
        {
            return new StringReader(Command, Cursor);
        }

        public bool CanRead()
        {
            return CanRead(1);
        }

        public bool CanRead(int length)
        {
            return Cursor + length <= Command.Length;
        }

        public char Read()
        {
            return Command[Cursor++];
        }

        public string Read(int length)
        {
            int start = Cursor;
            Skip(length);
            return Command[start..Cursor];
        }

        public string GetRemaining()
        {
            return Command.Substring(Cursor);
        }

        public int GetLength()
        {
            return Command.Length;
        }

        public void Skip()
        {
            Cursor++;
        }

        public void Skip(int length)
        {
            Cursor += length;
        }

        public void SkipWhitespace()
        {
            while (CanRead() && IsWhitespace(Peek()))
            {
                Skip();
            }
        }

        public char Peek()
        {
            return Command[Cursor];
        }

        public char Peek(int offset)
        {
            return Command[Cursor + offset];
        }

        public bool AtEndOfArgument()
        {
            return !CanRead() || IsWhitespace(Peek());
        }

        private static bool IsWhitespace(char c)
        {
            return c == ' ';
        }

        private static bool IsNumberPart(char c)
        {
            return c >= '0' && c <= '9' || c == '.' || c == '-';
        }

        public ReadResults ReadInteger(out int value)
        {
            value = default;
            int start = Cursor;
            while (CanRead() && IsNumberPart(Peek()))
            {
                Skip();
            }
            string number = Command[start..Cursor];
            if (string.IsNullOrEmpty(number))
            {
                Cursor = start;
                return new ReadResults(false, CommandError.ExpectedInteger().WithContext(this));
            }

            if (!int.TryParse(number, NumberStylesInteger, NumberFormatInfo, out value))
            {
                Cursor = start;
                return new ReadResults(false, CommandError.InvalidInteger(number).WithContext(this));
            }

            return new ReadResults(true, null);
        }

        public ReadResults ReadLong(out long value)
        {
            value = default;
            int start = Cursor;
            while (CanRead() && IsNumberPart(Peek()))
            {
                Skip();
            }
            string number = Command[start..Cursor];
            if (string.IsNullOrEmpty(number))
            {
                Cursor = start;
                return new ReadResults(false, CommandError.ExpectedLong().WithContext(this));
            }

            if (!long.TryParse(number, NumberStylesInteger, NumberFormatInfo, out value))
            {
                Cursor = start;
                return new ReadResults(false, CommandError.InvalidLong(number).WithContext(this));
            }

            return new ReadResults(true, null);
        }

        public ReadResults ReadFloat(out float value)
        {
            value = default;
            int start = Cursor;
            while (CanRead() && IsNumberPart(Peek()))
            {
                Skip();
            }
            string number = Command[start..Cursor];
            if (string.IsNullOrEmpty(number))
            {
                Cursor = start;
                return new ReadResults(false, CommandError.ExpectedFloat().WithContext(this));
            }

            if (!float.TryParse(number, NumberStylesFloating, NumberFormatInfo, out value))
            {
                Cursor = start;
                return new ReadResults(false, CommandError.InvalidFloat(number).WithContext(this));
            }

            return new ReadResults(true, null);
        }

        public ReadResults ReadDouble(out double value)
        {
            value = default;
            int start = Cursor;
            while (CanRead() && IsNumberPart(Peek()))
            {
                Skip();
            }
            string number = Command[start..Cursor];
            if (string.IsNullOrEmpty(number))
            {
                Cursor = start;
                return new ReadResults(false, CommandError.ExpectedDouble().WithContext(this));
            }

            if (!double.TryParse(number, NumberStylesFloating, NumberFormatInfo, out value))
            {
                Cursor = start;
                return new ReadResults(false, CommandError.InvalidDouble(number).WithContext(this));
            }

            return new ReadResults(true, null);
        }

        public ReadResults ReadBoolean(out bool result)
        {
            result = default;
            int start = Cursor;

            ReadResults readResults = ReadString(out string s);
            if (!readResults.Successful)
            {
                return readResults;
            }
            if (string.IsNullOrEmpty(s))
            {
                return new ReadResults(false, CommandError.ExpectedBoolean().WithContext(this));
            }

            if ("true".Equals(s))
            {
                result = true;
                return new ReadResults(true, null);
            } else if ("false".Equals(s))
            {
                result = false;
                return new ReadResults(true, null);
            } else
            {
                Cursor = start;
                return new ReadResults(false, CommandError.InvalidBoolean(s).WithContext(this));
            }
        }

        public ReadResults ReadString(out string result)
        {
            if (!CanRead())
            {
                result = "";
                return new ReadResults(true, null);
            }

            char next = Peek();
            if (IsQuotedStringStart(next))
            {
                Skip();
                return ReadStringUntil(next, out result);
            }
            return ReadUnquotedString(out result);
        }

        public ReadResults ReadUnquotedString(out string result)
        {
            int start = Cursor;
            while (CanRead() && IsUnquotedStringPart(Peek()))
            {
                Skip();
            }
            result = Command[start..Cursor];
            return new ReadResults(true, null);
        }

        public ReadResults ReadQuotedString(out string result)
        {
            result = default;
            if (!CanRead())
            {
                result = "";
                return new ReadResults(true, null);
            }

            char next = Peek();
            if (!IsQuotedStringStart(next))
            {
                return new ReadResults(false, CommandError.ExpectedStartOfQuote().WithContext(this));
            }

            Skip();
            return ReadStringUntil(next, out result);
        }

        public ReadResults ReadStringUntil(char terminator, out string result)
        {
            result = "";
            bool escaped = false;

            while (CanRead())
            {
                char c = Read();
                if (escaped)
                {
                    if (c == terminator || c == '\\')
                    {
                        escaped = false;
                        result += c;
                    }
                    else
                    {
                        Cursor--;
                        return new ReadResults(false, CommandError.InvalidEscapeSequence(c).WithContext(this));
                    }
                }
                else if (c == '\\')
                {
                    escaped = true;
                }
                else if (c == terminator)
                {
                    return new ReadResults(true, null);
                }
                else
                {
                    result += c;
                }
            }

            return new ReadResults(false, CommandError.ExpectedEndOfQuote().WithContext(this));
        }

        public static bool IsQuotedStringStart(char c)
        {
            return c == '\'' || c == '"';
        }

        private static bool IsUnquotedStringPart(char c)
        {
            return c >= '0' && c <= '9' ||
                c >= 'A' && c <= 'Z' ||
                c >= 'a' && c <= 'z' ||
                c == '_' || c == '-' ||
                c == '.' || c == '+';
        }

        public ReadResults Expect(char c)
        {
            if (!CanRead() || Peek() != c)
            {
                return new ReadResults(false, CommandError.ExpectedCharacter(c).WithContext(this));
            }
            Skip();
            return new ReadResults(true, null);
        }
    }
}
