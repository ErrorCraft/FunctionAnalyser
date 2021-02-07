using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Arguments
{
    public class TimeArgument : IArgument<Time>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Time result)
        {
            result = default;
            ReadResults readResults = reader.ReadFloat(out float time);
            if (!readResults.Successful) return readResults;

            if (time < 0.0f)
            {
                return ReadResults.Failure(CommandError.InvalidTickCount());
            }

            if (!reader.AtEndOfArgument())
            {
                if (resources.TimeScalars.TryGetScalar(reader.Peek(), out int scalar))
                {
                    time *= scalar;
                    reader.Skip();
                } else
                {
                    return ReadResults.Failure(CommandError.InvalidTimeUnit().WithContext(reader));
                }
            }

            result = new Time((int)time);
            return ReadResults.Success();
        }
    }
}
