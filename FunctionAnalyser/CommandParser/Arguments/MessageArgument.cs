using CommandParser.Parsers;
using CommandParser.Results;
using CommandParser.Results.Arguments;
using System.Collections.Generic;

namespace CommandParser.Arguments
{
    public class MessageArgument : IArgument<Message>
    {
        public ReadResults Parse(IStringReader reader, DispatcherResources resources, out Message result)
        {
            result = default;
            string message = reader.GetRemaining();
            Dictionary<int, EntitySelector> selectors = new Dictionary<int, EntitySelector>();

            while (reader.CanRead())
            {
                if (reader.Peek() == '@')
                {
                    if (reader.CanRead(2) && "parse".Contains(reader.Peek(1)))
                    {
                        int start = reader.GetCursor();
                        ReadResults readResults = new EntitySelectorParser(reader, resources).Parse(out EntitySelector entitySelector);
                        if (!readResults.Successful) return readResults;
                        selectors.Add(start, entitySelector);
                    } else
                    {
                        reader.Skip(2);
                    }
                    continue;
                }
                reader.Skip();
            }

            result = new Message(message, selectors);
            return new ReadResults(true, null);
        }
    }
}
