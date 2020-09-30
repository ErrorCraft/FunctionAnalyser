namespace CommandVerifier.Commands.SubcommandTypes.Coordinates
{
    class WorldCoordinates
    {
        public static bool TryReadDouble(StringReader reader, bool may_throw)
        {
            int start = reader.Cursor;

            if (!WorldCoordinate.TryReadDouble(reader, may_throw)) return false;
            if (!reader.CanRead() || reader.Peek() != ' ')
            {
                reader.SetCursor(start);
                if (may_throw) CommandError.Vec3CoordinatesIncomplete().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            if (!WorldCoordinate.TryReadDouble(reader, may_throw)) return false;
            if (!reader.CanRead() || reader.Peek() != ' ')
            {
                reader.SetCursor(start);
                if (may_throw) CommandError.Vec3CoordinatesIncomplete().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            if (!WorldCoordinate.TryReadDouble(reader, may_throw)) return false;
            return true;
        }

        public static bool TryReadInt(StringReader reader, bool may_throw)
        {
            int start = reader.Cursor;

            if (!WorldCoordinate.TryReadInt(reader, may_throw)) return false;
            if (!reader.CanRead() || reader.Peek() != ' ')
            {
                reader.SetCursor(start);
                if (may_throw) CommandError.Vec3CoordinatesIncomplete().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            if (!WorldCoordinate.TryReadInt(reader, may_throw)) return false;
            if (!reader.CanRead() || reader.Peek() != ' ')
            {
                reader.SetCursor(start);
                if (may_throw) CommandError.Vec3CoordinatesIncomplete().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            if (!WorldCoordinate.TryReadInt(reader, may_throw)) return false;
            return true;
        }
    }
}
