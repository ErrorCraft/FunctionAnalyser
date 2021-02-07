using CommandParser.Minecraft;
using CommandParser.Results;

namespace CommandParser.Arguments
{
    public class ResourceLocationArgument : IArgument<ResourceLocation>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ResourceLocation result)
        {
            return ResourceLocation.TryRead(reader, out result);
        }
    }
}
