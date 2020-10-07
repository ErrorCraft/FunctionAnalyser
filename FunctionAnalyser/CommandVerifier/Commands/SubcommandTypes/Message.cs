using CommandVerifier.Commands.SubcommandTypes.Selector;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Message : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            while (reader.CanRead())
            {
                if (reader.Peek() == '@')
                {
                    if (reader.CanRead(2)) {
                        if ("parse".Contains(reader.Peek(1)))
                        {
                            EntitySelectorParser entitySelectorParser = new EntitySelectorParser();
                            if (!entitySelectorParser.TryParse(reader, throw_on_fail, out _)) return false;
                        }
                        else
                        {
                            reader.Skip(2);
                            continue;
                        }
                    }
                }
                reader.Skip();
            }
            return true;
        }
    }
}
