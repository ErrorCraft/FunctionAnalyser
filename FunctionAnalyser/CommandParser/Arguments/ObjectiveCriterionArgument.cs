using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class ObjectiveCriterionArgument : IArgument<ObjectiveCriterion>
    {
        public ReadResults Parse(IStringReader reader, out ObjectiveCriterion result)
        {
            return new ObjectiveCriterionParser(reader).ByName(out result);
        }
    }
}
