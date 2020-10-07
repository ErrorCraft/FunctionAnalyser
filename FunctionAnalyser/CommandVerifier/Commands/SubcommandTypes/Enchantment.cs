﻿using CommandVerifier.Commands.Collections;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Enchantment : Subcommand
    {
        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.Data.EndedOptional = true;
                return true;
            }

            // Read namespaced id
            if (!reader.TryReadNamespacedId(throw_on_fail, true, out Types.NamespacedId id)) return false;

            // Verify enchantment
            if (!id.IsDefaultNamespace() || !Enchantments.Options.Contains(id.Path))
            {
                if (throw_on_fail) CommandError.UnknownEnchantment(id.ToString()).Add();
                return false;
            }

            SetLoopAttributes(reader);
            return true;
        }
    }
}
