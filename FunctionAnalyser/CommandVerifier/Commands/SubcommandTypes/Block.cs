using CommandVerifier.Commands.Collections;
using Newtonsoft.Json;
using System.ComponentModel;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Block : Subcommand
    {
        [JsonProperty("disable_tags")]
        [DefaultValue(false)]
        public bool DisableTags { get; set; }
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }

            int start = reader.Cursor;

            // Read namespaced id
            if (!reader.TryReadNamespacedId(throw_on_fail, false, out Types.NamespacedId id)) return false;

            // Tag
            if (id.IsTag && DisableTags)
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.BlockTagsNotAllowed().AddWithContext(reader);
                return false;
            }

            if (!id.IsTag)
            {
                if (!id.IsDefaultNamespace() || !Blocks.Options.ContainsKey(id.Path))
                {
                    if (throw_on_fail) CommandError.UnknownBlock(id.ToString()).AddWithContext(reader);
                    return false;
                }
            }

            // Block states
            if (reader.CanRead() && reader.Peek() == '[' && !TryReadBlockStates(reader, id, throw_on_fail))
                return false;

            // NBT
            if (reader.CanRead() && reader.Peek() == '{' && !NbtParser.NbtReader.TryParse(reader, throw_on_fail, out _))
                return false;

            SetLoopAttributes(reader);
            return true;
        }

        private bool TryReadBlockStates(StringReader reader, Types.NamespacedId block, bool may_throw)
        {
            if (!reader.Expect('[', may_throw)) return false;
            reader.SkipWhitespace();

            while (reader.CanRead() && reader.Peek() != ']')
            {
                reader.SkipWhitespace();
                int start = reader.Cursor;
                if (!reader.TryReadString(may_throw, out string key)) return false;

                // Verify if not a block tag
                if (!block.IsTag && !Blocks.Options[block.Path].ContainsKey(key))
                {
                    reader.SetCursor(start);
                    if (may_throw) CommandError.UnknownBlockProperty(block.ToString(), key).AddWithContext(reader);
                    return false;
                }

                reader.SkipWhitespace();
                start = reader.Cursor;

                if (!reader.CanRead() || reader.Peek() != '=')
                {
                    if (may_throw) CommandError.ExpectedValueForPropertyOnBlock(key, block.ToString()).AddWithContext(reader);
                    return false;
                }
                reader.Skip();

                reader.SkipWhitespace();
                if (!reader.TryReadString(may_throw, out string value)) return false;

                // Verify if not a block tag
                if (!block.IsTag && !Blocks.Options[block.Path][key].Contains(value))
                {
                    reader.SetCursor(start);
                    if (may_throw) CommandError.UnknownBlockPropertyValue(block.ToString(), key, value).AddWithContext(reader);
                    return false;
                }

                reader.SkipWhitespace();
                if (reader.CanRead() && reader.Peek() == ',')
                {
                    reader.Skip();
                    continue;
                }
                break;
            }

            if (!reader.CanRead() || reader.Peek() != ']')
            {
                if (may_throw) CommandError.UnclosedBlockStateProperties().AddWithContext(reader);
                return false;
            }
            reader.Skip();
            return true;
        }
    }
}
