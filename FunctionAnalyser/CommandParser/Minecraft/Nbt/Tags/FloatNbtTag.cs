using static CommandParser.Minecraft.Nbt.NbtUtilities;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class FloatNbtTag : INbtTag {
        private readonly float Data;

        public FloatNbtTag(float data) {
            Data = data;
        }

        public string GetName() {
            return "TAG_Float";
        }

        public sbyte GetId() {
            return 5;
        }

        public string ToSnbt() {
            return Data.ToString(NbtNumberFormatInfo) + "f";
        }
    }
}
