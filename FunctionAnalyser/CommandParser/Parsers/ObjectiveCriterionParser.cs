using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Parsers
{
    public class ObjectiveCriterionParser
    {
        private readonly IStringReader StringReader;
        
        public ObjectiveCriterionParser(IStringReader stringReader)
        {
            StringReader = stringReader;
        }

        public ReadResults ByName(out ObjectiveCriterion result)
        {
            int start = StringReader.GetCursor();
            while (!StringReader.AtEndOfArgument())
            {
                StringReader.Skip();
            }
            string criterion = StringReader.GetString()[start..StringReader.GetCursor()];

            if (criterion.Contains(':'))
            {
                if (ReadNamespacedCriterion(criterion))
                {
                    result = new ObjectiveCriterion(criterion);
                    return new ReadResults(true, null);
                }
            } else if (ReadNormalCriterion(criterion))
            {
                result = new ObjectiveCriterion(criterion);
                return new ReadResults(true, null);
            }

            result = default;
            return new ReadResults(false, CommandError.UnknownCriterion(criterion));
        }

        private static bool ReadNormalCriterion(string input)
        {
            string[] values = input.Split('.');
            if (values.Length > 2) return false;

            if (!Collections.ObjectiveCriteria.TryGetNormalCriterion(values[0], out Collections.ObjectiveCriterion contents)) return false;
            if (values.Length == 1) return contents.Read("");
            else return contents.Read(values[1]);
        }

        private static bool ReadNamespacedCriterion(string input)
        {
            string[] values = input.Split(':');
            if (values.Length != 2) return false;

            string criterion = Shorten(values[0]);
            if (!Collections.ObjectiveCriteria.TryGetNamespacedCriterion(criterion, out Collections.ObjectiveCriterion contents)) return false;
            return contents.Read(Shorten(values[1]));
        }

        private static string Shorten(string input)
        {
            if (input.StartsWith("minecraft.")) return input[10..];
            else return input;
        }
    }
}
