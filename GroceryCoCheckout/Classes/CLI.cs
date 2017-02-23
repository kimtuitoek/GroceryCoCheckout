using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// This class stores all the commands available. Calls all the PrintToCLI methods of 
    /// each class
    /// </summary>
    class CLI
    {
        private Catalog catalog;
        private Cart cart;
        private SortedList<string, Action> commands;

        public CLI(Catalog catalog, Cart cart)
        {
            //Pass an instance of an existing object to create a commads for it
            this.catalog = catalog;
            this.cart = cart;

            //Build list of commands mapping the command name to an action delegate
            commands = new SortedList<string, Action>();

            //All commands should be in small caps
            commands.Add("catalog", catalog.PrintToCLI);
            commands.Add("cart", cart.PrintToCLI);
            commands.Add("help", printHelp);
        }

        /// <summary>
        /// Searches for the command in the list of commands and executes it. If not found
        /// run the defaultCommand method
        /// </summary>
        /// <param name="command"></param>
        public void RunCommand(string command)
        {
            try
            {
                Action run = commands[command];
                run();
            }
            catch(KeyNotFoundException)
            {
                defaultCommand();
            }
        }

        /// <summary>
        /// Displays the default message
        /// </summary>
        private void defaultCommand()
        {
            Console.WriteLine("Error. Command does not exist. Type \"help\" to get a list of" + 
                "all available commands");
        }

        /// <summary>
        /// Displays all available commands
        /// </summary>
        private void printHelp()
        {
            Console.WriteLine("\n" + "-------------All Commands-------------");
            foreach (var v in commands)
            {
                Console.WriteLine(v.Key);
            }
            Console.WriteLine("exit");
        }

    }
}
