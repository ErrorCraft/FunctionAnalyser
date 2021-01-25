using System;
using System.Text;
using System.Collections.Generic;
using CommandParser.Results.Arguments;
using CommandParser.Parsers.NbtParser.NbtArguments;
using static CommandParser.Providers.NumberProvider;

namespace CommandParser
{
    public class CommandError
    {
        private string Message;

        public CommandError(string message)
        {
            Message = message;
        }

        public string GetMessage()
        {
            return Message;
        }

        public CommandError WithContext(IStringReader reader)
        {
            Message += GetContext(reader);
            return this;
        }

        private static string GetContext(IStringReader reader)
        {
            int minimum = Math.Max(reader.GetCursor() - 10, 0);
            int maximum = Math.Min(reader.GetCursor(), reader.GetLength());

            StringBuilder builder = new StringBuilder($" at position {reader.GetCursor()}: ");
            if (minimum > 0) builder.Append("...");
            builder.Append(reader.GetString()[minimum..maximum]);
            builder.Append("<--[HERE]");
            return builder.ToString();
        }

        private static string QuoteCharacter(char character)
        {
            if (character == '\'') return $"\"{character}\"";
            else return $"'{character}'";
        }

        public static CommandError ExpectedLiteral(string literal) => new CommandError($"Expected literal {literal}");
        public static CommandError ExpectedArgumentSeparator() => new CommandError($"Expected whitespace to end one argument, but found trailing data");
        public static CommandError UnknownOrIncompleteCommand() => new CommandError($"Unknown or incomplete command");
        public static CommandError IncorrectArgument() => new CommandError($"Incorrect argument for command");
        public static CommandError ExpectedCharacter(char character) => new CommandError($"Expected {QuoteCharacter(character)}");

        public static CommandError InvalidEscapeSequence(char character) => new CommandError($"Invalid escape sequence '\\{character}' in string");
        public static CommandError ExpectedStartOfQuote() => new CommandError($"Expected quote to start a string");
        public static CommandError ExpectedEndOfQuote() => new CommandError($"Unclosed quoted string");

        public static CommandError ExpectedBoolean() => new CommandError($"Expected boolean");
        public static CommandError InvalidBoolean(string value) => new CommandError($"Invalid boolean, expected 'true' or 'false' but found '{value}'");

        public static CommandError ExpectedInteger() => new CommandError($"Expected integer");
        public static CommandError ExpectedLong() => new CommandError($"Expected long");
        public static CommandError ExpectedFloat() => new CommandError($"Expected float");
        public static CommandError ExpectedDouble() => new CommandError($"Expected double");
        public static CommandError InvalidInteger(string number) => new CommandError($"Invalid integer '{number}'");
        public static CommandError InvalidLong(string number) => new CommandError($"Invalid long '{number}'");
        public static CommandError InvalidFloat(string number) => new CommandError($"Invalid float '{number}'");
        public static CommandError InvalidDouble(string number) => new CommandError($"Invalid double '{number}'");
        public static CommandError IntegerTooLow(int result, int minimum) => new CommandError($"Integer must not be less than {minimum.ToString(NumberFormatInfo)}, found {result.ToString(NumberFormatInfo)}");
        public static CommandError IntegerTooHigh(int result, int maximum) => new CommandError($"Integer must not be more than {maximum.ToString(NumberFormatInfo)}, found {result.ToString(NumberFormatInfo)}");
        public static CommandError LongTooLow(long result, long minimum) => new CommandError($"Long must not be less than {minimum.ToString(NumberFormatInfo)}, found {result.ToString(NumberFormatInfo)}");
        public static CommandError LongTooHigh(long result, long maximum) => new CommandError($"Long must not be more than {maximum.ToString(NumberFormatInfo)}, found {result.ToString(NumberFormatInfo)}");
        public static CommandError FloatTooLow(float result, float minimum) => new CommandError($"Float must not be less than {minimum.ToString(NumberFormatInfo)}, found {result.ToString(NumberFormatInfo)}");
        public static CommandError FloatTooHigh(float result, float maximum) => new CommandError($"Float must not be more than {maximum.ToString(NumberFormatInfo)}, found {result.ToString(NumberFormatInfo)}");
        public static CommandError DoubleTooLow(double result, double minimum) => new CommandError($"Double must not be less than {minimum.ToString(NumberFormatInfo)}, found {result.ToString(NumberFormatInfo)}");
        public static CommandError DoubleTooHigh(double result, double maximum) => new CommandError($"Double must not be more than {maximum.ToString(NumberFormatInfo)}, found {result.ToString(NumberFormatInfo)}");

        public static CommandError InvalidUuid() => new CommandError($"Invalid UUID");
        public static CommandError InvalidSwizzle(HashSet<char> characters) => new CommandError($"Invalid swizzle, expected combination of {string.Join(", ", characters)}");

        public static CommandError InvalidNameOrUuid() => new CommandError($"Invalid name or UUID");
        public static CommandError MissingSelectorType() => new CommandError($"Missing selector type");
        public static CommandError UnknownSelectorType(char type) => new CommandError($"Unknown selector type '@{type}'");
        public static CommandError UnknownSelectorOption(string option) => new CommandError($"Unknown selector option '{option}'");
        public static CommandError ExpectedValueForSelectorOption(string option) => new CommandError($"Expected value for option '{option}'");
        public static CommandError SelectorExpectedEndOfOptions() => new CommandError($"Expected end of options");
        public static CommandError SelectorLimitTooLow() => new CommandError($"Limit must be at least 1");
        public static CommandError UnknownGamemode(string gamemode) => new CommandError($"Unknown gamemode '{gamemode}'");
        public static CommandError UnknownSort(string sort) => new CommandError($"Unknown sort type '{sort}'");
        public static CommandError InvalidId() => new CommandError($"Invalid ID");
        public static CommandError UnknownEntity(ResourceLocation entity) => new CommandError($"Unknown entity: {entity}");
        public static CommandError SelectorTooManyPlayers() => new CommandError($"Only one player is allowed, but the provided selector allows more than one");
        public static CommandError SelectorTooManyEntities() => new CommandError($"Only one entity is allowed, but the provided selector allows more than one");
        public static CommandError SelectorPlayersOnly() => new CommandError($"Only players may be affected by this command, but the provided selector includes entities");

        public static CommandError ExpectedKey() => new CommandError($"Expected key");
        public static CommandError ExpectedValue() => new CommandError($"Expected value");
        public static CommandError InvalidArrayType(char type) => new CommandError($"Invalid array type {QuoteCharacter(type)}");
        public static CommandError NbtCannotInsert(INbtArgument value, INbtArgument into) => new CommandError($"Cannot insert {value.GetName()} into {into.GetName()}");
        public static CommandError InvalidNbtPath() => new CommandError($"Invalid NBT path element");
        public static CommandError ExpectedValueOrRange() => new CommandError($"Expected value or range of values");
        public static CommandError RangeMinBiggerThanMax() => new CommandError($"Min cannot be bigger than max");

        public static CommandError ItemTagsNotAllowed() => new CommandError($"Item tags are not allowed here, only actual items");
        public static CommandError UnknownItem(ResourceLocation item) => new CommandError($"Unknown item '{item}'");
        public static CommandError BlockTagsNotAllowed() => new CommandError($"Block tags are not allowed here, only actual items");
        public static CommandError UnknownBlock(ResourceLocation block) => new CommandError($"Unknown block '{block}'");
        public static CommandError UnknownBlockProperty(ResourceLocation block, string property) => new CommandError($"Block {block} does not have property '{property}'");
        public static CommandError UnknownBlockPropertyValue(ResourceLocation block, string property, string value) => new CommandError($"Block {block} does not accept '{value}' for {property} property");
        public static CommandError ExpectedValueForBlockProperty(ResourceLocation block, string property) => new CommandError($"Expected value for property '{property}' on block {block}");
        public static CommandError DuplicateBlockProperty(ResourceLocation block, string property) => new CommandError($"Property '{property}' can only be set once for block {block}");
        public static CommandError UnclosedBlockStateProperties() => new CommandError($"Expected closing ] for block state properties");
        public static CommandError InvalidTickCount() => new CommandError($"Tick count must be non-negative");
        public static CommandError InvalidTimeUnit() => new CommandError($"Invalid unit");
        public static CommandError UnknownColour(string colour) => new CommandError($"Unknown colour '{colour}'");
        public static CommandError InapplicableOption(string option) => new CommandError($"Option '{option}' isn't applicable here");
        public static CommandError InvalidEntityType(ResourceLocation entity) => new CommandError($"Invalid or unknown entity type '{entity}'");
        public static CommandError UnknownSlot(string slot) => new CommandError($"Unknown slot '{slot}'");
        public static CommandError MixedCoordinateType() => new CommandError($"Cannot mix world & local coordinates (everything must either use ^ or not)");
        public static CommandError Vec3CoordinatesIncomplete() => new CommandError($"Incomplete (expected 3 coordinates)");
        public static CommandError Vec2CoordinatesIncomplete() => new CommandError($"Incomplete (expected 2 coordinates)");
        public static CommandError RotationIncomplete() => new CommandError($"Incomplete (expected 2 angles)");
        public static CommandError ExpectedCoordinate() => new CommandError($"Expected a coordinate");
        public static CommandError ExpectedBlockPosition() => new CommandError($"Expected a block position");
        public static CommandError ExpectedAngle() => new CommandError($"Expected an angle");
        public static CommandError InvalidAngle() => new CommandError($"Invalid angle");

        public static CommandError UnknownParticle(ResourceLocation particle) => new CommandError($"Unknown particle: {particle}");
        public static CommandError UnknownEffect(ResourceLocation effect) => new CommandError($"Unknown effect: {effect}");
        public static CommandError UnknownEnchantment(ResourceLocation enchantment) => new CommandError($"Unknown enchantment: {enchantment}");
        public static CommandError ObjectiveNameTooLong() => new CommandError($"Objective names cannot be longer than 16 characters");
        public static CommandError UnknownCriterion(string criterion) => new CommandError($"Unknown criterion '{criterion}'");
        public static CommandError UnknownDisplaySlot(string slot) => new CommandError($"Unknown display slot '{slot}'");

        public static CommandError InvalidChatComponent(string component) => new CommandError($"Invalid chat component: {component}");
        public static CommandError InvalidEntityAnchor(string anchor) => new CommandError($"Invalid entity anchor position {anchor}");
        public static CommandError InvalidOperation() => new CommandError($"Invalid operation");
        public static CommandError UnknownStructureRotation(string structureRotation) => new CommandError($"Unknown structure rotation '{structureRotation}'");
        public static CommandError UnknownStructureMirror(string structureMirror) => new CommandError($"Unknown structure mirror '{structureMirror}'");
        public static CommandError InvalidUnquotedStringStart() => new CommandError($"Unquoted strings cannot start with a digit");
    }
}
