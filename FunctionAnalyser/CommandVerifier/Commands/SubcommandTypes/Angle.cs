using CommandVerifier.Commands.SubcommandTypes.Coordinates;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Angle : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            if (!reader.CanRead())
            {
                if (throw_on_fail) CommandError.AngleIncomplete().AddWithContext(reader);
                return false;
            }
            WorldCoordinate.IsRelative(reader);
            if (!reader.TryReadFloat(throw_on_fail, out _)) return false;

            SetLoopAttributes(reader);
            return true;
        }
    }
}
