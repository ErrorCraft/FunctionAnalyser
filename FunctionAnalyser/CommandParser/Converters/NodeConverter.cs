using CommandParser.Arguments;
using static CommandParser.Providers.NodeBuilder;
using CommandParser.Tree;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CommandParser.Converters
{
    public class NodeConverter : AbstractJsonConverter<ArgumentNode>
    {
        protected override ArgumentNode Create(JObject jObject, JsonSerializer serializer)
        {
            return GetArgument(jObject, "", serializer);
        }

        public static LiteralNode GetLiteral(JToken jToken, string name, JsonSerializer serializer)
        {
            LiteralNode literalNode = Literal(name, false, null);
            serializer.Populate(jToken.CreateReader(), literalNode);
            return literalNode;
        }

        public static ArgumentNode GetArgument(JToken jToken, string name, JsonSerializer serializer)
        {
            string parser = jToken["parser"].Value<string>();

            ArgumentNode argumentNode = parser switch
            {
                "boolean" => Argument(name, new BooleanArgument(), false, null),
                "integer" => Argument(name, new IntegerArgument(), false, null),
                "long" => Argument(name, new LongArgument(), false, null),
                "float" => Argument(name, new FloatArgument(), false, null),
                "double" => Argument(name, new DoubleArgument(), false, null),
                "string" => Argument(name, new StringArgument(), false, null),
                "uuid" => Argument(name, new UuidArgument(), false, null),
                "swizzle" => Argument(name, new SwizzleArgument(), false, null),
                "entity" => Argument(name, new EntityArgument(), false, null),
                "resource_location" => Argument(name, new ResourceLocationArgument(), false, null), // IS REPLACED, SOME NEED SPECIAL TYPES
                "compound_tag" => Argument(name, new CompoundTagArgument(), false, null),
                "nbt_tag" => Argument(name, new NbtTagArgument(), false, null),
                "nbt_path" => Argument(name, new NbtPathArgument(), false, null),
                "integer_range" => Argument(name, new IntegerRangeArgument(), false, null),
                "float_range" => Argument(name, new FloatRangeArgument(), false, null),
                "double_range" => Argument(name, new DoubleRangeArgument(), false, null),
                "item" => Argument(name, new ItemArgument(), false, null),
                "block" => Argument(name, new BlockArgument(), false, null),
                "time" => Argument(name, new TimeArgument(), false, null),
                "colour" => Argument(name, new ColourArgument(), false, null),
                "score_holder" => Argument(name, new ScoreHolderArgument(), false, null),
                "slot" => Argument(name, new ItemSlotArgument(), false, null),
                "vec3" => Argument(name, new Vec3Argument(), false, null),
                "vec2" => Argument(name, new Vec2Argument(), false, null),
                "block_pos" => Argument(name, new BlockPosArgument(), false, null),
                "column_pos" => Argument(name, new ColumnPosArgument(), false, null),
                "rotation" => Argument(name, new RotationArgument(), false, null),
                "angle" => Argument(name, new AngleArgument(), false, null),
                "particle" => Argument(name, new ParticleArgument(), false, null),
                "entity_summon" => Argument(name, new EntitySummonArgument(), false, null),
                "mob_effect" => Argument(name, new MobEffectArgument(), false, null),
                "item_enchantment" => Argument(name, new ItemEnchantmentArgument(), false, null),
                "function" => Argument(name, new FunctionArgument(), false, null),
                "objective" => Argument(name, new ObjectiveArgument(), false, null),
                "objective_criterion" => Argument(name, new ObjectiveCriterionArgument(), false, null),
                "scoreboard_slot" => Argument(name, new ScoreboardSlotArgument(), false, null),
                "advancement" => Argument(name, new AdvancementArgument(), false, null),
                "predicate" => Argument(name, new PredicateArgument(), false, null),
                "message" => Argument(name, new MessageArgument(), false, null),
                "component" => Argument(name, new ComponentArgument(), false, null),
                "team" => Argument(name, new TeamArgument(), false, null),
                "entity_anchor" => Argument(name, new EntityAnchorArgument(), false, null),
                "dimension" => Argument(name, new DimensionArgument(), false, null),
                "operation" => Argument(name, new OperationArgument(), false, null),
                "attribute" => Argument(name, new AttributeArgument(), false, null),
                "storage" => Argument(name, new StorageArgument(), false, null),
                "item_modifier" => Argument(name, new ItemModifierArgument(), false, null),
                "biome" => Argument(name, new BiomeArgument(), false, null),
                "loot_table" => Argument(name, new LootTableArgument(), false, null),
                "sound" => Argument(name, new SoundArgument(), false, null),
                "recipe" => Argument(name, new RecipeArgument(), false, null),
                _ => throw new ArgumentException($"'{parser}' is not a valid parser type")
            };

            serializer.Populate(jToken.CreateReader(), argumentNode);
            return argumentNode;
        }
    }
}
