using CommandParser.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FunctionAnalyser.Builders.Collections
{
    public class ObjectiveCriteriaBuilder
    {
        [JsonProperty("parent")]
        private readonly string Parent;
        [JsonProperty("custom")]
        private readonly HashSet<string> CustomCriteria;
        [JsonProperty("namespaced")]
        private readonly Dictionary<string, ObjectiveCriterion> NamespacedCriteria;
        [JsonProperty("normal")]
        private readonly Dictionary<string, ObjectiveCriterion> NormalCriteria;

        public ObjectiveCriteria Build(Dictionary<string, ObjectiveCriteriaBuilder> resources)
        {
            HashSet<string> custom = new HashSet<string>(CustomCriteria);
            Dictionary<string, ObjectiveCriterion> namespaced = new Dictionary<string, ObjectiveCriterion>(NamespacedCriteria);
            Dictionary<string, ObjectiveCriterion> normal = new Dictionary<string, ObjectiveCriterion>(NormalCriteria);
            ObjectiveCriteriaBuilder builder = this;
            while (builder.Parent != null)
            {
                builder = resources[builder.Parent];
                foreach (string s in builder.CustomCriteria) custom.Add(s);
                foreach (KeyValuePair<string, ObjectiveCriterion> pair in builder.NamespacedCriteria) namespaced.Add(pair.Key, pair.Value);
                foreach (KeyValuePair<string, ObjectiveCriterion> pair in builder.NormalCriteria) normal.Add(pair.Key, pair.Value);
            }
            return new ObjectiveCriteria(custom, namespaced, normal);
        }
    }
}
