using CommandVerifier.Commands.Collections;
using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Item : Subcommand
    {
        [JsonProperty("disable_tags")]
        [DefaultValue(false)]
        public bool DisableTags { get; set; }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            int start = reader.Cursor;

            // Read namespaced id
            if (!reader.TryReadNamespacedId(throw_on_fail, false, out Types.NamespacedId id)) return false;

            // Tag
            if (id.IsTag && DisableTags)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.ItemTagsNotAllowed().AddWithContext(reader);
                return false;
            }

            // Verify item (if not a tag)
            if (!id.IsTag)
            {
                if (!id.IsDefaultNamespace() || !Items.Options.Contains(id.Path))
                {
                    if (throw_on_fail) CommandError.UnknownItem(id.ToString()).Add();
                    return false;
                }
            }

            // NBT
            if (reader.CanRead() && reader.Peek() == '{' && !NbtParser.NbtReader.TryParse(reader, throw_on_fail, out _))
                    return false;

            SetLoopAttributes(reader);
            return true;
        }
    }
}
