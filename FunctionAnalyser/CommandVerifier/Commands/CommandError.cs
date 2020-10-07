using CommandVerifier.NbtParser.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandVerifier.Commands
{
    public class CommandError
    {
        public static List<string> StoredErrors { get; private set; } = new List<string>();
        public CommandError(string message) => Message = message;
        public readonly string Message;

        public void AddWithContext(StringReader reader) => StoredErrors.Add(Message + GetContext(reader));
        public void Add() => StoredErrors.Add(Message);

        private static string QuoteCharacter(char value)
        {
            if (value == '"') return "'" + value.ToString() + "'";
            return "\"" + value.ToString() + "\"";
        }

        private static string GetContext(StringReader reader)
        {
            int minimum = Math.Max(reader.Cursor - 10, 0);
            int maximum = Math.Min(reader.Cursor, reader.Command.Length);

            StringBuilder builder = new StringBuilder(" at position " + reader.Cursor.ToString() + ": ");
            if (minimum > 0) builder.Append("...");
            builder.Append(reader.Command[minimum..maximum]);
            builder.Append("<--[HERE]");
            return builder.ToString();
        }

        // Expected
        public static CommandError ExpectedStartOfQuote() => new CommandError("Expected quote to start a string");
        public static CommandError ExpectedEndOfQuote() => new CommandError("Unclosed quoted string");
        public static CommandError ExpectedBoolean() => new CommandError("Expected boolean");
        public static CommandError ExpectedInteger() => new CommandError("Expected integer");
        public static CommandError ExpectedFloat() => new CommandError("Expected float");
        public static CommandError ExpectedDouble() => new CommandError("Expected double");
        public static CommandError ExpectedArgumentSeparator() => new CommandError("Expected whitespace to end one argument, but found trailing data");
        public static CommandError ExpectedCharacter(char character) => new CommandError("Expected " + QuoteCharacter(character));
        public static CommandError ExpectedKey() => new CommandError("Expected key");
        public static CommandError ExpectedValue() => new CommandError("Expected value");
        public static CommandError ExpectedValueForPropertyOnBlock(string property, string block) => new CommandError("Expected value for property '" + property + "' on block " + block);
        
        // Invalid
        public static CommandError InvalidEscapeSequence(char character) => new CommandError("Invalid escape sequence " + QuoteCharacter(character) + " in quoted string");
        public static CommandError InvalidBoolean(string value) => new CommandError("Invalid boolean, expected 'true' or 'false' but found '" + value + "'");
        public static CommandError InvalidInteger(string value) => new CommandError("Invalid integer '" + value + "'");
        public static CommandError InvalidFloat(string value) => new CommandError("Invalid float '" + value + "'");
        public static CommandError InvalidDouble(string value) => new CommandError("Invalid double '" + value + "'");
        public static CommandError InvalidUuid() => new CommandError("Invalid UUID");
        public static CommandError InvalidNameOrUuid() => new CommandError("Invalid name or UUID");
        public static CommandError InvalidSwizzle(HashSet<char> values) => new CommandError("Invalid swizzle, expected combination of " + string.Join(", ", values));
        public static CommandError InvalidNamespacedId() => new CommandError("Invalid ID");
        public static CommandError InvalidTickCount() => new CommandError("Tick count must be non-negative");
        public static CommandError InvalidTimeUnit() => new CommandError("Invalid unit");
        public static CommandError InvalidChatComponent(string component) => new CommandError("Invalid chat component: " + component);

        // Numbers
        public static CommandError IntegerTooLow(int result, int minimum) => new CommandError("Integer must not be less than " + minimum.ToString(SubcommandHelper.MinecraftNumberFormatInfo) + ", found " + result.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError FloatTooLow(float result, float minimum) => new CommandError("Float must not be less than " + minimum.ToString(SubcommandHelper.MinecraftNumberFormatInfo) + ", found " + result.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError DoubleTooLow(double result, double minimum) => new CommandError("Double must not be less than " + minimum.ToString(SubcommandHelper.MinecraftNumberFormatInfo) + ", found " + result.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError IntegerTooHigh(int result, int maximum) => new CommandError("Integer must not be more than " + maximum.ToString(SubcommandHelper.MinecraftNumberFormatInfo) + ", found " + result.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError FloatTooHigh(float result, float maximum) => new CommandError("Float must not be more than " + maximum.ToString(SubcommandHelper.MinecraftNumberFormatInfo) + ", found " + result.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError DoubleTooHigh(double result, double maximum) => new CommandError("Double must not be more than " + maximum.ToString(SubcommandHelper.MinecraftNumberFormatInfo) + ", found " + result.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError RangeMinBiggerThanMax() => new CommandError("Min cannot be bigger than max");
        public static CommandError ExpectedValueOrRange() => new CommandError("Expected value or range of values");
        public static CommandError RangeIntegerTooLow(int minimum) => new CommandError("Integer range minimum shouldn't be less than " + minimum.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError RangeIntegerTooHigh(int maximum) => new CommandError("Integer range maximum shouldn't be more than " + maximum.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError RangeFloatTooLow(float minimum) => new CommandError("Float range minimum shouldn't be less than " + minimum.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError RangeFloatTooHigh(float maximum) => new CommandError("Float range maximum shouldn't be more than " + maximum.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError RangeDoubleTooLow(double minimum) => new CommandError("Double range minimum shouldn't be less than " + minimum.ToString(SubcommandHelper.MinecraftNumberFormatInfo));
        public static CommandError RangeDoubleTooHigh(double maximum) => new CommandError("Double range maximum shouldn't be more than " + maximum.ToString(SubcommandHelper.MinecraftNumberFormatInfo));


        // Unknown
        public static CommandError UnknownOrIncompleteCommand() => new CommandError("Unknown or incomplete command");
        public static CommandError UnknownParticle(Types.NamespacedId particle) => new CommandError("Unknown particle: " + particle.ToString());
        public static CommandError UnknownCriterion(string criterion) => new CommandError("Unknown criterion '" + criterion + "'");
        public static CommandError UnknownItem(string item) => new CommandError("Unknown item '" + item + "'");
        public static CommandError UnknownDisplaySlot(string slot) => new CommandError("Unknown display slot '" + slot + "'");
        public static CommandError UnknownEntity(string entity) => new CommandError("Unknown entity: " + entity);
        public static CommandError UnknownEffect(string effect) => new CommandError("Unknown effect: " + effect);
        public static CommandError UnknownEnchantment(string enchantment) => new CommandError("Unknown enchantment: " + enchantment);
        public static CommandError UnknownBlock(string block) => new CommandError("Unknown block type '" + block + "'");
        public static CommandError UnknownBlockProperty(string block, string property) => new CommandError("Block " + block + " does not have property '" + property + "'");
        public static CommandError UnknownBlockPropertyValue(string block, string property, string value) => new CommandError("Block " + block + " does not accept '" + value + "' for " + property + "property");
        public static CommandError UnknownSlot(string slot) => new CommandError("Unknown slot '" + slot + "'");
        public static CommandError UnknownColour(string colour) => new CommandError("Unknown colour '" + colour + "'");

        // Incorrect
        public static CommandError IncorrectArgument() => new CommandError("Incorrect argument for command");

        // Selector
        public static CommandError MissingSelectorType() => new CommandError("Missing selector type");
        public static CommandError UnknownSelectorType(string type) => new CommandError("Unknown selector type '" + type + "'");
        public static CommandError SelectorTooManyEntities() => new CommandError("Only one entity is allowed, but the provided selector allows more than one");
        public static CommandError SelectorTooManyPlayers() => new CommandError("Only one player is allowed, but the provided selector allows more than one");
        public static CommandError SelectorPlayersOnly() => new CommandError("Only players may be affected by this command, but the provided selector includes entities");
        public static CommandError SelectorLimitTooLow() => new CommandError("Limit must be at least 1");
        public static CommandError UnknownSelectorOption(string option) => new CommandError("Unknown option '" + option + "'");
        public static CommandError InapplicableSelectorOption(string option) => new CommandError("Option '" + option + "' isn't applicable here");

        // NBT
        public static CommandError NbtCannotInsert(INbtArgument value, INbtArgument into) => new CommandError("Can't insert " + value.Id + " into " + into.Id);
        public static CommandError NbtPathInvalid() => new CommandError("Invalid NBT path element");

        // Coordinates
        public static CommandError MixedCoordinateType() => new CommandError("Cannot mix world & local coordinates (everything must either use ^ or not)");
        public static CommandError Vec3CoordinatesIncomplete() => new CommandError("Incomplete (expected 3 coordinates)");
        public static CommandError Vec2CoordinatesIncomplete() => new CommandError("Incomplete (expected 2 coordinates)");
        public static CommandError AngleIncomplete() => new CommandError("Incomplete (expected 1 angle)");

        // Other
        public static CommandError ItemTagsNotAllowed() => new CommandError("Tags aren't allowed here, only actual items");
        public static CommandError BlockTagsNotAllowed() => new CommandError("Tags aren't allowed here, only actual blocks");
        public static CommandError UnclosedBlockStateProperties() => new CommandError("Expected closing ] for block state properties");
        public static CommandError ObjectiveNameTooLong() => new CommandError("Objective names cannot be longer than 16 characters");
    }
}
