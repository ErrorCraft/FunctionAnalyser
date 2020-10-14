using CommandVerifier.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.ComponentParser.ComponentTypes
{
    class NamespacedId : Component
    {
        public override bool Validate(JsonTypes.Object obj, string key, StringReader reader, int start, bool mayThrow)
        {
            if (!IsText(obj.Values[key]))
            {
                reader.SetCursor(start);
                if (mayThrow) ComponentError.StringFormatError(key, JsonTypes.String.Name, obj.Values[key].GetName()).AddWithContext(reader);
                return false;
            }

            if (!Types.NamespacedId.TryParse(obj.Values[key].ToString(), false, out _))
            {
                reader.SetCursor(start);
                if (mayThrow) CommandError.InvalidNamespacedId().AddWithContext(reader);
                return false;
            }
            return true;
        }
    }
}
