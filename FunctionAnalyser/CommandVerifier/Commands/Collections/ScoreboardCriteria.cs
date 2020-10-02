using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommandVerifier.Commands.Collections
{
    public class ScoreboardCriteria
    {
        [JsonProperty("custom_criteria")]
        public static HashSet<string> CustomCriteria { get; set; }

        [JsonProperty("namespaced")]
        public static Dictionary<string, ScoreboardCriterion> NamespacedCriteria { get; private set; }

        [JsonProperty("normal")]
        public static Dictionary<string, ScoreboardCriterion> NormalCriteria { get; private set; }

        static ScoreboardCriteria()
        {
            CustomCriteria = new HashSet<string>();
            NamespacedCriteria = new Dictionary<string, ScoreboardCriterion>();
            NormalCriteria = new Dictionary<string, ScoreboardCriterion>();
        }

        public static bool TryReadNormalCriterion(string input)
        {
            string[] values = input.Split('.');
            if (!NormalCriteria.ContainsKey(values[0])) return false;

            if (values.Length == 1) return NormalCriteria[values[0]].TryRead("");
            if (values.Length > 2 || NormalCriteria[values[0]].From == ScoreboardCriterion.FromType.None) return false;
            return NormalCriteria[values[0]].TryRead(values[1]);
        }

        public static bool TryReadNamespacedCriterion(string input)
        {
            string[] values = input.Split(':');
            if (values.Length != 2) return false;

            if (values[0].StartsWith("minecraft.")) values[0] = values[0].Substring(10);
            if (!NamespacedCriteria.ContainsKey(values[0])) return false;
            if (values[1].StartsWith("minecraft.")) values[1] = values[1].Substring(10);
            return NamespacedCriteria[values[0]].TryRead(values[1]);
        }

        public static void SetOptions(string json)
        {
            JsonConvert.DeserializeObject<ScoreboardCriteria>(json, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate });
        }
    }
}
