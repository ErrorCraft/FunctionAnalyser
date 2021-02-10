using static CommandParser.Minecraft.Nbt.NbtUtilities;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class DoubleNbtTag : INbtTag {
        private readonly double Data;

        public DoubleNbtTag(double data) {
            Data = data;
        }

        public string GetName() {
            return "TAG_Double";
        }

        public sbyte GetId() {
            return 6;
        }

        public string ToSnbt() {
            return Data.ToString(NbtNumberFormatInfo) + "d";
        }
    }
}
