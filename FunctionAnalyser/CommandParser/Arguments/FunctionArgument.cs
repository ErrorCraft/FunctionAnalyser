using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class FunctionArgument : IArgument<Function>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Function result)
        {
            result = default;
            bool isTag = false;
            if (reader.CanRead() && reader.Peek() == '#')
            {
                reader.Skip();
                isTag = true;
            }

            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation function);
            if (readResults.Successful) result = new Function(function, isTag);
            return readResults;
        }
    }
}
