using CommandVerifier.Commands.SubcommandTypes.Selector;
using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Entity : Subcommand
    {
        [JsonProperty("players_only")]
        [DefaultValue(false)]
        public bool PlayersOnly { get; set; }

        [JsonProperty("limited_to_one")]
        [DefaultValue(false)]
        public bool LimitedToOne { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead())
            {
                if (Optional)
                {
                    reader.commandData.EndedOptional = true;
                    return true;
                }

                if (throw_on_fail) CommandError.InvalidNameOrUuid().AddWithContext(reader);
                return false;
            }
            int start = reader.Cursor;

            EntitySelectorParser entitySelectorParser = new EntitySelectorParser();
            if (!entitySelectorParser.TryParse(reader, throw_on_fail, out EntitySelector entitySelector)) return false;

            if (entitySelector.MaxResults > 1 && LimitedToOne)
            {
                reader.SetCursor(start);
                if (throw_on_fail)
                {
                    if (PlayersOnly) CommandError.SelectorTooManyPlayers().AddWithContext(reader);
                    else CommandError.SelectorTooManyEntities().AddWithContext(reader);
                }
                return false;
            }
            if (entitySelector.IncludesEntities && PlayersOnly && !entitySelector.CurrentEntity)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.SelectorPlayersOnly().AddWithContext(reader);
                return false;
            }

            SetLoopAttributes(reader);
            return true;
        }
    }
}
