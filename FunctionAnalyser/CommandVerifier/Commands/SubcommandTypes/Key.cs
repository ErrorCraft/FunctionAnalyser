using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Key : Subcommand
    {
        [JsonProperty("value", Required = Required.Always)]
        public string Value { get; set; }

        [JsonProperty("check_outside")]
        [DefaultValue(false)]
        public bool CheckOutside { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }
            int start = reader.Cursor;
            if (reader.CanRead(Value.Length))
            {
                if (reader.Read(Value.Length).Equals(Value) && (CheckOutside || reader.IsEndOfArgument()))
                {
                    SetLoopAttributes(reader);
                    return true;
                }
                reader.SetCursor(start);
            }
            if (throw_on_fail) CommandError.IncorrectArgument().AddWithContext(reader);
            return false;
        }
    }
}
