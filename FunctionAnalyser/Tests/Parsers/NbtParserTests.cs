using CommandParser;
using CommandParser.Parsers.NbtParser;
using CommandParser.Parsers.NbtParser.NbtArguments;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Parsers
{
    [TestClass]
    public class NbtParserTests
    {
        [TestMethod]
        public void NbtParser_ResultsShouldBeCompound()
        {
            // Arrange
            StringReader reader = new StringReader("{foo: 'bar', baz: 3}");

            // Act
            NbtReader.ReadValue(reader, out INbtArgument result);

            // Assert
            Assert.IsTrue(result is NbtCompound);
        }

        [TestMethod]
        public void NbtParser_ResultsShouldBeInteger()
        {
            // Arrange
            StringReader reader = new StringReader("8");

            // Act
            NbtReader.ReadValue(reader, out INbtArgument result);

            // Assert
            Assert.IsTrue(result is NbtInteger);
        }

        [TestMethod]
        public void NbtParser_ParseCompoundShouldFail_BecauseEmptyKey()
        {
            // Arrange
            StringReader reader = new StringReader("{: 'bar'}");

            // Act
            ReadResults readResults = NbtReader.ReadCompound(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void NbtParser_ParseCompoundShouldFail_BecauseExpectedValue()
        {
            // Arrange
            StringReader reader = new StringReader("{foo:}");

            // Act
            ReadResults readResults = NbtReader.ReadCompound(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void NbtParser_ParseArrayShouldFail_BecauseInvalidArrayType()
        {
            // Arrange
            StringReader reader = new StringReader("[M; 1, 2, 3]");

            // Act
            ReadResults readResults = NbtReader.ReadArray(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void NbtParser_ParseArrayShouldFail_BecauseInvalidArrayItem()
        {
            // Arrange
            StringReader reader = new StringReader("[I; 1, 3L]");

            // Act
            ReadResults readResults = NbtReader.ReadArray(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void NbtParser_ParseListShouldFail_BecauseInvalidListItem()
        {
            // Arrange
            StringReader reader = new StringReader("[1, 3L]");

            // Act
            ReadResults readResults = NbtReader.ReadArray(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void NbtParser_ParseItem_ResultShouldBeInteger()
        {
            // Arrange
            StringReader reader = new StringReader("1");

            // Act
            NbtReader.ReadItem(reader, out INbtArgument result);

            // Assert
            Assert.IsTrue(result is NbtInteger);
        }

        [TestMethod]
        public void NbtParser_ParseItem_ResultShouldBeShort()
        {
            // Arrange
            StringReader reader = new StringReader("1s");

            // Act
            NbtReader.ReadItem(reader, out INbtArgument result);

            // Assert
            Assert.IsTrue(result is NbtShort);
        }

        [TestMethod]
        public void NbtParser_ParseItem_ResultShouldBeFloat()
        {
            // Arrange
            StringReader reader = new StringReader("1.0f");

            // Act
            NbtReader.ReadItem(reader, out INbtArgument result);

            // Assert
            Assert.IsTrue(result is NbtFloat);
        }

        [TestMethod]
        public void NbtParser_ParseItem_ResultShouldBeDouble()
        {
            // Arrange
            StringReader reader = new StringReader("1.0");

            // Act
            NbtReader.ReadItem(reader, out INbtArgument result);

            // Assert
            Assert.IsTrue(result is NbtDouble);
        }

        [TestMethod]
        public void NbtParser_ParseItem_ResultShouldBeString()
        {
            // Arrange
            StringReader reader = new StringReader("foo");

            // Act
            NbtReader.ReadItem(reader, out INbtArgument result);

            // Assert
            Assert.IsTrue(result is NbtString);
        }

        [TestMethod]
        public void NbtParser_ParseItem_ResultShouldBeString_Quoted()
        {
            // Arrange
            StringReader reader = new StringReader("'foo bar baz'");

            // Act
            NbtReader.ReadItem(reader, out INbtArgument result);
            
            // Assert
            Assert.IsTrue(result is NbtString);
        }

        [TestMethod]
        public void NbtParser_ParseItem_EscapesStringCorrectly()
        {
            // Arrange
            StringReader reader = new StringReader("'foo \\\'bar\\\' \\\\ baz'");

            // Act
            NbtReader.ReadItem(reader, out INbtArgument result);
            NbtString nbtString = result as NbtString;

            // Assert
            Assert.AreEqual(nbtString.ToString(), "foo 'bar' \\ baz");
        }
    }
}
