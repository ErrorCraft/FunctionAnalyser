using ErrorCraft.CommandParser.Tree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ErrorCraft.CommandParser.Tests {
    [TestClass]
    public class DispatcherTests {
        [TestMethod]
        public void Register_AddsNewCommand() {
            Dispatcher dispatcher = new Dispatcher();
            dispatcher.Register(new LiteralNode("foo"));
            Assert.AreEqual(1, dispatcher.CommandCount);
        }
    }
}
