namespace CommandVerifier.Commands.SubcommandTypes.Coordinates
{
    class LocalCoordinates
    {
        public static bool TryRead(StringReader reader, bool may_throw)
        {
            int start = reader.Cursor;

            if (!TryReadDouble(reader, may_throw)) return false;
            if (!reader.CanRead() || reader.Peek() != ' ')
            {
                reader.SetCursor(start);
                if (may_throw) CommandError.Vec3CoordinatesIncomplete().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            if (!TryReadDouble(reader, may_throw)) return false;
            if (!reader.CanRead() || reader.Peek() != ' ')
            {
                reader.SetCursor(start);
                if (may_throw) CommandError.Vec3CoordinatesIncomplete().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            if (!TryReadDouble(reader, may_throw)) return false;
            return true;
        }

        public static bool TryReadDouble(StringReader reader, bool may_throw)
        {
            if (!reader.CanRead())
            {
                if (may_throw) CommandError.ExpectedDouble().AddWithContext(reader);
                return false;
            }
            if (reader.Peek() != '^')
            {
                if (may_throw) CommandError.MixedCoordinateType().AddWithContext(reader);
                return false;
            }
            reader.Skip();
            if (!reader.IsEndOfArgument() && !reader.TryReadDouble(may_throw, out _)) return false;
            return true;
        }
    }
}
