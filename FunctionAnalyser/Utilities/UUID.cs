using System.Globalization;

namespace Utilities {
    public class UUID {
        private const char VALUE_SEPARATOR = '-';
        private readonly short[] Shorts;

        public UUID(short s1, short s2, short s3, short s4, short s5, short s6, short s7, short s8) {
            Shorts = new short[8] { s1, s2, s3, s4, s5, s6, s7, s8 };
        }

        public override string ToString() {
            return $"{Shorts[0]:x4}{Shorts[1]:x4}{VALUE_SEPARATOR}{Shorts[2]:x4}{VALUE_SEPARATOR}{Shorts[3]:x4}{VALUE_SEPARATOR}{Shorts[4]:x4}{VALUE_SEPARATOR}{Shorts[5]:x4}{Shorts[6]:x4}{Shorts[7]:x4}";
        }

        public static bool TryParse(string uuid, out UUID result) {
            result = default;

            string[] splitUUID = uuid.Split(VALUE_SEPARATOR);
            if (splitUUID.Length != 5) return false;

            short[] shorts = new short[8];

            // X-x-x-x-x
            short[] temp = GetShorts(splitUUID[0], 2);
            if (temp == null) return false;
            else {
                shorts[0] = temp[0];
                shorts[1] = temp[1];
            }

            // x-X-X-X-x
            for (int i = 0; i < 3; i++) {
                if ((temp = GetShorts(splitUUID[i + 1], 1)) == null) return false;
                else {
                    shorts[2 + i] = temp[0];
                }
            }

            // x-x-x-x-X
            if ((temp = GetShorts(splitUUID[4], 3)) == null) return false;
            else {
                shorts[5] = temp[0];
                shorts[6] = temp[1];
                shorts[7] = temp[2];
            }

            result = new UUID(shorts[0], shorts[1], shorts[2], shorts[3], shorts[4], shorts[5], shorts[6], shorts[7]);
            return true;
        }

        private static short[] GetShorts(string part, int length) {
            int actualLength = length * 4;
            int partLength = part.Length;
            if (!IsValidUUIDPart(part) || partLength > actualLength || partLength == 0) {
                return null;
            }
            string actualPart = part;

            for (int i = actualLength - actualPart.Length; i > 0; i--) {
                actualPart = actualPart.Insert(0, "0");
            }

            short[] values = new short[length];
            for (int i = length - 1; i >= 0; i--) {
                if (!short.TryParse(actualPart.Substring(4 * i, 4), NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo, out values[i])) return null;
            }
            return values;
        }

        private static bool IsValidUUIDPart(string s) {
            int length = s.Length;
            for (int i = 0; i < length; i++) {
                if (!IsDigit(s[i])) return false;
            }
            return true;
        }

        private static bool IsDigit(char c) {
            return c >= '0' && c <= '9' ||
                   c >= 'a' && c <= 'f' ||
                   c >= 'A' && c <= 'F';
        }
    }
}
