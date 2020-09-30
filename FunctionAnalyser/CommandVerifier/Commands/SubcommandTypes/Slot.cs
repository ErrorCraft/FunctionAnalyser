using System.Collections.Generic;

namespace CommandVerifier.Commands.SubcommandTypes
{
    class Slot : Subcommand
    {
        private static readonly HashSet<string> SLOTS;

        static Slot()
        {
            SLOTS = new HashSet<string>();
            int i;
            for (i = 0; i < 54; i++) SLOTS.Add("container." + i);
            for (i = 0; i < 9; i++) SLOTS.Add("hotbar." + i);
            for (i = 0; i < 27; i++) SLOTS.Add("inventory." + i);
            for (i = 0; i < 27; i++) SLOTS.Add("enderchest." + i);
            for (i = 0; i < 8; i++) SLOTS.Add("villager." + i);
            for (i = 0; i < 15; i++) SLOTS.Add("horse." + i);
            SLOTS.Add("weapon");
            SLOTS.Add("weapon.mainhand");
            SLOTS.Add("weapon.offhand");
            SLOTS.Add("armor.head");
            SLOTS.Add("armor.chest");
            SLOTS.Add("armor.legs");
            SLOTS.Add("armor.feet");
            SLOTS.Add("horse.saddle");
            SLOTS.Add("horse.armor");
            SLOTS.Add("horse.chest");
        }

        public override bool Check(StringReader reader, bool throw_on_fail)
        {
            if (!reader.CanRead() && Optional)
            {
                reader.commandData.EndedOptional = true;
                return true;
            }

            if (!reader.TryReadUnquotedString(out string result)) return false;
            if (!SLOTS.Contains(result))
            {
                if (throw_on_fail) CommandError.UnknownSlot(result).Add();
                return false;
            }

            SetLoopAttributes(reader);
            return true;
        }
    }
}
