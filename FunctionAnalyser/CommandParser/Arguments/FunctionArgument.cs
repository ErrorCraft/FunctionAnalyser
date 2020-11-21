using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class FunctionArgument : IArgument<Function>
    {
        public ReadResults Parse(StringReader reader, out Function result)
        {
            result = default;
            bool isTag = false;
            if (reader.CanRead() && reader.Peek() == '#')
            {
                reader.Skip();
                isTag = true;
            }

            ReadResults readResults = new ResourceLocationParser(reader).Read(out ResourceLocation function);
            if (readResults.Successful) result = new Function(new ResourceLocation(function, isTag));
            return readResults;
        }
    }
}
