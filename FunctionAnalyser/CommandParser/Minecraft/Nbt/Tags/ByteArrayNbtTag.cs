using System.Collections.Generic;
using System.Text;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class ByteArrayNbtTag : INbtCollectionTag {
        private readonly List<ByteNbtTag> Data;

        public ByteArrayNbtTag() {
            Data = new List<ByteNbtTag>();
        }

        public string GetName() {
            return "TAG_Byte_Array";
        }

        public string ToSnbt() {
            StringBuilder stringBuilder = new StringBuilder("[B;");
            stringBuilder.Append(NbtUtilities.Join(", ", Data, n => n.ToSnbt()));
            return stringBuilder.Append(']').ToString();
        }

        public sbyte GetId() {
            return 7;
        }

        public bool Add(INbtTag tag) {
            if (tag is ByteNbtTag b) {
                Data.Add(b);
                return true;
            }
            return false;
        }
    }
}
