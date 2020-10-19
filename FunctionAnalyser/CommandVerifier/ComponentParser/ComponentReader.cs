using CommandVerifier.Commands;
using JsonTypes = CommandVerifier.ComponentParser.JsonTypes;
using System.Collections.Generic;
using System.Globalization;

namespace CommandVerifier.ComponentParser
{
    class ComponentReader
    {
        private static readonly Dictionary<char, char> ESCAPABLE_CHARACTERS = new Dictionary<char, char>() { { '\'', '\'' }, { '"', '"' }, { '\\', '\\' }, { 'b', '\b' }, { 'f', '\f' }, { 'n', '\n' }, { 'r', '\r' }, { 't', '\t' } };

        public static bool TryRead(StringReader reader, bool mayThrow, out JsonTypes::IComponent result)
        {
            result = new JsonTypes::Null();

            if (!reader.CanRead())
            {
                if (mayThrow) CommandError.IncorrectArgument().AddWithContext(reader);
                return false;
            }
            return TryReadAny(reader, mayThrow, out result);
        }

        private static bool TryReadAny(StringReader reader, bool mayThrow, out JsonTypes::IComponent result)
        {
            result = new JsonTypes::Null();
            SkipWhitespace(reader);

            if (!reader.CanRead()) return true;
            switch (reader.Peek())
            {
                case '{':
                    if (!TryReadCompound(reader, mayThrow, out JsonTypes::Object result_compound)) return false;
                    result = result_compound;
                    return true;
                case '[':
                    if (!TryReadArray(reader, mayThrow, out JsonTypes::Array result_array)) return false;
                    result = result_array;
                    return true;
                case '"':
                    if (!TryReadString(reader, mayThrow, out JsonTypes::String result_string)) return false;
                    result = result_string;
                    return true;
                default:
                    string value = ReadSpecial(reader);
                    if (string.IsNullOrEmpty(value))
                    {
                        if (mayThrow) ComponentError.ExpectedValueError().AddWithContext(reader);
                        return false;
                    }
                    if ("true".Equals(value))
                    {
                        result = new JsonTypes::Boolean(true);
                        return true;
                    }
                    if ("false".Equals(value))
                    {
                        result = new JsonTypes::Boolean(false);
                        return true;
                    }
                    if ("null".Equals(value))
                    {
                        result = new JsonTypes::Null();
                        return true;
                    }
                    if (double.TryParse(value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out _))
                    {
                        result = new JsonTypes::Number(value);
                        return true;
                    }
                    break;
            }

            if (mayThrow) ComponentError.MalformedJsonError().AddWithContext(reader);
            return false;
        }

        private static bool IsSpecialTypePart(char c)
            => c != ' ' && c != ',' && c != '"' && c != '[' && c != ']' && c != '{' && c != '}';

        private static string ReadSpecial(StringReader reader)
        {
            int start = reader.Cursor;
            while (reader.CanRead() && IsSpecialTypePart(reader.Peek())) reader.Skip();
            return reader.Command[start..reader.Cursor];
        }

        private static bool TryReadCompound(StringReader reader, bool mayThrow, out JsonTypes::Object compound)
        {
            compound = new JsonTypes::Object();

            if (!reader.CanRead() || reader.Peek() != '{')
            {
                if (mayThrow) ComponentError.MalformedJsonError().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            SkipWhitespace(reader);
            while (reader.CanRead() && reader.Peek() != '}')
            {
                SkipWhitespace(reader);
                if (!TryReadString(reader, mayThrow, out JsonTypes::String name)) return false;

                SkipWhitespace(reader);
                if (!reader.CanRead() || reader.Peek() != ':')
                {
                    if (mayThrow) ComponentError.ExpectedNameValueSeparatorError().AddWithContext(reader);
                    return false;
                }
                reader.Skip();

                SkipWhitespace(reader);
                if (!TryReadAny(reader, mayThrow, out JsonTypes::IComponent value)) return false;
                if (compound.Values.ContainsKey(name))
                {
                    compound.Values[name] = value;
                } else
                {
                    compound.Values.Add(name, value);
                }

                SkipWhitespace(reader);
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    continue;
                }
                break;
            }

            if (!reader.CanRead() || reader.Peek() != '}')
            {
                if (mayThrow) ComponentError.EndOfInputError().AddWithContext(reader);
                return false;
            }
            reader.Skip();
            return true;
        }

        private static bool TryReadArray(StringReader reader, bool mayThrow, out JsonTypes::Array array)
        {
            array = new JsonTypes::Array();

            if (!reader.CanRead() || reader.Peek() != '[')
            {
                if (mayThrow) ComponentError.MalformedJsonError().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            SkipWhitespace(reader);
            while (reader.CanRead() && reader.Peek() != ']')
            {
                SkipWhitespace(reader);
                if (!TryReadAny(reader, mayThrow, out JsonTypes::IComponent value)) return false;
                array.Values.Add(value);

                SkipWhitespace(reader);
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    continue;
                }
                break;
            }

            if (!reader.CanRead() || reader.Peek() != ']')
            {
                if (mayThrow) ComponentError.EndOfInputError().AddWithContext(reader);
                return false;
            }
            reader.Skip();
            return true;
        }

        private static bool TryReadString(StringReader reader, bool mayThrow, out JsonTypes::String result)
        {
            result = "";

            if (!reader.CanRead() || reader.Peek() != '"')
            {
                if (mayThrow) ComponentError.MalformedJsonError().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            bool escaped = false;
            while (reader.CanRead())
            {
                char c = reader.Read();
                if (escaped)
                {
                    if (c == 'u')
                    {
                        if (!reader.CanRead(4))
                        {
                            if (mayThrow) ComponentError.UnterminatedEscapeSequenceError().AddWithContext(reader);
                            return false;
                        }
                        string hex = reader.Read(4);
                        if (!short.TryParse(hex, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out short s))
                        {
                            if (mayThrow) CommandError.InvalidChatComponent("\\u" + hex).AddWithContext(reader);
                            return false;
                        }
                        result += (char)s;
                        escaped = false;
                    } else if (ESCAPABLE_CHARACTERS.ContainsKey(c))
                    {
                        result += ESCAPABLE_CHARACTERS[c];
                        escaped = false;
                    }
                    else
                    {
                        if (mayThrow) ComponentError.InvalidEscapeSequenceError().AddWithContext(reader);
                        return false;
                    }
                }
                else if (c == '\\') escaped = true;
                else if (c == '"') return true;
                else result += c;
            }

            if (mayThrow) ComponentError.UnterminatedStringError().AddWithContext(reader);
            return false;
        }

        private static void SkipWhitespace(StringReader reader)
        {
            while (reader.CanRead() && IsWhitespace(reader.Peek())) reader.Skip();
        }

        private static bool IsWhitespace(char c)
        {
            return c == ' ' || c == '\t';
        }
    }
}
