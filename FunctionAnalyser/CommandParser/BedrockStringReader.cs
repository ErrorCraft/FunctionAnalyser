using static CommandParser.Providers.NumberProvider;
using CommandParser.Results;

namespace CommandParser
{
    public class BedrockStringReader : IStringReader
    {
        private readonly string Command;
        private int Cursor;

        public BedrockStringReader(string command) : this(command, 0) { }

        private BedrockStringReader(string command, int cursor)
        {
            Command = command;
            Cursor = cursor;
        }

        public IStringReader Copy()
        {
            return new BedrockStringReader(Command, Cursor);
        }

        public string GetString()
        {
            return Command;
        }

        public int GetRemainingLength()
        {
            return Command.Length - Cursor;
        }

        public int GetLength()
        {
            return Command.Length;
        }

        public int GetCursor()
        {
            return Cursor;
        }

        public void SetCursor(int cursor)
        {
            Cursor = cursor;
        }

        public string GetRemaining()
        {
            return Command[Cursor..];
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

        public bool IsQuotedStringStart(char c)
        {
            return c == '"' || c == '“' || c == '”';
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
                return ReadResults.Failure(CommandError.ExpectedInteger().WithContext(this));
            }

            if (!int.TryParse(number, NumberStylesInteger, NumberFormatInfo, out value))
            {
                Cursor = start;
                return ReadResults.Failure(CommandError.InvalidInteger(number).WithContext(this));
            }

            return ReadResults.Success();
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
                return ReadResults.Failure(CommandError.ExpectedLong().WithContext(this));
            }

            if (!long.TryParse(number, NumberStylesInteger, NumberFormatInfo, out value))
            {
                Cursor = start;
                return ReadResults.Failure(CommandError.InvalidLong(number).WithContext(this));
            }

            return ReadResults.Success();
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
                return ReadResults.Failure(CommandError.ExpectedFloat().WithContext(this));
            }

            if (!float.TryParse(number, NumberStylesFloating, NumberFormatInfo, out value))
            {
                Cursor = start;
                return ReadResults.Failure(CommandError.InvalidFloat(number).WithContext(this));
            }

            return ReadResults.Success();
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
                return ReadResults.Failure(CommandError.ExpectedDouble().WithContext(this));
            }

            if (!double.TryParse(number, NumberStylesFloating, NumberFormatInfo, out value))
            {
                Cursor = start;
                return ReadResults.Failure(CommandError.InvalidDouble(number).WithContext(this));
            }

            return ReadResults.Success();
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
                return ReadResults.Failure(CommandError.ExpectedBoolean().WithContext(this));
            }

            if ("true".Equals(s))
            {
                result = true;
                return ReadResults.Success();
            }
            else if ("false".Equals(s))
            {
                result = false;
                return ReadResults.Success();
            }
            else
            {
                Cursor = start;
                return ReadResults.Failure(CommandError.InvalidBoolean(s).WithContext(this));
            }
        }

        public ReadResults ReadString(out string result)
        {
            if (!CanRead())
            {
                result = "";
                return ReadResults.Success();
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
            if (CanRead() && IsDigit(Peek()))
            {
                result = default;
                return ReadResults.Failure(CommandError.InvalidUnquotedStringStart().WithContext(this));
            }
            while (CanRead() && IsUnquotedStringPart(Peek()))
            {
                Skip();
            }
            result = Command[start..Cursor];
            return ReadResults.Success();
        }

        public ReadResults ReadQuotedString(out string result)
        {
            result = default;
            if (!CanRead())
            {
                result = "";
                return ReadResults.Success();
            }

            char next = Peek();
            if (!IsQuotedStringStart(next))
            {
                return ReadResults.Failure(CommandError.ExpectedStartOfQuote().WithContext(this));
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
                        return ReadResults.Failure(CommandError.InvalidEscapeSequence(c).WithContext(this));
                    }
                }
                else if (c == '\\')
                {
                    escaped = true;
                }
                else if (c == terminator)
                {
                    return ReadResults.Success();
                }
                else
                {
                    result += c;
                }
            }

            return ReadResults.Failure(CommandError.ExpectedEndOfQuote().WithContext(this));
        }

        public ReadResults Expect(char c)
        {
            if (!CanRead() || Peek() != c)
            {
                return ReadResults.Failure(CommandError.ExpectedCharacter(c).WithContext(this));
            }
            Skip();
            return ReadResults.Success();
        }

        private static bool IsWhitespace(char c)
        {
            return c == ' ';
        }

        private static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private static bool IsNumberPart(char c)
        {
            return IsDigit(c) || c == '.' || c == '-';
        }

        private static bool IsUnquotedStringPart(char c)
        {
            return c >= '0' && c <= '9' ||
                c >= 'A' && c <= 'Z' ||
                c >= 'a' && c <= 'z' ||
                c == '_' || c == '-' ||
                c == '.' || c == '§';
        }
    }
}
