using Newtonsoft.Json;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Character : Subcommand
    {
        [JsonProperty("value", Required = Required.Always)]
        public char Value { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }
            if (reader.Expect(Value, throw_on_fail))
            {
                SetLoopAttributes(reader);
                return true;
            }
            return false;
        }
    }
}
