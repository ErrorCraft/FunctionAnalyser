using CommandParser.Parsers.NbtParser.NbtArguments;
using static CommandParser.Parsers.NbtParser.NbtNumberProvider;
using CommandParser.Results;

namespace CommandParser.Parsers.NbtParser
{
    public class NbtReader
    {
        public static ReadResults ReadValue(IStringReader reader, out INbtArgument result)
        {
            reader.SkipWhitespace();
            switch (reader.Peek())
            {
                case '{':
                    ReadResults compoundReadResults = ReadCompound(reader, out NbtCompound compound);
                    result = compound;
                    return compoundReadResults;
                case '[':
                    ReadResults arrayReadResults = ReadArray(reader, out INbtCollection array);
                    result = array;
                    return arrayReadResults;
                default:
                    return ReadItem(reader, out result);
            }
        }

        public static ReadResults ReadCompound(IStringReader reader, out NbtCompound result)
        {
            result = new NbtCompound();

            reader.SkipWhitespace();
            ReadResults readResults = reader.Expect('{');
            if (!readResults.Successful) return readResults;

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != '}')
            {
                reader.SkipWhitespace();
                readResults = reader.ReadString(out string key);
                if (!readResults.Successful) return readResults;

                if (string.IsNullOrEmpty(key))
                {
                    return new ReadResults(false, CommandError.ExpectedKey().WithContext(reader));
                }

                reader.SkipWhitespace();
                readResults = reader.Expect(':');
                if (!readResults.Successful) return readResults;

                reader.SkipWhitespace();
                if (!reader.CanRead())
                {
                    return new ReadResults(false, CommandError.ExpectedValue().WithContext(reader));
                } else
                {
                    readResults = ReadValue(reader, out INbtArgument valueResult);
                    if (!readResults.Successful) return readResults;
                    result.Add(key, valueResult);
                }

                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    reader.SkipWhitespace();
                    continue;
                }
                break;
            }
            return reader.Expect('}');
        }

        public static ReadResults ReadArray(IStringReader reader, out INbtCollection result)
        {
            result = default;

            reader.SkipWhitespace();
            ReadResults readResults = reader.Expect('[');
            if (!readResults.Successful) return readResults;

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != ']')
            {
                reader.SkipWhitespace();
                int start = reader.GetCursor();
                if (result == null)
                {
                    if (reader.CanRead(2) && reader.Peek(1) == ';') // Array type
                    {
                        char type = reader.Read();
                        reader.Skip();

                        switch (type)
                        {
                            case 'B':
                                result = new NbtByteArray();
                                continue;
                            case 'I':
                                result = new NbtIntegerArray();
                                continue;
                            case 'L':
                                result = new NbtLongArray();
                                continue;
                            default:
                                reader.SetCursor(start);
                                return new ReadResults(false, CommandError.InvalidArrayType(type).WithContext(reader));
                        }
                    } else // List type
                    {
                        readResults = ReadValue(reader, out INbtArgument valueResult);
                        if (!readResults.Successful) return readResults;
                        result = new NbtList<INbtArgument>(valueResult);
                    }
                } else
                {
                    readResults = ReadValue(reader, out INbtArgument valueResult);
                    if (!readResults.Successful) return readResults;
                    if (!result.TryAdd(valueResult))
                    {
                        reader.SetCursor(start);
                        return new ReadResults(false, CommandError.NbtCannotInsert(valueResult, result).WithContext(reader));
                    }
                }

                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    continue;
                }
                break;
            }
            if (result == null) result = new NbtList<INbtArgument>();
            return reader.Expect(']');
        }

        public static ReadResults ReadItem(IStringReader reader, out INbtArgument result)
        {
            result = default;
            ReadResults readResults;

            char c = reader.Peek();
            if (reader.IsQuotedStringStart(c)) // Quoted string
            {
                reader.Skip();
                readResults = reader.ReadStringUntil(c, out string quotedStringResult);
                if (readResults.Successful) result = new NbtString(quotedStringResult);
                return readResults;
            }

            // Unquoted string
            readResults = reader.ReadUnquotedString(out string stringResult);
            if (!readResults.Successful) return readResults;

            // Empty string
            if (string.IsNullOrEmpty(stringResult))
            {
                return new ReadResults(false, CommandError.ExpectedValue().WithContext(reader));
            }

            // Number or unquoted string
            if (!TryParseNumber(stringResult, out result)) result = new NbtString(stringResult);
            return new ReadResults(true, null);
        }

        private static bool TryParseNumber(string input, out INbtArgument result)
        {
            result = default;
            if (string.IsNullOrEmpty(input)) return false;

            char lastCharacter = input[^1];
            string inputWithoutSuffix = input[0..^1];

            switch (lastCharacter)
            {
                case 'b':
                case 'B':
                    if (!sbyte.TryParse(inputWithoutSuffix, NumberStylesInteger, NumberFormatInfo, out sbyte byteResult)) return false;
                    result = new NbtByte(byteResult);
                    return true;
                case 's':
                case 'S':
                    if (!short.TryParse(inputWithoutSuffix, NumberStylesInteger, NumberFormatInfo, out short shortResult)) return false;
                    result = new NbtShort(shortResult);
                    return true;
                case 'l':
                case 'L':
                    if (!long.TryParse(inputWithoutSuffix, NumberStylesInteger, NumberFormatInfo, out long longResult)) return false;
                    result = new NbtLong(longResult);
                    return true;
                case 'f':
                case 'F':
                    if (!float.TryParse(inputWithoutSuffix, NumberStylesFloating, NumberFormatInfo, out float floatResult)) return false;
                    result = new NbtFloat(floatResult);
                    return true;
                case 'd':
                case 'D':
                    if (!double.TryParse(inputWithoutSuffix, NumberStylesFloating, NumberFormatInfo, out double doubleResult)) return false;
                    result = new NbtDouble(doubleResult);
                    return true;
                default:
                    if (int.TryParse(input, NumberStylesInteger, NumberFormatInfo, out int integerResult))
                    {
                        result = new NbtInteger(integerResult);
                        return true;
                    } else if (double.TryParse(input, NumberStylesFloating, NumberFormatInfo, out double doubleResultWithoutSuffix))
                    {
                        result = new NbtDouble(doubleResultWithoutSuffix);
                        return true;
                    } else if ("true".Equals(input))
                    {
                        result = new NbtByte(1);
                        return true;
                    } else if ("false".Equals(input))
                    {
                        result = new NbtByte(0);
                        return true;
                    } else
                    {
                        return false;
                    }
            }
        }
    }
}
