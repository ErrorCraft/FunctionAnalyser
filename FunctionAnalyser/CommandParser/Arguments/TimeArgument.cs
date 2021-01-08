using CommandParser.Collections;
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
                return new ReadResults(false, CommandError.InvalidTickCount());
            }

            if (!reader.AtEndOfArgument())
            {
                if (TimeScalars.TryGetScalar(reader.Peek(), out int scalar))
                {
                    time *= scalar;
                    reader.Skip();
                } else
                {
                    return new ReadResults(false, CommandError.InvalidTimeUnit().WithContext(reader));
                }
            }

            result = new Time((int)time);
            return new ReadResults(true, null);
        }
    }
}
