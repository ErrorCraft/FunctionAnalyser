namespace CommandParser.Results.Arguments
{
    public class ObjectiveCriterion
    {
        public string Criterion { get; }

        public ObjectiveCriterion(string criterion)
        {
            Criterion = criterion;
        }

        public override string ToString()
        {
            return Criterion;
        }
    }
}
