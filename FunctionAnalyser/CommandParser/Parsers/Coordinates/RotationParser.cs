using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Parsers.Coordinates
{
    public class RotationParser
    {
        private readonly StringReader StringReader;
        private readonly int Start;

        public RotationParser(StringReader stringReader)
        {
            StringReader = stringReader;
            Start = stringReader.Cursor;
        }

        public ReadResults Parse(out Rotation result)
        {
            result = default;
            AngleParser angleParser = new AngleParser(StringReader);

            ReadResults readResults = angleParser.Read(out Angle yRotation);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.Cursor = Start;
                return new ReadResults(false, CommandError.RotationIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = angleParser.Read(out Angle xRotation);
            if (readResults.Successful) result = new Rotation(yRotation, xRotation);
            return readResults;
        }
    }
}
