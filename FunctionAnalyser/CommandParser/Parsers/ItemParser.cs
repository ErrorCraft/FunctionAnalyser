using CommandParser.Minecraft;
using CommandParser.Minecraft.Nbt;
using CommandParser.Minecraft.Nbt.Tags;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Parsers {
    public class ItemParser {
        private readonly IStringReader StringReader;
        private readonly DispatcherResources Resources;
        private readonly bool ForTesting;
        private readonly bool UseBedrock;

        public ItemParser(IStringReader stringReader, DispatcherResources resources, bool forTesting, bool useBedrock) {
            StringReader = stringReader;
            Resources = resources;
            ForTesting = forTesting;
            UseBedrock = useBedrock;
        }

        public ReadResults Parse(out Item result) {
            result = default;

            ReadResults readResults = ReadTag(out bool isTag);
            if (!readResults.Successful) return readResults;

            readResults = ResourceLocation.TryRead(StringReader, out ResourceLocation item);
            if (!readResults.Successful) return readResults;

            // Temporary
            if (UseBedrock) {
                result = new Item(item, null, isTag);
                return ReadResults.Success();
            }

            if (!isTag && !Resources.Items.Contains(item)) {
                return ReadResults.Failure(CommandError.UnknownItem(item));
            }

            if (StringReader.CanRead() && StringReader.Peek() == '{') {
                readResults = new NbtParser(StringReader).ReadCompound(out INbtTag nbt);
                if (readResults.Successful) result = new Item(item, nbt, isTag);
                return readResults;
            }

            result = new Item(item, null, isTag);
            return ReadResults.Success();
        }

        private ReadResults ReadTag(out bool isTag) {
            isTag = false;
            if (StringReader.CanRead() && StringReader.Peek() == '#') {
                if (!ForTesting) return ReadResults.Failure(CommandError.ItemTagsNotAllowed().WithContext(StringReader));
                isTag = true;
                StringReader.Skip();
            }
            return ReadResults.Success();
        }
    }
}
