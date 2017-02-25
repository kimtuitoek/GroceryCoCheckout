using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryCoCheckout;

namespace GroceryCoCheckoutTests
{
    [TestClass]
    public class CLITests
    {
        [TestMethod]
        public void CLIRunCommandSuccess()
        {
            //Create an Output object to be passed to the CLI constructor
            Output command = new OutputCLI("test", "description", "command testing!");

            CLI commands = new CLI(command);

            //Assert that a command that exists succeeds
            Assert.IsTrue(commands.RunCommand("test"));
        }

        [TestMethod]
        public void CLIRunCommandFailed()
        {
            //Create an Output object to be passed to the CLI constructor
            Output command = new OutputCLI("test", "description", "command testing!");

            CLI commands = new CLI(command);

            //Assert that a command that has not been defined will fail
            Assert.IsFalse(commands.RunCommand("fail"));
        }
    }
}
