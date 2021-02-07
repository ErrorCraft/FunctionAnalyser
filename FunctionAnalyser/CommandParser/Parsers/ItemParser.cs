using CommandParser.Minecraft;
using CommandParser.Parsers.NbtParser;
using CommandParser.Parsers.NbtParser.NbtArguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Parsers
{
    public class ItemParser
    {
        private readonly IStringReader StringReader;
        private readonly DispatcherResources Resources;
        private readonly bool ForTesting;
        private readonly bool UseBedrock;

        public ItemParser(IStringReader stringReader, DispatcherResources resources, bool forTesting, bool useBedrock)
        {
            StringReader = stringReader;
            Resources = resources;
            ForTesting = forTesting;
            UseBedrock = useBedrock;
        }

        public ReadResults Parse(out Item result)
        {
            result = default;

            ReadResults readResults = ReadTag(out bool isTag);
            if (!readResults.Successful) return readResults;

            readResults = ResourceLocation.TryRead(StringReader, out ResourceLocation item);
            if (!readResults.Successful) return readResults;

            // Temporary
            if (UseBedrock)
            {
                result = new Item(item, null, isTag);
                return new ReadResults(true, null);
            }

            if (!isTag && !Resources.Items.Contains(item))
            {
                return new ReadResults(false, CommandError.UnknownItem(item));
            }

            if (StringReader.CanRead() && StringReader.Peek() == '{')
            {
                readResults = NbtReader.ReadCompound(StringReader, out NbtCompound nbt);
                if (readResults.Successful) result = new Item(item, nbt, isTag);
                return readResults;
            }

            result = new Item(item, null, isTag);
            return new ReadResults(true, null);
        }

        private ReadResults ReadTag(out bool isTag)
        {
            isTag = false;
            if (StringReader.CanRead() && StringReader.Peek() == '#')
            {
                if (!ForTesting) return new ReadResults(false, CommandError.ItemTagsNotAllowed().WithContext(StringReader));
                isTag = true;
                StringReader.Skip();
            }
            return new ReadResults(true, null);
        }
    }
}
