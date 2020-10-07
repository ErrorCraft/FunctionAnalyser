using CommandVerifier.Commands.SubcommandTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Coordinates = CommandVerifier.Commands.SubcommandTypes.Coordinates;

namespace CommandVerifier.Converters
{
    class SubcommandConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Subcommand);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                JObject jo = JObject.Load(reader);
                if (!jo.HasValues) return null;
                if (jo["type"] == null) throw new Exception("'type' was not found at " + reader.Path);
                return GetSubcommand(jo, serializer);
            }
            JArray ja = JArray.Load(reader);
            if (!ja.HasValues) return null;

            List<Subcommand> subcommands = new List<Subcommand>();
            foreach (JObject jo in ja)
            {
                if (!jo.HasValues) continue;
                if (jo["type"] == null) throw new Exception("'type' was not found at " + reader.Path);
                subcommands.Add(GetSubcommand(jo, serializer));
            }
            return subcommands.ToArray();
        }

        private Subcommand GetSubcommand(JObject jObject, JsonSerializer serializer) => (jObject["type"].Value<string>()) switch
        {
            "key" => jObject.ToObject<Key>(serializer),
            "requirements" => jObject.ToObject<Requirements>(serializer),
            "alternative" => jObject.ToObject<Alternative>(serializer),
            "boolean" => jObject.ToObject<Commands.SubcommandTypes.Boolean>(serializer),
            "integer" => jObject.ToObject<Integer>(serializer),
            "float" => jObject.ToObject<Float>(serializer),
            "double" => jObject.ToObject<Commands.SubcommandTypes.Double>(serializer),
            "string" => jObject.ToObject<Commands.SubcommandTypes.String>(serializer),
            "uuid" => jObject.ToObject<Uuid>(serializer),
            "swizzle" => jObject.ToObject<Swizzle>(serializer),
            "entity" => jObject.ToObject<Entity>(serializer),
            "character" => jObject.ToObject<Character>(serializer),
            "namespaced_id" => jObject.ToObject<NamespacedId>(serializer),
            "nbt" => jObject.ToObject<NbtTag>(serializer),
            "nbt_path" => jObject.ToObject<NbtPath>(serializer),
            "integer_range" => jObject.ToObject<IntegerRange>(serializer),
            "float_range" => jObject.ToObject<FloatRange>(serializer),
            "double_range" => jObject.ToObject<DoubleRange>(serializer),
            "score_holder" => jObject.ToObject<ScoreHolder>(serializer),
            "item" => jObject.ToObject<Item>(serializer),
            "block" => jObject.ToObject<Block>(serializer),
            "time" => jObject.ToObject<Time>(serializer),
            "vec3" => jObject.ToObject<Coordinates::Vec3>(serializer),
            "vec2" => jObject.ToObject<Coordinates::Vec2>(serializer),
            "rotation" => jObject.ToObject<Coordinates::Rotation>(serializer),
            "block_pos" => jObject.ToObject<Coordinates::BlockPos>(serializer),
            "column_pos" => jObject.ToObject<Coordinates::ColumnPos>(serializer),
            "colour" => jObject.ToObject<Colour>(serializer),
            "angle" => jObject.ToObject<Angle>(serializer),
            "item_slot" => jObject.ToObject<Slot>(serializer),
            "particle" => jObject.ToObject<Particle>(serializer),
            "scoreboard_criteria" => jObject.ToObject<ScoreboardObjective>(serializer),
            "scoreboard_slot" => jObject.ToObject<ScoreboardSlot>(serializer),
            "entity_summon" => jObject.ToObject<EntitySummon>(serializer),
            "effect" => jObject.ToObject<Effect>(serializer),
            "enchantment" => jObject.ToObject<Enchantment>(serializer),
            "message" => jObject.ToObject<Message>(serializer),
            "objective" => jObject.ToObject<Objective>(serializer),
            "component" => jObject.ToObject<Component>(serializer),
            _ => throw new Exception("Type '" + jObject["type"].Value<string>() + "' is not a valid subcommand type"),
        };

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
