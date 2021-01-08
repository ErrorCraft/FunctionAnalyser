using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public class ObjectiveCriteria
    {
        private static HashSet<string> CustomCriteriaObsolete = new HashSet<string>();
        private static Dictionary<string, ObjectiveCriterion> NamespacedCriteriaObsolete = new Dictionary<string, ObjectiveCriterion>();
        private static Dictionary<string, ObjectiveCriterion> NormalCriteriaObsolete = new Dictionary<string, ObjectiveCriterion>();
        private readonly HashSet<string> CustomCriteria;
        private readonly Dictionary<string, ObjectiveCriterion> NamespacedCriteria;
        private readonly Dictionary<string, ObjectiveCriterion> NormalCriteria;

        public ObjectiveCriteria(HashSet<string> customCriteria, Dictionary<string, ObjectiveCriterion> namespacedCriteria, Dictionary<string, ObjectiveCriterion> normalCriteria)
        {
            CustomCriteria = customCriteria;
            NamespacedCriteria = namespacedCriteria;
            NormalCriteria = normalCriteria;
        }

        public static void Set(string json)
        {
            JObject jObject = JObject.Parse(json);
            if (jObject.TryGetValue("custom", out JToken customCriteriaJson)) CustomCriteriaObsolete = customCriteriaJson.ToObject<HashSet<string>>();
            if (jObject.TryGetValue("namespaced", out JToken namespacedCriteriaJson)) NamespacedCriteriaObsolete = namespacedCriteriaJson.ToObject<Dictionary<string, ObjectiveCriterion>>();
            if (jObject.TryGetValue("normal", out JToken normalCriteriaJson)) NormalCriteriaObsolete = normalCriteriaJson.ToObject<Dictionary<string, ObjectiveCriterion>>();
        }

        public static bool TryGetNormalCriterion(string criterion, out ObjectiveCriterion result)
        {
            return NormalCriteriaObsolete.TryGetValue(criterion, out result);
        }

        public static bool TryGetNamespacedCriterion(string criterion, out ObjectiveCriterion result)
        {
            return NamespacedCriteriaObsolete.TryGetValue(criterion, out result);
        }

        public static bool ContainsCustomCriterion(string criterion)
        {
            return CustomCriteriaObsolete.Contains(criterion);
        }
    }
}
