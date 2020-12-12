using CommandParser;
using CommandParser.Arguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests.Arguments
{
    [TestClass]
    public class SwizzleArgumentTests
    {
        [TestMethod]
        public void SwizzleArgument_ParseShouldSucceed()
        {
            // Arrange
            HashSet<char> characters = new HashSet<char>() { 'a', 'b', 'c' };
            SwizzleArgument argument = new SwizzleArgument(characters);
            IStringReader reader = new IStringReader("abc");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void SwizzleArgument_ParseShouldFail_BecauseDuplicateCharacters()
        {
            // Arrange
            HashSet<char> characters = new HashSet<char>() { 'a', 'b', 'c' };
            SwizzleArgument argument = new SwizzleArgument(characters);
            IStringReader reader = new IStringReader("abbc");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void SwizzleArgument_ParseShouldFail_BecauseInvalidCharacter()
        {
            // Arrange
            HashSet<char> characters = new HashSet<char>() { 'a', 'b', 'c' };
            SwizzleArgument argument = new SwizzleArgument(characters);
            IStringReader reader = new IStringReader("abcd");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void SwizzleArgument_ShouldNotRead_SpaceCharacter()
        {
            // Arrange
            HashSet<char> characters = new HashSet<char>() { 'a', 'b', 'c' };
            SwizzleArgument argument = new SwizzleArgument(characters);
            IStringReader reader = new IStringReader("ab c");

            // Act
            argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(reader.CanRead(2));
        }
    }
}
