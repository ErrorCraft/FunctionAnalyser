using static CommandParser.Minecraft.Nbt.NbtUtilities;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class LongNbtTag : INbtTag {
        private readonly long Data;

        public LongNbtTag(long data) {
            Data = data;
        }

        public string GetName() {
            return "TAG_Long";
        }

        public sbyte GetId() {
            return 4;
        }

        public string ToSnbt() {
            return Data.ToString(NbtNumberFormatInfo) + "L";
        }
    }
}
