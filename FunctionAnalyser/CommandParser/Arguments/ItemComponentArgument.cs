using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ItemComponentArgument : IArgument<ItemComponent>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out ItemComponent result)
        {
            result = default;
            return new ReadResults(true, null);
        }
    }
}
