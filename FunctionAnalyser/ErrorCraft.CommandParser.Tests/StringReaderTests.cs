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
    }
}
