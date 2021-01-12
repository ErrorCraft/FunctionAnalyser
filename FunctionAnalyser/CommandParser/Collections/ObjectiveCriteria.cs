using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class ObjectiveCriteria
    {
        private readonly HashSet<string> CustomCriteria;
        private readonly Dictionary<string, ObjectiveCriterion> NamespacedCriteria;
        private readonly Dictionary<string, ObjectiveCriterion> NormalCriteria;

        public ObjectiveCriteria() : this(new HashSet<string>(), new Dictionary<string, ObjectiveCriterion>(), new Dictionary<string, ObjectiveCriterion>()) { }

        public ObjectiveCriteria(HashSet<string> customCriteria, Dictionary<string, ObjectiveCriterion> namespacedCriteria, Dictionary<string, ObjectiveCriterion> normalCriteria)
        {
            CustomCriteria = customCriteria;
            NamespacedCriteria = namespacedCriteria;
            NormalCriteria = normalCriteria;
        }

        public bool TryGetNormalCriterion(string criterion, out ObjectiveCriterion result)
        {
            return NormalCriteria.TryGetValue(criterion, out result);
        }

        public bool TryGetNamespacedCriterion(string criterion, out ObjectiveCriterion result)
        {
            return NamespacedCriteria.TryGetValue(criterion, out result);
        }

        public bool ContainsCustomCriterion(string criterion)
        {
            return CustomCriteria.Contains(criterion);
        }
    }
}
