using CommandParser.Results;
using CommandParser.Results.Arguments;
using System.Globalization;

namespace CommandParser.Parsers
{
    public class UuidParser
    {
        private readonly string UuidToParse;
        private int Cursor;

        public UuidParser(string uuidToParse)
        {
            UuidToParse = uuidToParse;
            Cursor = 0;
        }

        public static ReadResults FromReader(IStringReader reader, out Uuid result)
        {
            int start = reader.GetCursor();
            while (reader.CanRead() && IsUuidPart(reader.Peek()))
            {
                reader.Skip();
            }
            string uuid = reader.GetString()[start..reader.GetCursor()];
            UuidParser uuidParser = new UuidParser(uuid);
            if (!uuidParser.Parse(out result))
            {
                return ReadResults.Failure(CommandError.InvalidUuid());
            }
            return ReadResults.Success();
        }

        public bool Parse(out Uuid result)
        {
            result = default;
            short[] shorts = new short[8];

            short[] temp;
            if ((temp = GetShorts(2)) == null) return false;
            else
            {
                shorts[0] = temp[0];
                shorts[1] = temp[1];
                if (!CanRead() || UuidToParse[Cursor] != Uuid.VALUE_SEPARATOR) return false;
                Skip();
            }

            for (int i = 0; i < 3; i++)
            {
                if ((temp = GetShorts(1)) == null) return false;
                else
                {
                    shorts[2 + i] = temp[0];
                    if (!CanRead() || UuidToParse[Cursor] != Uuid.VALUE_SEPARATOR) return false;
                    Skip();
                }
            }

            if ((temp = GetShorts(3)) == null) return false;
            else
            {
                shorts[5] = temp[0];
                shorts[6] = temp[1];
                shorts[7] = temp[2];
                if (CanRead()) return false;
            }

            result = new Uuid(shorts[0], shorts[1], shorts[2], shorts[3], shorts[4], shorts[5], shorts[6], shorts[7]);
            return true;
        }

        private short[] GetShorts(int length)
        {
            short[] values = new short[length];
            int actualLength = length * 4;

            string uuidPart = GetUuidPart();
            if (uuidPart.Length == 0 || uuidPart.Length > actualLength)
            {
                return null;
            }

            // Fill with leading zeros
            while (uuidPart.Length < actualLength)
            {
                uuidPart = uuidPart.Insert(0, "0");
            }

            for (int i = length - 1; i >= 0; i--)
            {
                if (!short.TryParse(uuidPart.Substring(4 * i, 4), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out values[i])) return null;
            }
            return values;
        }

        private bool CanRead()
        {
            return Cursor < UuidToParse.Length;
        }

        private void Skip()
        {
            Cursor++;
        }

        private string GetUuidPart()
        {
            int start = Cursor;
            while (CanRead() && IsDigit(UuidToParse[Cursor]))
            {
                Skip();
            }
            return UuidToParse[start..Cursor];
        }

        private static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9' ||
                c >= 'a' && c <= 'f' ||
                c >= 'A' && c <= 'F';
        }

        private static bool IsUuidPart(char c)
        {
            return IsDigit(c) || c == '-';
        }
    }
}
