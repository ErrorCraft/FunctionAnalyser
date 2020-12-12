using CommandParser.Collections;
using CommandParser.Parsers.NbtParser;
using CommandParser.Parsers.NbtParser.NbtArguments;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Parsers
{
    public class ItemParser
    {
        private readonly IStringReader StringReader;
        private readonly bool ForTesting;

        public ItemParser(IStringReader stringReader, bool forTesting)
        {
            StringReader = stringReader;
            ForTesting = forTesting;
        }

        public ReadResults Parse(out Item result)
        {
            result = default;
            bool isTag = false;

            if (StringReader.CanRead() && StringReader.Peek() == '#')
            {
                if (ForTesting)
                {
                    isTag = true;
                    StringReader.Skip();
                } else
                {
                    return new ReadResults(false, CommandError.ItemTagsNotAllowed().WithContext(StringReader));
                }
            }

            ReadResults readResults = new ResourceLocationParser(StringReader).Read(out ResourceLocation item);
            if (!readResults.Successful) return readResults;

            if (!isTag && !Items.Contains(item))
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
    }
}
