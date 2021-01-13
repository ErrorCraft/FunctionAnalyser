using CommandParser.Results;
using CommandParser.Results.Arguments;
using System.Text.RegularExpressions;

namespace CommandParser.Parsers
{
    public class ResourceLocationParser
    {
        private static readonly Regex RESOURCE_LOCATION_REGEX = new Regex("^[a-z0-9._-]*:?[a-z0-9._/-]*$");
        private readonly IStringReader StringReader;

        public ResourceLocationParser(IStringReader stringReader)
        {
            StringReader = stringReader;
        }

        public ReadResults Read(out ResourceLocation result)
        {
            int start = StringReader.GetCursor();
            while (StringReader.CanRead() && IsResourceLocationPart(StringReader.Peek()))
            {
                StringReader.Skip();
            }

            string s = StringReader.GetString()[start..StringReader.GetCursor()];
            return ReadFromString(s, start, out result);
        }

        public ReadResults ReadFromString(string s, int start, out ResourceLocation result)
        {
            if (TryParse(s, out result)) return new ReadResults(true, null);
            StringReader.SetCursor(start);
            return new ReadResults(false, CommandError.InvalidId().WithContext(StringReader));
        }

        public static bool TryParse(string s, out ResourceLocation result)
        {
            result = default;
            if (!RESOURCE_LOCATION_REGEX.IsMatch(s)) return false;
            string[] splitValues = s.Split(ResourceLocation.NAMESPACE_SEPARATOR);

            if (splitValues.Length == 1)
            {
                result = new ResourceLocation(splitValues[0]);
            }
            else if (string.IsNullOrEmpty(splitValues[0]))
            {
                result = new ResourceLocation(splitValues[1]);
            }
            else
            {
                result = new ResourceLocation(splitValues[1], splitValues[0]);
            }
            return true;
        }

        private static bool IsResourceLocationPart(char c)
        {
            return c >= '0' && c <= '9' ||
                c >= 'a' && c <= 'z' ||
                c == ':' || c == '/' ||
                c == '-' || c == '_' ||
                c == '.';
        }
    }
}
