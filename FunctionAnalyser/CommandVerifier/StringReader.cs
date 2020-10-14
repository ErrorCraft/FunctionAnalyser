using CommandVerifier.Commands;
using CommandVerifier.Types;
using System;

namespace CommandVerifier
{
    public class StringReader
    {
        public readonly string Command;
        public int Cursor { get; private set; }
        public CommandData Data { get; set; }
        public CommandInformation Information { get; set; }

        public StringReader(string command)
        {
            Command = command;
            Data = new CommandData();
            Information = new CommandInformation();
        }

        public char Read()
        {
            return Command[Cursor++];
        }

        public string Read(int length)
        {
            string s = Command.Substring(Cursor, length);
            Cursor += length;
            return s;
        }

        public string ReadRemaining()
        {
            string s = Command.Substring(Cursor);
            SetCursor(Command.Length);
            return s;
        }

        public bool TryReadString(bool mayThrow, out string result)
        {
            result = "";
            if (!CanRead()) return true;
            char next = Peek();
            if (IsQuotedStringStart(next))
            {
                Skip();
                return TryReadStringUntil(next, mayThrow, out result);
            }
            return TryReadUnquotedString(out result);
        }

        public bool TryReadStringUntil(char terminator, bool mayThrow, out string result)
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
                        result += c;
                        escaped = false;
                    }
                    else
                    {
                        if (mayThrow) CommandError.InvalidEscapeSequence(c).AddWithContext(this);
                        return false;
                    }
                }
                else if (c == '\\') escaped = true;
                else if (c == terminator) return true;
                else result += c;
            }

            if (mayThrow) CommandError.ExpectedEndOfQuote().AddWithContext(this);
            return false;
        }

        public bool TryReadQuotedString(bool mayThrow, out string result)
        {
            result = "";
            if (!CanRead())
            {
                if (mayThrow) CommandError.ExpectedStartOfQuote().AddWithContext(this);
                return false;
            }
            char next = Peek();
            if (!IsQuotedStringStart(next))
            {
                if (mayThrow) CommandError.ExpectedStartOfQuote().AddWithContext(this);
                return false;
            }
            Skip();
            return TryReadStringUntil(next, mayThrow, out result);
        }

        public bool IsUnquotedStringPart(char c)
        {
            return c >= '0' && c <= '9'
                || c >= 'A' && c <= 'Z'
                || c >= 'a' && c <= 'z'
                || c == '_' || c == '-'
                || c == '.' || c == '+';
        }

        public bool TryReadUnquotedString(out string result)
        {
            int start = Cursor;

            while (CanRead() && IsUnquotedStringPart(Peek())) Skip();
            result = Command[start..Cursor];
            return true;
        }

        public bool TryReadBoolean(bool mayThrow, out bool result)
        {
            result = false;
            int start = Cursor;
            if (TryReadString(mayThrow, out string value))
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (mayThrow) CommandError.ExpectedBoolean().AddWithContext(this);
                    return false;
                }
                if ("true".Equals(value))
                {
                    result = true;
                    return true;
                } else if ("false".Equals(value))
                {
                    result = false;
                    return true;
                }
                Cursor = start;
                if (mayThrow) CommandError.InvalidBoolean(value).AddWithContext(this);
                return false;
            }
            if (mayThrow) CommandError.ExpectedBoolean().AddWithContext(this);
            return false;
        }

        private string ReadNumber()
        {
            int start = Cursor;
            while (CanRead() && IsNumberPart(Peek())) Skip();
            return Command[start..Cursor];
        }

        public bool TryReadInt(bool mayThrow, out int result)
        {
            result = 0;
            int start = Cursor;
            string number = ReadNumber();

            if (string.IsNullOrEmpty(number))
            {
                if (mayThrow) CommandError.ExpectedInteger().AddWithContext(this);
                return false;
            }
            if (int.TryParse(number, SubcommandHelper.MinecraftNumberStyles, SubcommandHelper.MinecraftNumberFormatInfo, out result))
                return true;

            Cursor = start;
            if (mayThrow) CommandError.InvalidInteger(number).AddWithContext(this);
            return false;
        }

        public bool TryReadFloat(bool mayThrow, out float result)
        {
            result = 0.0f;
            int start = Cursor;
            string number = ReadNumber();

            if (string.IsNullOrEmpty(number))
            {
                if (mayThrow) CommandError.ExpectedFloat().AddWithContext(this);
                return false;
            }
            if (float.TryParse(number, SubcommandHelper.MinecraftNumberStyles, SubcommandHelper.MinecraftNumberFormatInfo, out result))
                return true;

            Cursor = start;
            if (mayThrow) CommandError.InvalidFloat(number).AddWithContext(this);
            return false;
        }

        public bool TryReadDouble(bool mayThrow, out double result)
        {
            result = 0.0d;
            int start = Cursor;
            string number = ReadNumber();

            if (string.IsNullOrEmpty(number))
            {
                if (mayThrow) CommandError.ExpectedDouble().AddWithContext(this);
                return false;
            }
            if (double.TryParse(number, SubcommandHelper.MinecraftNumberStyles, SubcommandHelper.MinecraftNumberFormatInfo, out result))
                return true;

            Cursor = start;
            if (mayThrow) CommandError.InvalidDouble(number).AddWithContext(this);
            return false;
        }

        public bool TryReadUuid(bool mayThrow, out Guid result)
        {
            result = new Guid();
            int start = Cursor;
            while (CanRead() && IsUuidPart(Peek())) Skip();
            string uuid = Command[start..Cursor];
            if (Uuid.TryParse(uuid))
            {
                if (IsEndOfArgument()) return true;
                if (mayThrow) CommandError.ExpectedArgumentSeparator().AddWithContext(this);
                return false;
            }
            if (mayThrow) CommandError.InvalidUuid().AddWithContext(this);
            return false;
        }

        public bool IsUuidPart(char c)
        {
            return c >= 'a' && c <= 'f' || c >= 'A' && c <= 'F' || c >= '0' && c <= '9' || c == '-';
        }

        public bool IsQuotedStringStart(char c)
        {
            return c == '\'' || c == '"';
        }

        public bool IsEndOfArgument()
        {
            return !CanRead() || IsWhitespace(Peek());
        }

        public bool CanRead()
        {
            return CanRead(1);
        }

        public bool CanRead(int length)
        {
            return Cursor + length <= Command.Length;
        }

        public char Peek()
        {
            return Command[Cursor];
        }

        public char Peek(int offset)
        {
            return Command[Cursor + offset];
        }

        public bool IsNumberPart(char c)
        {
            return c >= '0' && c <= '9' || c == '.' || c == '-';
        }

        public bool IsWhitespace(char c)
        {
            return c == ' ';
        }

        public void SetCursor(int cursor)
        {
            Cursor = cursor;
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
            while (CanRead() && IsWhitespace(Peek())) Skip();
        }

        public bool TryReadNamespacedId(bool mayThrow, bool disableTags, out NamespacedId result)
        {
            result = null;
            if (!CanRead())
            {
                if (mayThrow) CommandError.IncorrectArgument().AddWithContext(this);
                return false;
            }
            bool isTag = false;
            if (!disableTags && CanRead() && Peek() == '#')
            {
                Skip();
                isTag = true;
            }

            int start = Cursor;
            while (CanRead() && IsNamespacedIdPart(Peek())) Skip();
            string s = Command[start..Cursor];

            if (!NamespacedId.TryParse(s, isTag, out result))
            {
                SetCursor(start);
                if (mayThrow) CommandError.InvalidNamespacedId().AddWithContext(this);
                return false;
            }
            return true;
        }

        private bool IsNamespacedIdPart(char c)
        {
            return c >= 'a' && c <= 'z' ||
                c >= '0' && c <= '9' ||
                c == '_' || c == '-' || c == '.' || c == '/' || c == ':';
        }

        public bool Expect(char c, bool mayThrow)
        {
            if (!CanRead() || Peek() != c)
            {
                if (mayThrow) CommandError.ExpectedCharacter(c).AddWithContext(this);
                return false;
            }
            Skip();
            return true;
        }
    }
}
