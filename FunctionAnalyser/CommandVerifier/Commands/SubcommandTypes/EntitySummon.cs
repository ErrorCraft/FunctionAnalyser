﻿using CommandVerifier.Commands.Collections;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class EntitySummon : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }

            // Read namespaced id
            if (!reader.TryReadNamespacedId(throw_on_fail, true, out Types.NamespacedId id)) return false;

            if (!id.IsDefaultNamespace() || !Entities.Options.Contains(id.Path))
            {
                if (throw_on_fail) CommandError.UnknownEntity(id.ToString()).Add();
                return false;
            }

            SetLoopAttributes(reader);
            return true;
        }
    }
}
