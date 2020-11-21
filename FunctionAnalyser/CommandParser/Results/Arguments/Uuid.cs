namespace CommandParser.Results.Arguments
{
    public class Uuid
    {
        public const char VALUE_SEPARATOR = '-';
        private readonly short[] Shorts;

        public Uuid(short s1, short s2, short s3, short s4, short s5, short s6, short s7, short s8)
        {
            Shorts = new short[8] { s1, s2, s3, s4, s5, s6, s7, s8 };
        }

        public override string ToString()
        {
            return $"{Shorts[0]:x4}{Shorts[1]:x4}{VALUE_SEPARATOR}{Shorts[2]:x4}{VALUE_SEPARATOR}{Shorts[3]:x4}{VALUE_SEPARATOR}{Shorts[4]:x4}{VALUE_SEPARATOR}{Shorts[5]:x4}{Shorts[6]:x4}{Shorts[7]:x4}";
        }
    }
}
