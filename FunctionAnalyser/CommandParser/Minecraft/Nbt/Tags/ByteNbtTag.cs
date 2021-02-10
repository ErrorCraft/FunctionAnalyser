using static CommandParser.Minecraft.Nbt.NbtUtilities;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class ByteNbtTag : INbtTag {
        public static readonly ByteNbtTag ZERO = new ByteNbtTag(0);
        public static readonly ByteNbtTag ONE = new ByteNbtTag(1);

        private readonly sbyte Data;

        public ByteNbtTag(sbyte data) {
            Data = data;
        }

        public string GetName() {
            return "TAG_Byte";
        }

        public sbyte GetId() {
            return 1;
        }

        public string ToSnbt() {
            return Data.ToString(NbtNumberFormatInfo) + "b";
        }
    }
}
