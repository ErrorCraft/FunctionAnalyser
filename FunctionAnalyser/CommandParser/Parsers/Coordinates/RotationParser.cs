using CommandParser.Results;
using CommandParser.Results.Arguments.Coordinates;

namespace CommandParser.Parsers.Coordinates
{
    public class RotationParser
    {
        private readonly IStringReader StringReader;
        private readonly int Start;
        private readonly bool UseBedrock;

        public RotationParser(IStringReader stringReader, bool useBedrock)
        {
            StringReader = stringReader;
            Start = stringReader.GetCursor();
            UseBedrock = useBedrock;
        }

        public ReadResults Parse(out Rotation result)
        {
            result = default;
            AngleParser angleParser = new AngleParser(StringReader);

            ReadResults readResults = angleParser.Read(out Angle yRotation);
            if (!readResults.Successful) return readResults;
            if (!StringReader.AtEndOfArgument())
            {
                if (!UseBedrock)
                {
                    StringReader.SetCursor(Start);
                    return ReadResults.Failure(CommandError.RotationIncomplete().WithContext(StringReader));
                }
            } else StringReader.Skip();

            readResults = angleParser.Read(out Angle xRotation);
            if (readResults.Successful) result = new Rotation(yRotation, xRotation);
            return readResults;
        }
    }
}
