using CommandParser.Minecraft;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class AttributeArgument : IArgument<Attribute>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Attribute result)
        {
            result = default;
            ReadResults readResults = ResourceLocation.TryRead(reader, out ResourceLocation attribute);
            if (readResults.Successful) result = new Attribute(attribute);
            return readResults;
        }
    }
}
