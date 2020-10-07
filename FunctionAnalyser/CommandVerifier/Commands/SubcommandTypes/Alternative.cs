namespace CommandVerifier.Commands.SubcommandTypes
{
    class Alternative : SubcommandCollection
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            // Store cursor before reading
            int start = reader.Cursor;

            // Store if a previous requirement was passed
            bool previous_pass = reader.Data.PassedFirstRequirement;

            // Check all
            reader.Data.PassedFirstRequirement = false;
            for (int i = 0; i < Values.Length; i++)
            {
                // Check passed
                if (Values[i].Check(reader, false))
                {
                    SetLoopAttributes(reader);
                    return true;
                }

                // Passed first requirement
                if (reader.Data.PassedFirstRequirement) return false;

                // Set cursor back
                reader.SetCursor(start);
            }

            // Store requirement passed back
            reader.Data.PassedFirstRequirement = previous_pass;

            // Run first again, but allow errors to be logged
            if (Values.Length > 0 && throw_on_fail) Values[0].Check(reader, true);
            return false;
        }
    }
}
