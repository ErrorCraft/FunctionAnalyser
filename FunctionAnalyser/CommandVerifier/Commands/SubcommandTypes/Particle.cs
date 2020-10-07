using CommandVerifier.Commands.Collections;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Particle : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            if (!reader.TryReadNamespacedId(throw_on_fail, true, out Types.NamespacedId particle)) return false;
            if (!particle.IsDefaultNamespace() || !Particles.Options.ContainsKey(particle.Path))
            {
                if (throw_on_fail) CommandError.UnknownParticle(particle).Add();
                return false;
            }

            if (Particles.Options[particle.Path].HasParameters)
            {
                int length = Particles.Options[particle.Path].Parameters.Length;
                if (reader.CanRead()) reader.Skip();
                for (int i = 0; i < length; i++)
                {
                    if (!Particles.Options[particle.Path].Parameters[i].Check(reader, throw_on_fail)) return false;
                    if (!reader.IsEndOfArgument())
                    {
                        if (throw_on_fail) CommandError.ExpectedArgumentSeparator().AddWithContext(reader);
                        return false;
                    }
                    if (i + 1 < length && reader.CanRead()) reader.Skip();
                }
            }

            SetLoopAttributes(reader);
            return true;
        }
    }
}
