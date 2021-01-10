using CommandParser.Results;
using CommandParser.Results.Arguments;

namespace CommandParser.Parsers
{
    public class ObjectiveCriterionParser
    {
        private readonly IStringReader StringReader;
        private readonly DispatcherResources Resources;
        
        public ObjectiveCriterionParser(IStringReader stringReader, DispatcherResources resources)
        {
            StringReader = stringReader;
            Resources = resources;
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

        private bool ReadNormalCriterion(string input)
        {
            string[] values = input.Split('.');
            if (values.Length > 2) return false;

            if (!Resources.ObjectiveCriteria.TryGetNormalCriterion(values[0], out Collections.ObjectiveCriterion contents)) return false;
            if (values.Length == 1) return contents.Read("", Resources);
            else return contents.Read(values[1], Resources);
        }

        private bool ReadNamespacedCriterion(string input)
        {
            string[] values = input.Split(':');
            if (values.Length != 2) return false;

            string criterion = Shorten(values[0]);
            if (!Resources.ObjectiveCriteria.TryGetNamespacedCriterion(criterion, out Collections.ObjectiveCriterion contents)) return false;
            return contents.Read(Shorten(values[1]), Resources);
        }

        private static string Shorten(string input)
        {
            if (input.StartsWith("minecraft.")) return input[10..];
            else return input;
        }
    }
}
