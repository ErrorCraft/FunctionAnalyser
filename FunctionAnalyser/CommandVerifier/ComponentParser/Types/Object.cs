using CommandVerifier.Commands;
using CommandVerifier.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandVerifier.ComponentParser.Types
{
    class Object : IComponent
    {
        private static readonly string STRING_FORMAT_ERROR = "Expected {0} to be {1}, was {2}";
        private static readonly string INVALID_SCORE_COMPONENT = "A score component needs at least a name and an objective";
        public Dictionary<string, IComponent> Values { get; private set; }
        public Object()
        {
            Values = new Dictionary<string, IComponent>();
        }
        public override string ToString()
        {
            string s = "{";
            foreach (string key in Values.Keys)
            {
                s += "\"" + key + "\":" + Values[key].ToString() + ",";
            }
            return s.TrimEnd(',') + "}";
        }
        public bool Validate(StringReader reader, int start, bool mayThrow)
        {
            if (!ValidateContents(reader, start, mayThrow)) return false;
            if (!ValidateChildren(reader, start, mayThrow)) return false;
            if (!ValidateFormatting(reader, start, mayThrow)) return false;
            if (!ValidateInteractivity(reader, start, mayThrow)) return false;
            return true;
        }

        private bool ValidateContents(StringReader reader, int start, bool mayThrow)
        {
            if (Values.ContainsKey("text"))
            {
                if (!IsText(Values["text"]))
                {
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "text", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }
                return true;
            }
            if (Values.ContainsKey("translate"))
            {
                if (!IsText(Values["translate"]))
                {
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "translate", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }

                // Optional
                if (Values.ContainsKey("with"))
                {
                    if (!(Values["with"] is Array))
                    {
                        if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "with", "an array", "<insert>")).AddWithContext(reader);
                        return false;
                    }
                    if (!Values["with"].Validate(reader, start, mayThrow)) return false;
                }
                return true;
            }   
            if (Values.ContainsKey("score"))
            {
                if (!(Values["score"] is Object score))
                {
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "score", "an object", "<insert>")).AddWithContext(reader);
                    return false;
                }
                return ValidateScore(reader, start, score, mayThrow);
            }
            if (Values.ContainsKey("selector"))
            {
                if (!IsText(Values["selector"]))
                {
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "selector", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }
                return true;
            }
            if (Values.ContainsKey("keybind"))
            {
                if (!IsText(Values["keybind"]))
                {
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "keybind", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }
                return true;
            }
            if (Values.ContainsKey("nbt"))
            {
                if (!IsText(Values["nbt"]))
                {
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "nbt", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }
                return ValidateNbt(reader, start, mayThrow);
            }

            if (mayThrow) CommandError.InvalidChatComponent("Don't know how to turn " + ToString() + " into a component").AddWithContext(reader);
            return false;
        }

        private bool ValidateScore(StringReader reader, int start, Object score, bool mayThrow)
        {
            if (!score.Values.ContainsKey("name") || !score.Values.ContainsKey("objective"))
            {
                reader.SetCursor(start);
                if (mayThrow) CommandError.InvalidChatComponent(INVALID_SCORE_COMPONENT).AddWithContext(reader);
                return false;
            }
            if (!IsText(score.Values["name"]))
            {
                reader.SetCursor(start);
                if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "name", "a string", "<insert>")).AddWithContext(reader);
                return false;
            }
            if (!IsText(score.Values["objective"]))
            {
                reader.SetCursor(start);
                if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "objective", "a string", "<insert>")).AddWithContext(reader);
                return false;
            }
            return true;
        }

        private bool ValidateNbt(StringReader reader, int start, bool mayThrow)
        {
            if (Values.ContainsKey("block"))
            {
                if (!IsText(Values["block"]))
                {
                    reader.SetCursor(start);
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "block", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }
                return true;
            }
            
            if (Values.ContainsKey("entity"))
            {
                if (!IsText(Values["entity"]))
                {
                    reader.SetCursor(start);
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "block", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }
                return true;
            }
            
            if (Values.ContainsKey("storage"))
            {
                if (!IsText(Values["storage"]))
                {
                    reader.SetCursor(start);
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "block", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }

                string id = Values["storage"].ToString();
                if (Values["storage"] is String s)
                {
                    id = s.Value;
                }

                if (!NamespacedId.TryParse(id, false, out _))
                {
                    reader.SetCursor(start);
                    if (mayThrow) CommandError.InvalidNamespacedId().AddWithContext(reader);
                    return false;
                }
                return true;
            }

            reader.SetCursor(start);
            if (mayThrow) CommandError.InvalidChatComponent("Don't know how to turn " + ToString() + " into a component").AddWithContext(reader);
            return false;
        }

        private bool ValidateChildren(StringReader reader, int start, bool mayThrow)
        {
            if (Values.ContainsKey("extra"))
            {
                if (!(Values["extra"] is Array))
                {
                    reader.SetCursor(start);
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "extra", "array", "<insert>")).AddWithContext(reader);
                    return false;
                }
                return Values["extra"].Validate(reader, start, mayThrow);
            }
            return true;
        }

        private bool ValidateFormatting(StringReader reader, int start, bool mayThrow)
        {
            if (!ValidateOptionalText(reader, "color", start, mayThrow)) return false;
            if (!ValidateOptionalText(reader, "font", start, mayThrow)) return false;
            if (!ValidateOptionalText(reader, "bold", start, mayThrow)) return false;
            if (!ValidateOptionalText(reader, "italic", start, mayThrow)) return false;
            if (!ValidateOptionalText(reader, "underlined", start, mayThrow)) return false;
            if (!ValidateOptionalText(reader, "strikethrough", start, mayThrow)) return false;
            if (!ValidateOptionalText(reader, "obfuscated", start, mayThrow)) return false;
            return true;
        }

        private bool ValidateInteractivity(StringReader reader, int start, bool mayThrow)
        {
            if (!ValidateOptionalText(reader, "insertion", start, mayThrow)) return false;

            if (Values.ContainsKey("clickEvent"))
            {
                if (!(Values["clickEvent"] is Object click_event))
                {
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "clickEvent", "an object", "<insert>")).AddWithContext(reader);
                    return false;
                }
                if (ValidateClickEvent(reader, start, click_event, mayThrow)) return false;
            }

            if (Values.ContainsKey("hoverEvent"))
            {
                if (!(Values["hoverEvent"] is Object hover_event))
                {
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "hoverEvent", "an object", "<insert>")).AddWithContext(reader);
                    return false;
                }
                if (ValidateHoverEvent(reader, start, hover_event, mayThrow)) return false;
            }

            return true;
        }

        private bool ValidateClickEvent(StringReader reader, int start, Object click_event, bool mayThrow)
        {
            if (click_event.Values.ContainsKey("action") && !IsText(click_event.Values["action"]))
            {
                reader.SetCursor(start);
                if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "action", "string", "<insert>")).AddWithContext(reader);
                return false;
            }
            if (click_event.Values.ContainsKey("value") && !IsText(click_event.Values["value"]))
            {
                reader.SetCursor(start);
                if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "value", "string", "<insert>")).AddWithContext(reader);
                return false;
            }
            return true;
        }

        private bool ValidateHoverEvent(StringReader reader, int start, Object hover_event, bool mayThrow)
        {
            string key = "";
            if (hover_event.Values.ContainsKey("action"))
            {
                if (!IsText(hover_event.Values["action"]))
                {
                    reader.SetCursor(start);
                    if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "action", "string", "<insert>")).AddWithContext(reader);
                    return false;
                }
                key = hover_event.Values["action"].ToString();
            }
            if (hover_event.Values.ContainsKey("contents"))
            {
                switch (key)
                {
                    case "show_text":
                        return hover_event.Values["contents"].Validate(reader, start, mayThrow);
                    case "show_item":
                        if (!(hover_event.Values["contents"] is Object showItemObject))
                        {
                            reader.SetCursor(start);
                            if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "value", "string", "<insert>")).AddWithContext(reader);
                            return false;
                        }
                        if (!showItemObject.Values.ContainsKey("id"))
                        {
                            reader.SetCursor(start);
                            if (mayThrow) CommandError.InvalidChatComponent("Missing id, expected to find a string").AddWithContext(reader);
                            return false;
                        }
                        if (!IsText(showItemObject.Values["id"]))
                        {
                            reader.SetCursor(start);
                            if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "id", "string", "<insert>")).AddWithContext(reader);
                            return false;
                        }
                        if (showItemObject.Values.ContainsKey("count") && !(showItemObject.Values["count"] is Number))
                        {
                            reader.SetCursor(start);
                            if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "count", "int", "<insert>")).AddWithContext(reader);
                            return false;
                        }
                        if (!ValidateOptionalText(reader, "tag", start, mayThrow)) return false;
                        break;
                    case "show_entity":
                        if (!(hover_event.Values["contents"] is Object showEntityObject))
                        {
                            reader.SetCursor(start);
                            if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, "value", "string", "<insert>")).AddWithContext(reader);
                            return false;
                        }
                        if (!showEntityObject.Values.ContainsKey("type"))
                        {
                            reader.SetCursor(start);
                            if (mayThrow) CommandError.InvalidChatComponent("Missing type, expected to find a string").AddWithContext(reader);
                            return false;
                        }
                        if (!showEntityObject.Values.ContainsKey("id"))
                        {
                            reader.SetCursor(start);
                            if (mayThrow) CommandError.InvalidChatComponent("Missing id, expected to find a string").AddWithContext(reader);
                            return false;
                        }

                        Regex UuidRegex = new Regex("^[0-9a-fA-F]{1,8}-([0-9a-fA-F]{1,4}-){3}[0-9a-fA-F]{1,12}$");
                        if (UuidRegex.IsMatch(showEntityObject.Values["id"].ToString()))
                        {
                            if (mayThrow) CommandError.InvalidChatComponent("Invalid UUID string: " + showEntityObject.Values["id"].ToString()).AddWithContext(reader);
                            return false;
                        }
                        if (showEntityObject.Values.ContainsKey("name") && !showEntityObject.Values["name"].Validate(reader, start, mayThrow)) return false;
                        break;
                }
            }
            return true;
        }

        private bool ValidateOptionalText(StringReader reader, string key, int start, bool mayThrow)
        {
            if (Values.ContainsKey(key) && !IsText(Values[key]))
            {
                reader.SetCursor(start);
                if (mayThrow) CommandError.InvalidChatComponent(string.Format(STRING_FORMAT_ERROR, key, "string", "<insert>")).AddWithContext(reader);
                return false;
            }
            return true;
        }

        private bool IsText(IComponent component)
            => component is String || component is Number || component is Boolean;
    }
}
