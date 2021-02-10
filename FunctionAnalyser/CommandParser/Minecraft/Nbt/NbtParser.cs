using CommandParser.Minecraft.Nbt.Tags;
using CommandParser.Results;
using System;
using static CommandParser.Minecraft.Nbt.NbtUtilities;

namespace CommandParser.Minecraft.Nbt {
    public class NbtParser {
        private readonly IStringReader Reader;

        public NbtParser(IStringReader reader) {
            Reader = reader;
        }

        public ReadResults ReadValue(out INbtTag result) {
            result = default;
            Reader.SkipWhitespace();
            if (!Reader.CanRead()) return ReadResults.Failure(CommandError.ExpectedValue().WithContext(Reader));
            char c = Reader.Peek();
            if (c == '{') {
                return ReadCompound(out result);
            }
            if (c == '[') {
                return ReadList(out result);
            }
            return ReadTypedValue(out result);
        }

        public ReadResults ReadCompound(out INbtTag result) {
            CompoundNbtTag compound;
            result = compound = new CompoundNbtTag();
            ReadResults readResults = Reader.Expect('{');
            if (!readResults.Successful) return readResults;
            Reader.SkipWhitespace();
            while (Reader.CanRead() && Reader.Peek() != '}') {
                int start = Reader.GetCursor();
                readResults = ReadKey(out string key);
                if (!readResults.Successful) return readResults;
                if (string.IsNullOrEmpty(key)) {
                    Reader.SetCursor(start);
                    return ReadResults.Failure(CommandError.ExpectedKey().WithContext(Reader));
                }
                readResults = Reader.Expect(':');
                if (!readResults.Successful) return readResults;
                readResults = ReadValue(out INbtTag value);
                if (!readResults.Successful) return readResults;
                compound.Add(key, value);
                if (!HasElementSeparator()) break;
                if (Reader.CanRead()) continue;
                return ReadResults.Failure(CommandError.ExpectedKey().WithContext(Reader));
            }
            return Reader.Expect('}');
        }

        private ReadResults ReadKey(out string result) {
            Reader.SkipWhitespace();
            if (!Reader.CanRead()) {
                result = default;
                return ReadResults.Failure(CommandError.ExpectedKey().WithContext(Reader));
            }
            return Reader.ReadString(out result);
        }

        private ReadResults ReadList(out INbtTag result) {
            if (Reader.CanRead(3) && !Reader.IsQuotedStringStart(Reader.Peek(1)) && Reader.Peek(2) == ';') return ReadArrayTag(out result);
            else return ReadListTag(out result);
        }

        private ReadResults ReadListTag(out INbtTag result) {
            result = default;
            ReadResults readResults = Reader.Expect('[');
            if (!readResults.Successful) return readResults;
            if (!Reader.CanRead()) return ReadResults.Failure(CommandError.ExpectedValue().WithContext(Reader));

            INbtCollectionTag list = new ListNbtTag();
            return ReadListContents(ref list);
        }

        private ReadResults ReadArrayTag(out INbtTag result) {
            result = default;
            ReadResults readResults = Reader.Expect('[');
            if (!readResults.Successful) return readResults;
            int start = Reader.GetCursor();
            char type = Reader.Read();
            Reader.Skip();
            Reader.SkipWhitespace();
            if (!Reader.CanRead()) return ReadResults.Failure(CommandError.ExpectedValue().WithContext(Reader));

            INbtCollectionTag list;
            switch (type) {
                case 'B':
                    list = new ByteArrayNbtTag();
                    break;
                case 'I':
                    list = new IntegerArrayNbtTag();
                    break;
                case 'L':
                    list = new LongArrayNbtTag();
                    break;
                default:
                    Reader.SetCursor(start);
                    return ReadResults.Failure(CommandError.InvalidArrayType(type).WithContext(Reader));
            }
            readResults = ReadListContents(ref list);
            if (readResults.Successful) result = list;
            return readResults;
        }

        private ReadResults ReadListContents(ref INbtCollectionTag list) {
            ReadResults readResults;
            while (Reader.CanRead() && Reader.Peek() != ']') {
                int start = Reader.GetCursor();
                readResults = ReadValue(out INbtTag tag);
                if (!readResults.Successful) return readResults;
                if (!list.Add(tag)) {
                    Reader.SetCursor(start);
                    return ReadResults.Failure(CommandError.NbtCannotInsert(tag, list).WithContext(Reader));
                }
                if (!HasElementSeparator()) break;
                if (Reader.CanRead()) continue;
                return ReadResults.Failure(CommandError.ExpectedValue().WithContext(Reader));
            }
            return Reader.Expect(']');
        }

        private ReadResults ReadTypedValue(out INbtTag result) {
            result = default;

            Reader.SkipWhitespace();
            int start = Reader.GetCursor();
            ReadResults readResults;

            if (Reader.IsQuotedStringStart(Reader.Peek())) {
                readResults = Reader.ReadQuotedString(out string quotedString);
                if (readResults.Successful) result = new StringNbtTag(quotedString);
                return readResults;
            }
            readResults = Reader.ReadUnquotedString(out string s);
            if (!readResults.Successful) return readResults;
            if (string.IsNullOrEmpty(s)) {
                Reader.SetCursor(start);
                return ReadResults.Failure(CommandError.ExpectedValue());
            }

            result = GetType(s);
            return ReadResults.Success();
        }

        private static INbtTag GetType(string input) {
            char lastCharacter = input[^1];
            string inputWithoutSuffix = input[..^1];

            switch (lastCharacter) {
                case 'b':
                case 'B':
                    if (sbyte.TryParse(inputWithoutSuffix, NbtNumberStylesInteger, NbtNumberFormatInfo, out sbyte byteResult)) return new ByteNbtTag(byteResult);
                    break;
                case 's':
                case 'S':
                    if (!short.TryParse(inputWithoutSuffix, NbtNumberStylesInteger, NbtNumberFormatInfo, out short shortResult)) return new ShortNbtTag(shortResult);
                    break;
                case 'l':
                case 'L':
                    if (!long.TryParse(inputWithoutSuffix, NbtNumberStylesInteger, NbtNumberFormatInfo, out long longResult)) return new LongNbtTag(longResult);
                    break;
                case 'f':
                case 'F':
                    if (!float.TryParse(inputWithoutSuffix, NbtNumberStylesFloating, NbtNumberFormatInfo, out float floatResult)) return new FloatNbtTag(floatResult);
                    break;
                case 'd':
                case 'D':
                    if (!double.TryParse(inputWithoutSuffix, NbtNumberStylesFloating, NbtNumberFormatInfo, out double doubleResult)) return new DoubleNbtTag(doubleResult);
                    break;
                default:
                    if (int.TryParse(input, NbtNumberStylesInteger, NbtNumberFormatInfo, out int integerResult)) return new IntegerNbtTag(integerResult);
                    else if (double.TryParse(input, NbtNumberStylesFloating, NbtNumberFormatInfo, out double doubleResultWithoutSuffix)) return new DoubleNbtTag(doubleResultWithoutSuffix);
                    else if ("true".Equals(input, StringComparison.InvariantCultureIgnoreCase)) return ByteNbtTag.ONE;
                    else if ("false".Equals(input)) return ByteNbtTag.ZERO;
                    break;
            }
            return new StringNbtTag(input);
        }

        private bool HasElementSeparator() {
            Reader.SkipWhitespace();
            if (Reader.CanRead() && Reader.Peek() == ',') {
                Reader.Skip();
                Reader.SkipWhitespace();
                return true;
            }
            return false;
        }
    }
}
