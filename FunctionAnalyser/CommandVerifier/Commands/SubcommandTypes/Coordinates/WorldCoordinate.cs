namespace CommandVerifier.Commands.SubcommandTypes.Coordinates
{
    class WorldCoordinate
    {
        public static bool TryReadDouble(StringReader reader, bool may_throw)
        {
            if (!reader.CanRead())
            {
                if (may_throw) CommandError.ExpectedDouble().AddWithContext(reader);
                return false;
            }
            if (reader.Peek() == '^')
            {
                if (may_throw) CommandError.MixedCoordinateType().AddWithContext(reader);
                return false;
            }
            IsRelative(reader);
            if (!reader.IsEndOfArgument())
            {
                if (!reader.TryReadDouble(may_throw, out _)) return false;
            }
            return true;
        }

        public static bool TryReadInt(StringReader reader, bool may_throw)
        {
            if (reader.CanRead() && reader.Peek() == '^')
            {
                if (may_throw) CommandError.MixedCoordinateType().AddWithContext(reader);
                return false;
            }
            if (!reader.CanRead())
            {
                if (may_throw) CommandError.ExpectedInteger().AddWithContext(reader);
                return false;
            }
            bool is_relative = IsRelative(reader);
            if (!reader.IsEndOfArgument())
            {
                if (is_relative)
                {
                    if (!reader.TryReadDouble(may_throw, out _)) return false;
                }
                else if (!reader.TryReadInt(may_throw, out _)) return false;
            }
            return true;
        }

        public static bool IsRelative(StringReader reader)
        {
            if (reader.Peek() == '~')
            {
                reader.Skip();
                return true;
            }
            return false;
        }
    }
}
