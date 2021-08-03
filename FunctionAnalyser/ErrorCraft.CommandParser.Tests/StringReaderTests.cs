using ErrorCraft.CommandParser.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.CommandParser.Tests {
    [TestClass]
    public class StringReaderTests {
        [TestMethod]
        public void GetString_ReturnsCorrectValue() {
            StringReader stringReader = new StringReader("foo");
            string result = stringReader.GetString();
            Assert.AreEqual("foo", result);
        }

        [TestMethod]
        public void GetCursor_ReturnsCorrectValue() {
            StringReader stringReader = new StringReader("foo");
            int result = stringReader.GetCursor();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void SetCursor_SetsCorrectValue() {
            StringReader stringReader = new StringReader("foo");
            stringReader.SetCursor(2);
            Assert.AreEqual(2, stringReader.GetCursor());
        }

        [TestMethod]
        public void CanRead_ReturnsTrue() {
            StringReader stringReader = new StringReader("foo");
            bool successful = stringReader.CanRead();
            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void CanRead_ReturnsFalse() {
            StringReader stringReader = new StringReader("");
            bool successful = stringReader.CanRead();
            Assert.IsFalse(successful);
        }

        [TestMethod]
        public void CanRead_WithLength_ReturnsTrue() {
            StringReader stringReader = new StringReader("foo");
            bool successful = stringReader.CanRead(2);
            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void CanRead_WithLength_ReturnsFalse() {
            StringReader stringReader = new StringReader("foo");
            bool successful = stringReader.CanRead(5);
            Assert.IsFalse(successful);
        }

        [TestMethod]
        public void Peek_ReturnsCorrectValue() {
            StringReader stringReader = new StringReader("foo");
            char character = stringReader.Peek();
            Assert.AreEqual('f', character);
        }

        [TestMethod]
        public void Read_ReturnsCorrectValue() {
            StringReader stringReader = new StringReader("foo");
            char character = stringReader.Read();
            Assert.AreEqual('f', character);
        }

        [TestMethod]
        public void Read_IsAtNewPosition() {
            StringReader stringReader = new StringReader("foo");
            stringReader.Read();
            Assert.AreEqual(1, stringReader.GetCursor());
        }

        [TestMethod]
        public void Skip_IsAtNewPosition() {
            StringReader stringReader = new StringReader("foo");
            stringReader.Skip();
            Assert.AreEqual(1, stringReader.GetCursor());
        }

        [TestMethod]
        public void Skip_WithLength_IsAtNewPosition() {
            StringReader stringReader = new StringReader("foo");
            stringReader.Skip(2);
            Assert.AreEqual(2, stringReader.GetCursor());
        }

        [TestMethod]
        public void IsNext_ReturnsTrue() {
            StringReader stringReader = new StringReader("abc");
            bool successful = stringReader.IsNext('a');
            Assert.IsTrue(successful);
        }

        [TestMethod]
        public void IsNext_ReturnsFalse() {
            StringReader stringReader = new StringReader("abc");
            bool successful = stringReader.IsNext('b');
            Assert.IsFalse(successful);
        }

        [TestMethod]
        public void ReadBoolean_ReadsCorrectValue() {
            StringReader stringReader = new StringReader("true");
            stringReader.ReadBoolean(out bool result);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ReadBoolean_ReadsCorrectValue_WithAllCapitals() {
            StringReader stringReader = new StringReader("TRUE");
            stringReader.ReadBoolean(out bool result);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ReadBoolean_IsUnsuccessful_BecauseValueReadIsNotABoolean() {
            StringReader stringReader = new StringReader("foo");
            ParseResults parseResults = stringReader.ReadBoolean(out _);
            Assert.IsFalse(parseResults.Successful);
        }
    }
}
