using CommandParser.Minecraft;
using CommandParser.Minecraft.Nbt;
using CommandParser.Minecraft.Nbt.Tags;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using System.Collections.Generic;

namespace CommandParser.Parsers {
    public class BlockParser {
        private readonly IStringReader StringReader;
        private readonly bool ForTesting;
        private readonly DispatcherResources Resources;
        private readonly bool UseBedrock;

        private ResourceLocation Block;
        private bool IsTag = false;

        public BlockParser(IStringReader stringReader, bool forTesting, DispatcherResources resources, bool useBedrock) {
            StringReader = stringReader;
            ForTesting = forTesting;
            Resources = resources;
            UseBedrock = useBedrock;

            Block = null;
            IsTag = false;
        }

        public ReadResults Parse(out Block result) {
            result = default;

            ReadResults readResults = ReadTag();
            if (!readResults.Successful) return readResults;

            readResults = ResourceLocation.TryRead(StringReader, out Block);
            if (!readResults.Successful) return readResults;

            // Temporary
            if (UseBedrock) {
                result = new Block(Block, null, null, IsTag);
                return ReadResults.Success();
            }

            if (!IsTag && !Resources.Blocks.ContainsBlock(Block)) {
                return ReadResults.Failure(CommandError.UnknownBlock(Block));
            }

            Dictionary<string, string> blockStates = null;
            if (StringReader.CanRead() && StringReader.Peek() == '[') {
                readResults = ReadBlockStates(out blockStates);
                if (!readResults.Successful) return readResults;
            }

            INbtTag nbt = null;
            if (StringReader.CanRead() && StringReader.Peek() == '{') {
                readResults = new NbtParser(StringReader).ReadCompound(out nbt);
                if (!readResults.Successful) return readResults;
            }

            result = new Block(Block, blockStates, nbt, IsTag);
            return ReadResults.Success();
        }

        private ReadResults ReadTag() {
            if (StringReader.CanRead() && StringReader.Peek() == '#') {
                if (!ForTesting) return ReadResults.Failure(CommandError.BlockTagsNotAllowed().WithContext(StringReader));
                IsTag = true;
                StringReader.Skip();
            }
            return ReadResults.Success();
        }

        private ReadResults ReadBlockStates(out Dictionary<string, string> result) {
            result = new Dictionary<string, string>();

            ReadResults readResults = StringReader.Expect('[');
            if (!readResults.Successful) return readResults;
            StringReader.SkipWhitespace();

            while (StringReader.CanRead() && StringReader.Peek() != ']') {
                StringReader.SkipWhitespace();
                int start = StringReader.GetCursor();

                readResults = StringReader.ReadString(out string property);
                if (!readResults.Successful) return readResults;
                if (result.ContainsKey(property)) {
                    StringReader.SetCursor(start);
                    return ReadResults.Failure(CommandError.DuplicateBlockProperty(Block, property).WithContext(StringReader));
                }

                if (!IsTag && !Resources.Blocks.ContainsProperty(Block, property)) {
                    StringReader.SetCursor(start);
                    return ReadResults.Failure(CommandError.UnknownBlockProperty(Block, property).WithContext(StringReader));
                }

                StringReader.SkipWhitespace();
                start = StringReader.GetCursor();

                if (!StringReader.CanRead() || StringReader.Peek() != '=') {
                    return ReadResults.Failure(CommandError.ExpectedValueForBlockProperty(Block, property));
                }
                StringReader.Skip();

                StringReader.SkipWhitespace();
                readResults = StringReader.ReadString(out string value);
                if (!readResults.Successful) return readResults;

                if (!IsTag && !Resources.Blocks.PropertyContainsValue(Block, property, value)) {
                    StringReader.SetCursor(start);
                    return ReadResults.Failure(CommandError.UnknownBlockPropertyValue(Block, property, value).WithContext(StringReader));
                }

                result.Add(property, value);

                StringReader.SkipWhitespace();
                if (StringReader.CanRead() && StringReader.Peek() == ',') {
                    StringReader.Skip();
                    continue;
                }
                break;
            }

            if (!StringReader.CanRead() || StringReader.Peek() != ']') {
                return ReadResults.Failure(CommandError.UnclosedBlockStateProperties().WithContext(StringReader));
            }
            StringReader.Skip();

            return ReadResults.Success();
        }
    }
}
