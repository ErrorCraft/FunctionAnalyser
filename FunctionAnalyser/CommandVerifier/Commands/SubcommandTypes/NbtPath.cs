namespace CommandVerifier.Commands.SubcommandTypes
{
    class NbtPath : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            bool is_root = true;
            while (!reader.IsEndOfArgument())
            {
                if (!TryReadNode(reader, throw_on_fail, is_root)) return false;
                is_root = false;
                if (!reader.CanRead()) continue;
                char c = reader.Peek();
                if (c == ' ' || c == '[' || c == '{') continue;
                if (!reader.Expect('.', throw_on_fail)) return false;
            }
            SetLoopAttributes(reader);
            reader.Information.NbtAccess++;
            return true;
        }

        private static bool TryReadNode(StringReader reader, bool may_throw, bool is_root)
        {
            switch (reader.Peek())
            {
                case '{':
                    if (!is_root)
                    {
                        if (may_throw) CommandError.NbtPathInvalid().AddWithContext(reader);
                        return false;
                    }
                    return NbtParser.NbtReader.TryParse(reader, may_throw, out _);
                case '[':
                    reader.Skip();
                    if (!reader.CanRead())
                    {
                        if (may_throw) CommandError.ExpectedInteger().AddWithContext(reader);
                        return false;
                    }
                    char c = reader.Peek();
                    if (c == '{')
                    {
                        if (!NbtParser.NbtReader.TryParse(reader, may_throw, out _)) return false;
                        return reader.Expect(']', may_throw);
                    }
                    if (c == ']')
                    {
                        reader.Skip();
                        return true;
                    }
                    if (!reader.TryReadInt(may_throw, out int _)) return false;
                    if (!reader.Expect(']', may_throw)) return false;
                    return true;
                case '"':
                    return reader.TryReadString(may_throw, out _);
            }

            if (!TryReadUnquotedName(reader, may_throw, out string _)) return false;
            if (reader.CanRead() && reader.Peek() == '{')
            {
                if (!NbtParser.NbtReader.TryParse(reader, may_throw, out _)) return false;
            }
            return true;
        }

        private static bool TryReadUnquotedName(StringReader reader, bool may_throw, out string result)
        {
            int start = reader.Cursor;
            while (reader.CanRead() && IsUnquotedNamePart(reader.Peek())) reader.Skip();
            result = reader.Command[start..reader.Cursor];
            if (string.IsNullOrEmpty(result))
            {
                if (may_throw) CommandError.NbtPathInvalid().AddWithContext(reader);
                return false;
            }
            return true;
        }

        private static bool IsUnquotedNamePart(char c)
            => c != ' ' && c != '"' && c != '[' && c != ']' && c != '.' && c != '{' && c != '}';
    }
}
