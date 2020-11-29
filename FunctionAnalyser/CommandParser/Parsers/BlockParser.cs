﻿using CommandParser.Collections;
using CommandParser.Parsers.NbtParser;
using CommandParser.Parsers.NbtParser.NbtArguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using System.Collections.Generic;

namespace CommandParser.Parsers
{
    public class BlockParser
    {
        private readonly StringReader StringReader;
        private readonly bool ForTesting;

        private ResourceLocation Block;
        private bool IsTag = false;

        public BlockParser(StringReader stringReader, bool forTesting)
        {
            StringReader = stringReader;
            ForTesting = forTesting;

            Block = null;
            IsTag = false;
        }

        public ReadResults Parse(out Block result)
        {
            result = default;

            if (StringReader.CanRead() && StringReader.Peek() == '#')
            {
                if (ForTesting)
                {
                    IsTag = true;
                    StringReader.Skip();
                }
                else
                {
                    return new ReadResults(false, CommandError.BlockTagsNotAllowed().WithContext(StringReader));
                }
            }

            ReadResults readResults = new ResourceLocationParser(StringReader).Read(out Block);
            if (!readResults.Successful) return readResults;

            if (!IsTag && !Blocks.ContainsBlock(Block))
            {
                return new ReadResults(false, CommandError.UnknownBlock(Block));
            }

            Dictionary<string, string> blockStates = null;
            if (StringReader.CanRead() && StringReader.Peek() == '[')
            {
                readResults = ReadBlockStates(out blockStates);
                if (!readResults.Successful) return readResults;
            }

            NbtCompound nbt = null;
            if (StringReader.CanRead() && StringReader.Peek() == '{')
            {
                readResults = NbtReader.ReadCompound(StringReader, out nbt);
                if (!readResults.Successful) return readResults;
            }

            result = new Block(Block, blockStates, nbt, IsTag);
            return new ReadResults(true, null);
        }

        private ReadResults ReadBlockStates(out Dictionary<string, string> result)
        {
            result = new Dictionary<string, string>();

            ReadResults readResults = StringReader.Expect('[');
            if (!readResults.Successful) return readResults;
            StringReader.SkipWhitespace();

            while (StringReader.CanRead() && StringReader.Peek() != ']')
            {
                StringReader.SkipWhitespace();
                int start = StringReader.Cursor;

                readResults = StringReader.ReadString(out string property);
                if (!readResults.Successful) return readResults;
                if (result.ContainsKey(property))
                {
                    StringReader.Cursor = start;
                    return new ReadResults(false, CommandError.DuplicateBlockProperty(Block, property).WithContext(StringReader));
                }

                if (!IsTag && !Blocks.ContainsProperty(Block, property))
                {
                    StringReader.Cursor = start;
                    return new ReadResults(false, CommandError.UnknownBlockProperty(Block, property).WithContext(StringReader));
                }

                StringReader.SkipWhitespace();
                start = StringReader.Cursor;

                if (!StringReader.CanRead() || StringReader.Peek() != '=')
                {
                    return new ReadResults(false, CommandError.ExpectedValueForBlockProperty(Block, property));
                }
                StringReader.Skip();

                StringReader.SkipWhitespace();
                readResults = StringReader.ReadString(out string value);
                if (!readResults.Successful) return readResults;

                if (!IsTag && !Blocks.PropertyContainsValue(Block, property, value))
                {
                    StringReader.Cursor = start;
                    return new ReadResults(false, CommandError.UnknownBlockPropertyValue(Block, property, value).WithContext(StringReader));
                }

                result.Add(property, value);

                StringReader.SkipWhitespace();
                if (StringReader.CanRead() && StringReader.Peek() == ',')
                {
                    StringReader.Skip();
                    continue;
                }
                break;
            }

            if (!StringReader.CanRead() || StringReader.Peek() != ']')
            {
                return new ReadResults(false, CommandError.UnclosedBlockStateProperties().WithContext(StringReader));
            }
            StringReader.Skip();

            return new ReadResults(true, null);
        }
    }
}