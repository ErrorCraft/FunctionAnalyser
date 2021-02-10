using System.Collections.Generic;
using System.Text;

namespace CommandParser.Minecraft.Nbt.Tags {
    public class LongArrayNbtTag : INbtCollectionTag {
        private readonly List<LongNbtTag> Data;

        public LongArrayNbtTag() {
            Data = new List<LongNbtTag>();
        }

        public string GetName() {
            return "TAG_Long_Array";
        }

        public string ToSnbt() {
            StringBuilder stringBuilder = new StringBuilder("[L;");
            stringBuilder.Append(NbtUtilities.Join(", ", Data, n => n.ToSnbt()));
            return stringBuilder.Append(']').ToString();
        }

        public sbyte GetId() {
            return 12;
        }

        public bool Add(INbtTag tag) {
            if (tag is LongNbtTag l) {
                Data.Add(l);
                return true;
            }
            return false;
        }
    }
}
