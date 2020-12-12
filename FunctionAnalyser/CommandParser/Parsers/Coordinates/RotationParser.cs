using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Parsers.Coordinates
{
    public class RotationParser
    {
        private readonly IStringReader StringReader;
        private readonly int Start;

        public RotationParser(IStringReader stringReader)
        {
            StringReader = stringReader;
            Start = stringReader.GetCursor();
        }

        public ReadResults Parse(out Rotation result)
        {
            result = default;
            AngleParser angleParser = new AngleParser(StringReader);

            ReadResults readResults = angleParser.Read(out Angle yRotation);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                StringReader.SetCursor(Start);
                return new ReadResults(false, CommandError.RotationIncomplete().WithContext(StringReader));
            }
            StringReader.Skip();

            readResults = angleParser.Read(out Angle xRotation);
            if (readResults.Successful) result = new Rotation(yRotation, xRotation);
            return readResults;
        }
    }
}
