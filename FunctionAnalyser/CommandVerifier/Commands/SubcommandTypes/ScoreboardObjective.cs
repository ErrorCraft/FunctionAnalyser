using CommandVerifier.Commands.Collections;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class ScoreboardObjective : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            int start = reader.Cursor;
            while (!reader.IsEndOfArgument()) reader.Skip();
            string s = reader.Command[start..reader.Cursor];

            if (s.Contains(':'))
            {
                if (ScoreboardCriteria.TryReadNamespacedCriterion(s))
                {
                    SetLoopAttributes(reader);
                    return true;
                }
            } else if (ScoreboardCriteria.TryReadNormalCriterion(s))
            {
                SetLoopAttributes(reader);
                return true;
            }

            if (throw_on_fail) CommandError.UnknownCriterion(s).Add();
            return false;
        }
    }
}
