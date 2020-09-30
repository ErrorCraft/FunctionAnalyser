using CommandVerifier.Commands;
using TextComponent = CommandVerifier.ComponentParser.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace CommandVerifier.ComponentParser
{
    class ComponentReader
    {
        private static readonly Dictionary<char, char> ESCAPABLE_CHARACTERS = new Dictionary<char, char>() { { '\'', '\'' }, { '"', '"' }, { '\\', '\\' }, { 'b', '\b' }, { 'f', '\f' }, { 'n', '\n' }, { 'r', '\r' }, { 't', '\t' } };
        private static readonly string MALFORMED_JSON_ERROR = "Malformed JSON";
        private static readonly string EXPECTED_VALUE_ERROR = "Expected value";
        private static readonly string UNTERMINATED_ESCAPE_SEQUENCE_ERROR = "Unterminated escape sequence";
        private static readonly string INVALID_ESCAPE_SEQUENCE_ERROR = "Invalid escape sequence";
        private static readonly string EXPECTED_NAME_VALUE_SEPARATOR_ERROR = "Expected ':'";
        private static readonly string END_OF_INPUT = "End of input";
        private static readonly string UNTERMINATED_STRING = "Unterminated string";

        public static bool TryRead(StringReader reader, bool mayThrow, out TextComponent::IComponent result)
        {
            result = new TextComponent::Null();

            if (!reader.CanRead())
            {
                if (mayThrow) CommandError.IncorrectArgument().AddWithContext(reader);
                return false;
            }
            return TryReadAny(reader, mayThrow, out result);
        }

        private static bool TryReadAny(StringReader reader, bool mayThrow, out TextComponent::IComponent result)
        {
            result = new TextComponent::Null();
            reader.SkipWhitespace();

            if (!reader.CanRead()) return true;
            switch (reader.Peek())
            {
                case '{':
                    if (!TryReadCompound(reader, mayThrow, out TextComponent::Object result_compound)) return false;
                    result = result_compound;
                    return true;
                case '[':
                    if (!TryReadArray(reader, mayThrow, out TextComponent::Array result_array)) return false;
                    result = result_array;
                    return true;
                case '"':
                    if (!TryReadString(reader, mayThrow, out TextComponent::String result_string)) return false;
                    result = result_string;
                    return true;
                default:
                    string value = ReadSpecial(reader);
                    if (string.IsNullOrEmpty(value))
                    {
                        if (mayThrow) CommandError.InvalidChatComponent(EXPECTED_VALUE_ERROR).AddWithContext(reader);
                        return false;
                    }
                    if ("true".Equals(value))
                    {
                        result = new TextComponent::Boolean(true);
                        return true;
                    }
                    if ("false".Equals(value))
                    {
                        result = new TextComponent::Boolean(false);
                        return true;
                    }
                    if ("null".Equals(value))
                    {
                        result = new TextComponent::Null();
                        return true;
                    }
                    if (double.TryParse(value, NumberStyles.Number, NumberFormatInfo.InvariantInfo, out _))
                    {
                        result = new TextComponent::Number(value);
                        return true;
                    }
                    break;
            }

            if (mayThrow) CommandError.InvalidChatComponent(MALFORMED_JSON_ERROR).AddWithContext(reader);
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

        private static bool TryReadCompound(StringReader reader, bool mayThrow, out TextComponent::Object compound)
        {
            compound = new TextComponent::Object();

            if (!reader.CanRead() || reader.Peek() != '{')
            {
                if (mayThrow) CommandError.InvalidChatComponent(MALFORMED_JSON_ERROR).AddWithContext(reader);
                return false;
            }
            reader.Skip();

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != '}')
            {
                reader.SkipWhitespace();
                if (!TryReadString(reader, mayThrow, out TextComponent::String name)) return false;

                reader.SkipWhitespace();
                if (!reader.CanRead() || reader.Peek() != ':')
                {
                    if (mayThrow) CommandError.InvalidChatComponent(EXPECTED_NAME_VALUE_SEPARATOR_ERROR).AddWithContext(reader);
                    return false;
                }
                reader.Skip();

                reader.SkipWhitespace();
                if (!TryReadAny(reader, mayThrow, out TextComponent::IComponent value)) return false;
                compound.Values.Add(name, value);

                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    continue;
                }
                break;
            }

            if (!reader.CanRead() || reader.Peek() != '}')
            {
                if (mayThrow) CommandError.InvalidChatComponent(END_OF_INPUT).AddWithContext(reader);
                return false;
            }
            reader.Skip();
            return true;
        }

        private static bool TryReadArray(StringReader reader, bool mayThrow, out TextComponent::Array array)
        {
            array = new TextComponent::Array();

            if (!reader.CanRead() || reader.Peek() != '[')
            {
                if (mayThrow) CommandError.InvalidChatComponent(MALFORMED_JSON_ERROR).AddWithContext(reader);
                return false;
            }
            reader.Skip();

            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != ']')
            {
                reader.SkipWhitespace();
                if (!TryReadAny(reader, mayThrow, out TextComponent::IComponent value)) return false;
                array.Values.Add(value);

                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    continue;
                }
                break;
            }

            if (!reader.CanRead() || reader.Peek() != ']')
            {
                if (mayThrow) CommandError.InvalidChatComponent(END_OF_INPUT).AddWithContext(reader);
                return false;
            }
            reader.Skip();
            return true;
        }

        private static bool TryReadString(StringReader reader, bool mayThrow, out TextComponent::String result)
        {
            result = "";

            if (!reader.CanRead() || reader.Peek() != '"')
            {
                if (mayThrow) CommandError.InvalidChatComponent(MALFORMED_JSON_ERROR).AddWithContext(reader);
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
                            if (mayThrow) CommandError.InvalidChatComponent(UNTERMINATED_ESCAPE_SEQUENCE_ERROR).AddWithContext(reader);
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
                        if (mayThrow) CommandError.InvalidChatComponent(INVALID_ESCAPE_SEQUENCE_ERROR).AddWithContext(reader);
                        return false;
                    }
                }
                else if (c == '\\') escaped = true;
                else if (c == '"') return true;
                else result += c;
            }

            if (mayThrow) CommandError.InvalidChatComponent(UNTERMINATED_STRING).AddWithContext(reader);
            return false;
        }
    }
}
