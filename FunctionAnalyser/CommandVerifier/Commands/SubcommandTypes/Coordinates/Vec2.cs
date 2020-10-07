﻿namespace CommandVerifier.Commands.SubcommandTypes.Coordinates
{
    class Vec2 : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            int start = reader.Cursor;

            if (!WorldCoordinate.TryReadDouble(reader, throw_on_fail)) return false;
            if (!reader.CanRead() || reader.Peek() != ' ')
            {
                reader.SetCursor(start);
                if (throw_on_fail) CommandError.Vec2CoordinatesIncomplete().AddWithContext(reader);
                return false;
            }
            reader.Skip();

            if (!WorldCoordinate.TryReadDouble(reader, throw_on_fail)) return false;
            SetLoopAttributes(reader);
            return true;
        }
    }
}
