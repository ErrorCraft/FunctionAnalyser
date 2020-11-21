namespace CommandParser.Results.Arguments
{
    public class Time
    {
        public int Value { get; }

        public Time(int time)
        {
            Value = time;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
