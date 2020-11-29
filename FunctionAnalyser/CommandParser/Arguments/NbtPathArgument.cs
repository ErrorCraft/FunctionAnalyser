﻿using CommandParser.Parsers.NbtParser;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using System;

namespace CommandParser.Arguments
{
    public class NbtPathArgument : IArgument<NbtPath>
    {
        public ReadResults Parse(StringReader reader, out NbtPath result)
        {
            result = default;

            int start = reader.Cursor;
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
            result = new NbtPath(reader.Command[start..reader.Cursor]);
            return new ReadResults(true, null);
        }

        private static ReadResults ReadNode(StringReader reader, bool isRoot)
        {
            ReadResults readResults;
            switch (reader.Peek())
            {
                case '{':
                    if (!isRoot)
                    {
                        return new ReadResults(false, CommandError.InvalidNbtPath().WithContext(reader));
                    }
                    return NbtReader.ReadCompound(reader, out _);
                case '[':
                    reader.Skip();
                    if (!reader.CanRead())
                    {
                        return new ReadResults(false, CommandError.ExpectedInteger().WithContext(reader));
                    }
                    char c = reader.Peek();
                    if (c == ']')
                    {
                        reader.Skip();
                        return new ReadResults(true, null);
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

        private static ReadResults ReadObjectNode(StringReader reader)
        {
            if (reader.CanRead() && reader.Peek() == '{')
            {
                return NbtReader.ReadCompound(reader, out _);
            }
            return new ReadResults(true, null);
        }

        private static ReadResults ReadUnquotedName(StringReader reader)
        {
            int start = reader.Cursor;
            while (reader.CanRead() && IsAllowedInUnquotedName(reader.Peek()))
            {
                reader.Skip();
            }
            if (reader.Cursor == start)
            {
                return new ReadResults(false, CommandError.InvalidNbtPath().WithContext(reader));
            } else
            {
                return new ReadResults(true, null);
            }
        }

        private static bool IsAllowedInUnquotedName(char c)
        {
            return c != ' ' && c != '\"' && c != '[' && c != ']' && c != '{' && c != '}' && c != '.';
        }
    }
}