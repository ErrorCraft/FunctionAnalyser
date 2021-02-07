using CommandParser.Parsers.NbtParser;
using CommandParser.Results;
using CommandParser.Results.Arguments;


namespace CommandParser.Arguments
{
    public class NbtPathArgument : IArgument<NbtPath>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out NbtPath result)
        {
            result = default;

            int start = reader.GetCursor();
            bool isRoot = true;

            ReadResults readResults;

            while (!reader.AtEndOfArgument())
            {
                readResults = ReadNode(reader, isRoot);
                if (!readResults.Successful) return readResults;

                isRoot = false;
                if (!reader.CanRead()) continue;
                char c = reader.Peek();
                if (c == ' ' || c == '[' || c == '{') continue;
                readResults = reader.Expect('.');
                if (!readResults.Successful) return readResults;
            }
            result = new NbtPath(reader.GetString()[start..reader.GetCursor()]);
            return ReadResults.Success();
        }

        private static ReadResults ReadNode(IStringReader reader, bool isRoot)
        {
            ReadResults readResults;
            switch (reader.Peek())
            {
                case '{':
                    if (!isRoot)
                    {
                        return ReadResults.Failure(CommandError.InvalidNbtPath().WithContext(reader));
                    }
                    return NbtReader.ReadCompound(reader, out _);
                case '[':
                    reader.Skip();
                    if (!reader.CanRead())
                    {
                        return ReadResults.Failure(CommandError.ExpectedInteger().WithContext(reader));
                    }
                    char c = reader.Peek();
                    if (c == ']')
                    {
                        reader.Skip();
                        return ReadResults.Success();
                    }
                    if (c == '{')
                    {
                        readResults = NbtReader.ReadCompound(reader, out _);
                    } else
                    {
                        readResults = reader.ReadInteger(out _);
                    }
                    if (readResults.Successful) readResults = reader.Expect(']');
                    return readResults;
                case '"':
                    return reader.ReadQuotedString(out _);
            }
            readResults = ReadUnquotedName(reader);
            if (readResults.Successful)
            {
                readResults = ReadObjectNode(reader);
            }
            return readResults;
        }

        private static ReadResults ReadObjectNode(IStringReader reader)
        {
            if (reader.CanRead() && reader.Peek() == '{')
            {
                return NbtReader.ReadCompound(reader, out _);
            }
            return ReadResults.Success();
        }

        private static ReadResults ReadUnquotedName(IStringReader reader)
        {
            int start = reader.GetCursor();
            while (reader.CanRead() && IsAllowedInUnquotedName(reader.Peek()))
            {
                reader.Skip();
            }
            if (reader.GetCursor() == start)
            {
                return ReadResults.Failure(CommandError.InvalidNbtPath().WithContext(reader));
            } else
            {
                return ReadResults.Success();
            }
        }

        private static bool IsAllowedInUnquotedName(char c)
        {
            return c != ' ' && c != '\"' && c != '[' && c != ']' && c != '{' && c != '}' && c != '.';
        }
    }
}
