using System.Globalization;

namespace CommandParser.Minecraft.Coordinates {
    public struct Angle {
        public float Value { get; }
        public bool IsRelative { get; }

        public Angle(float value, bool isRelative) {
            Value = value;
            IsRelative = isRelative;
        }

        public override string ToString() {
            string value = Value.ToString(NumberFormatInfo.InvariantInfo);
            if (IsRelative) return "~" + value;
            else return value;
        }
    }
}
