namespace CommandParser.Minecraft.Nbt.Tags {
    public class StringNbtTag : INbtTag {
        private readonly string Data;

        public StringNbtTag(string data) {
            Data = data;
        }

        public string GetName() {
            return "TAG_String";
        }

        public sbyte GetId() {
            return 8;
        }

        public string ToSnbt() {
            // Currently doesn't unescape
            return Data;
        }
    }
}
