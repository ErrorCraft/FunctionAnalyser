namespace CommandParser.Results.Arguments
{
    public class ScoreHolder
    {
        public string Name { get; }
        public EntitySelector Selector { get; }

        public ScoreHolder(string name, EntitySelector selector)
        {
            Name = name;
            Selector = selector;
        }

        public override string ToString()
        {
            if (Selector == null)
            {
                return Name;
            } else
            {
                return Selector.ToString();
            }
        }
    }
}
