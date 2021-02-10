using static CommandParser.Minecraft.Nbt.NbtUtilities;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class IntegerNbtTag : INbtTag {
        private readonly int Data;

        public IntegerNbtTag(int data) {
            Data = data;
        }

        public string GetName() {
            return "TAG_Integer";
        }

        public sbyte GetId() {
            return 3;
        }

        public string ToSnbt() {
            return Data.ToString(NbtNumberFormatInfo);
        }
    }
}
