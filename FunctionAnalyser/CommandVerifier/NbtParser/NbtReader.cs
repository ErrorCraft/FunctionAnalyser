using CommandVerifier.Commands;
using CommandVerifier.NbtParser.Types;

namespace CommandVerifier.NbtParser
{
    class NbtReader
    {
        public static bool TryParse(StringReader reader, bool mayThrow, out NbtCompound result) => TryReadCompound(reader, mayThrow, out result);

        public static bool TryReadCompound(StringReader reader, bool mayThrow, out NbtCompound result)
        {
            result = new NbtCompound();

            reader.SkipWhitespace();
            if (!reader.Expect('{', mayThrow)) return false;
            reader.SkipWhitespace();

            while (reader.CanRead() && reader.Peek() != '}')
            {
                reader.SkipWhitespace();
                if (!reader.TryReadString(mayThrow, out string key)) return false;
                if (string.IsNullOrEmpty(key))
                {
                    if (mayThrow) CommandError.ExpectedKey().AddWithContext(reader);
                    return false;
                }
                
                reader.SkipWhitespace();
                if (!reader.Expect(':', mayThrow)) return false;

                reader.SkipWhitespace();
                if (!reader.CanRead())
                {
                    if (mayThrow) CommandError.ExpectedValue().AddWithContext(reader);
                    return false;
                }

                switch (reader.Peek())
                {
                    case '{': // compound
                        if (!TryReadCompound(reader, mayThrow, out NbtCompound compound)) return false;
                        result.Add(key, compound);
                        break;
                    case '[': // array/list
                        if (!TryReadArray(reader, mayThrow, out INbtCollection array)) return false;
                        result.Add(key, array);
                        break;
                    default: // string/number
                        if (!TryReadValue(reader, mayThrow, out INbtArgument argument)) return false;
                        result.Add(key, argument);
                        break;
                }

                // Next value
                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    reader.SkipWhitespace();
                    continue;
                }
                break;
            }
            // End of compound
            if (!reader.Expect('}', mayThrow)) return false;
            return true;
        }

        public static bool TryReadArray(StringReader reader, bool may_throw, out INbtCollection result)
        {
            result = null;

            reader.SkipWhitespace();
            if (!reader.Expect('[', may_throw)) return false;
            reader.SkipWhitespace();

            while (reader.CanRead() && reader.Peek() != ']')
            {
                reader.SkipWhitespace();

                // Was not set (first value)
                if (result == null)
                {
                    int start = reader.Cursor;
                    bool is_array_type = false;
                    // Is one of ARRAY types
                    if (reader.CanRead(2))
                    {
                        string expected_type = reader.Read(2);
                        int start_array_values = reader.Cursor;
                        reader.SkipWhitespace();
                        switch (expected_type)
                        {
                            case "B;":
                                if (!TryReadValue(reader, may_throw, out INbtArgument byte_argument)) return false;
                                result = new NbtByteArray();
                                if (!result.TryAdd(byte_argument))
                                {
                                    reader.SetCursor(start_array_values);
                                    if (may_throw) CommandError.NbtCannotInsert(byte_argument, result).AddWithContext(reader);
                                    return false;
                                }
                                is_array_type = true;
                                break;
                            case "I;":
                                if (!TryReadValue(reader, may_throw, out INbtArgument integer_argument)) return false;
                                result = new NbtIntegerArray();
                                if (!result.TryAdd(integer_argument))
                                {
                                    reader.SetCursor(start_array_values);
                                    if (may_throw) CommandError.NbtCannotInsert(integer_argument, result).AddWithContext(reader);
                                    return false;
                                }
                                is_array_type = true;
                                break;
                            case "L;":
                                if (!TryReadValue(reader, may_throw, out INbtArgument long_argument)) return false;
                                result = new NbtLongArray();
                                if (!result.TryAdd(long_argument))
                                {
                                    reader.SetCursor(start_array_values);
                                    if (may_throw) CommandError.NbtCannotInsert(long_argument, result).AddWithContext(reader);
                                    return false;
                                }
                                is_array_type = true;
                                break;
                        }
                    }

                    if (!is_array_type)
                    {
                        reader.SetCursor(start);
                        // Is LIST type
                        switch (reader.Peek())
                        {
                            case '{': // compound
                                if (!TryReadCompound(reader, may_throw, out NbtCompound compound)) return false;
                                result = new NbtList(typeof(NbtCompound));
                                result.TryAdd(compound);
                                break;
                            case '[': // array/list
                                if (!TryReadArray(reader, may_throw, out INbtCollection array)) return false;
                                result = new NbtList(array.GetType());
                                result.TryAdd(array);
                                break;
                            default: // string/number
                                if (!TryReadValue(reader, may_throw, out INbtArgument argument)) return false;
                                result = new NbtList(argument.GetType());
                                result.TryAdd(argument);
                                break;
                        }
                    }
                } else
                {
                    int start = reader.Cursor;
                    switch (reader.Peek())
                    {
                        case '{': // compound
                            if (!TryReadCompound(reader, may_throw, out NbtCompound compound)) return false;
                            if (!result.TryAdd(compound))
                            {
                                reader.SetCursor(start);
                                if (may_throw) CommandError.NbtCannotInsert(compound, result).AddWithContext(reader);
                                return false;
                            }
                            break;
                        case '[': // array/list
                            if (!TryReadArray(reader, may_throw, out INbtCollection array)) return false;
                            if (!result.TryAdd(array))
                            {
                                reader.SetCursor(start);
                                if (may_throw) CommandError.NbtCannotInsert(array, result).AddWithContext(reader);
                                return false;
                            }
                            break;
                        default: // string/number
                            if (!TryReadValue(reader, may_throw, out INbtArgument argument)) return false;
                            if (!result.TryAdd(argument))
                            {
                                reader.SetCursor(start);
                                if (may_throw) CommandError.NbtCannotInsert(argument, result).AddWithContext(reader);
                                return false;
                            }
                            break;
                    }
                }

                // Next value
                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    continue;
                }

                break;
            }

            // End of array
            if (!reader.Expect(']', may_throw)) return false;

            // Default
            if (result == null) result = new NbtList(typeof(NbtCompound));
            return true;
        }

        public static bool TryReadValue(StringReader reader, bool may_throw, out INbtArgument result)
        {
            result = null;

            // Quoted (cannot be a number)
            char c = reader.Peek();
            if (reader.IsQuotedStringStart(c))
            {
                reader.Skip();
                if (!reader.TryReadStringUntil(c, may_throw, out string quoted_string_result)) return false;
                result = new NbtString(quoted_string_result);
                return true;
            }

            // Unquoted (may be a number)
            if (!reader.TryReadUnquotedString(out string string_result)) return false;

            // Empty (invalid)
            if (string.IsNullOrEmpty(string_result))
            {
                if (may_throw) CommandError.ExpectedValue().AddWithContext(reader);
                return false;
            }

            // Number type
            if (!TryParseNumber(string_result, out result)) result = new NbtString(string_result);
            return true;
        }


        // Cannot throw, because it's a string otherwise (may add)
        public static bool TryParseNumber(string input, out INbtArgument number)
        {
            number = null;
            if (string.IsNullOrEmpty(input)) return false;

            char last = input[^1];
            string expected_number = input[0..^1];

            switch (last)
            {
                case 'b':
                case 'B':
                    if (sbyte.TryParse(expected_number, INbtArgument.NbtNumberStyles, INbtArgument.NbtNumberFormatInfo, out sbyte byte_result))
                    {
                        number = new NbtByte(byte_result);
                        return true;
                    }
                    return false;
                case 's':
                case 'S':
                    if (short.TryParse(expected_number, INbtArgument.NbtNumberStyles, INbtArgument.NbtNumberFormatInfo, out short short_result))
                    {
                        number = new NbtShort(short_result);
                        return true;
                    }
                    return false;
                case 'l':
                case 'L':
                    if (long.TryParse(expected_number, INbtArgument.NbtNumberStyles, INbtArgument.NbtNumberFormatInfo, out long long_result))
                    {
                        number = new NbtLong(long_result);
                        return true;
                    }
                    return false;
                case 'f':
                case 'F':
                    if (float.TryParse(expected_number, INbtArgument.NbtNumberStyles, INbtArgument.NbtNumberFormatInfo, out float float_result))
                    {
                        number = new NbtFloat(float_result);
                        return true;
                    }
                    return false;
                case 'd':
                case 'D':
                    if (double.TryParse(expected_number, INbtArgument.NbtNumberStyles, INbtArgument.NbtNumberFormatInfo, out double double_result))
                    {
                        number = new NbtDouble(double_result);
                        return true;
                    }
                    return false;
                default:
                    // May be a double (without prefix)
                    if (input.Contains('.') && double.TryParse(input, INbtArgument.NbtNumberStyles, INbtArgument.NbtNumberFormatInfo, out double double_no_suffix_result))
                    {
                        number = new NbtDouble(double_no_suffix_result);
                        return true;
                    }

                    // May be an integer (doesn't have a prefix)
                    if (int.TryParse(input, INbtArgument.NbtNumberStyles, INbtArgument.NbtNumberFormatInfo, out int int_result))
                    {
                        number = new NbtInteger(int_result);
                        return true;
                    }

                    // true or false
                    if (input == "true")
                    {
                        number = new NbtByte(1);
                        return true;
                    }
                    if (input == "false")
                    {
                        number = new NbtByte(0);
                        return true;
                    }
                    return false; // Not a number (string)
            }
        }
    }
}
