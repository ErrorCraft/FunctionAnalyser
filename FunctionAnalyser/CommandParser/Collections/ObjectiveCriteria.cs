using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CommandParser.Collections
{
    public static class ObjectiveCriteria
    {
        private static HashSet<string> CustomCriteria = new HashSet<string>();
        private static Dictionary<string, ObjectiveCriterion> NamespacedCriteria = new Dictionary<string, ObjectiveCriterion>();
        private static Dictionary<string, ObjectiveCriterion> NormalCriteria = new Dictionary<string, ObjectiveCriterion>();

        public static void Set(string json)
        {
            JObject jObject = JObject.Parse(json);
            if (jObject.TryGetValue("custom", out JToken customCriteriaJson)) CustomCriteria = customCriteriaJson.ToObject<HashSet<string>>();
            if (jObject.TryGetValue("namespaced", out JToken namespacedCriteriaJson)) NamespacedCriteria = namespacedCriteriaJson.ToObject<Dictionary<string, ObjectiveCriterion>>();
            if (jObject.TryGetValue("normal", out JToken normalCriteriaJson)) NormalCriteria = normalCriteriaJson.ToObject<Dictionary<string, ObjectiveCriterion>>();
        }

        public static bool TryGetNormalCriterion(string criterion, out ObjectiveCriterion result)
        {
            return NormalCriteria.TryGetValue(criterion, out result);
        }

        public static bool TryGetNamespacedCriterion(string criterion, out ObjectiveCriterion result)
        {
            return NamespacedCriteria.TryGetValue(criterion, out result);
        }

        public static bool ContainsCustomCriterion(string criterion)
        {
            return CustomCriteria.Contains(criterion);
        }
    }
}
