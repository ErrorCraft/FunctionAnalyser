using CommandVerifier.Commands.Collections;
using System.Text.RegularExpressions;

namespace CommandVerifier.Commands.SubcommandTypes.Selector
{
    class EntitySelectorParser
    {
        private static readonly Regex UuidRegex = new Regex("^[0-9a-fA-F]{1,8}-([0-9a-fA-F]{1,4}-){3}[0-9a-fA-F]{1,12}$");
        private readonly EntitySelector entitySelector;

        public EntitySelectorParser()
        {
            entitySelector = new EntitySelector();
        }

        public bool TryParse(StringReader reader, bool may_throw, out EntitySelector result)
        {
            result = entitySelector;

            if (reader.CanRead() && reader.Peek() == '@')
            {
                reader.Skip();
                if (CheckSelector(reader, may_throw)) return true;
            }
            else if (CheckNameOrUUID(reader, may_throw)) return true;
            return false;
        }

        private bool CheckSelector(StringReader reader, bool may_throw)
        {
            if (!reader.CanRead())
            {
                if (may_throw) CommandError.MissingSelectorType().AddWithContext(reader);
                return false;
            }

            int start = reader.Cursor;
            char c = reader.Read();
            switch (c)
            {
                case 'p':
                    entitySelector.MaxResults = 1;
                    entitySelector.IncludesEntities = false;
                    break;
                case 'a':
                    entitySelector.MaxResults = int.MaxValue;
                    entitySelector.IncludesEntities = false;
                    break;
                case 'r':
                    entitySelector.MaxResults = 1;
                    entitySelector.IncludesEntities = false;
                    break;
                case 's':
                    entitySelector.MaxResults = 1;
                    entitySelector.IncludesEntities = true;
                    entitySelector.CurrentEntity = true;
                    break;
                case 'e':
                    entitySelector.MaxResults = int.MaxValue;
                    entitySelector.IncludesEntities = true;
                    reader.Information.EntitySelectors++;
                    break;
                default:
                    reader.SetCursor(start);
                    if (may_throw) CommandError.UnknownSelectorType("@" + c).AddWithContext(reader);
                    return false;
            }

            if (reader.CanRead() && reader.Peek() == '[')
            {
                reader.Skip();
                if (!ParseOptions(reader, may_throw)) return false;
            }
            return true;
        }

        private bool CheckNameOrUUID(StringReader reader, bool may_throw)
        {
            int start = reader.Cursor;
            if (reader.TryReadString(may_throw, out string result))
            {
                // UUID
                if (UuidRegex.IsMatch(result))
                {
                    entitySelector.IncludesEntities = true;
                }
                else
                {
                    // Player name
                    if (string.IsNullOrEmpty(result) || result.Length > 16)
                    {
                        reader.SetCursor(start);
                        if (may_throw) CommandError.InvalidNameOrUuid().AddWithContext(reader);
                        return false;
                    }
                    entitySelector.IncludesEntities = false;
                }
                entitySelector.MaxResults = 1;
                return true;
            }
            return false;
        }

        private bool ParseOptions(StringReader reader, bool may_throw)
        {
            reader.SkipWhitespace();
            while (reader.CanRead() && reader.Peek() != ']')
            {
                reader.SkipWhitespace();
                if (reader.TryReadString(may_throw, out string s))
                {
                    if (!EntitySelectorOptions.TryGet(s, reader, may_throw, out EntitySelectorOption option)) return false;
                    reader.SkipWhitespace();
                    if (!reader.CanRead() || reader.Peek() != '=')
                    {
                        if (may_throw) CommandError.ExpectedValueForEntityOption(s).AddWithContext(reader);
                        return false;
                    }
                    reader.Skip();
                    reader.SkipWhitespace();
                    if (!option.Handle(reader, may_throw, entitySelector)) return false;
                    reader.SkipWhitespace();
                    if (!reader.CanRead()) break;
                    if (reader.Peek() == ',')
                    {
                        reader.Skip();
                        continue;
                    }
                    if (reader.Peek() == ']') break;

                    if (may_throw) CommandError.ExpectedEndOfOptions().AddWithContext(reader);
                    return false;
                }
                else return false;
            }

            if (!reader.CanRead())
            {
                if (may_throw) CommandError.ExpectedEndOfOptions().AddWithContext(reader);
                return false;
            }

            reader.Skip();
            return true;
        }
    }
}
