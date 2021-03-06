﻿using CommandParser;
using CommandParser.Arguments;
using CommandParser.Collections;
using CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Arguments
{
    [TestClass]
    public class ItemArgumentTests
    {
        [TestMethod]
        public void ItemArgument_ParseShouldSucceed()
        {
            // Arrange
            Items.Set("[\"foo\", \"bar\"]");
            ItemArgument argument = new ItemArgument();
            IStringReader reader = new StringReader("foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ItemArgument_ParseShouldFail_BecauseItemDoesNotExist()
        {
            // Arrange
            Items.Set("[\"foo\", \"bar\"]");
            ItemArgument argument = new ItemArgument();
            IStringReader reader = new StringReader("baz");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsFalse(readResults.Successful);
        }

        [TestMethod]
        public void ItemArgument_ParseShouldSucceed_WithNamespace()
        {
            // Arrange
            Items.Set("[\"foo\", \"bar\"]");
            ItemArgument argument = new ItemArgument();
            IStringReader reader = new StringReader("minecraft:foo");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }

        [TestMethod]
        public void ItemArgument_ParseShouldSucceed_WithNbt()
        {
            // Arrange
            Items.Set("[\"foo\", \"bar\"]");
            ItemArgument argument = new ItemArgument();
            IStringReader reader = new StringReader("foo{bar: 'baz'}");

            // Act
            ReadResults readResults = argument.Parse(reader, out _);

            // Assert
            Assert.IsTrue(readResults.Successful);
        }
    }
}
