using CommandVerifier.Commands.SubcommandTypes.Selector;
using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class ScoreHolder : Subcommand
    {
        [JsonProperty("limited_to_one")]
        [DefaultValue(false)]
        public bool LimitedToOne { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }

            int start = reader.Cursor;
            if (reader.CanRead() && reader.Peek() == '@')
            {
                EntitySelectorParser entitySelectorParser = new EntitySelectorParser();
                if (!entitySelectorParser.TryParse(reader, throw_on_fail, out EntitySelector entitySelector)) return false;
                if (LimitedToOne && entitySelector.MaxResults > 1)
                {
                    reader.SetCursor(start);
                    if (throw_on_fail) CommandError.SelectorTooManyEntities().AddWithContext(reader);
                    return false;
                }
                return true;
            }

            while (!reader.IsEndOfArgument()) reader.Skip();
            
            SetLoopAttributes(reader);
            return true;
        }
    }
}
