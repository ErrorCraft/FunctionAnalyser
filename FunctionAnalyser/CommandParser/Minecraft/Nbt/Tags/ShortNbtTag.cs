using static CommandParser.Minecraft.Nbt.NbtUtilities;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class ShortNbtTag : INbtTag {
        private readonly short Data;

        public ShortNbtTag(short data) {
            Data = data;
        }

        public string GetName() {
            return "TAG_Short";
        }

        public sbyte GetId() {
            return 2;
        }

        public string ToSnbt() {
            return Data.ToString(NbtNumberFormatInfo) + "s";
        }
    }
}
