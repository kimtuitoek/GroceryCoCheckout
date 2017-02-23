using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Classes
{
    /// <summary>
    /// This class stores all the commands available. Calls all the PrintToCLI methods of 
    /// each class
    /// </summary>
    class CLI
    {
        private Catalog catalog;

        public CLI(Catalog catalog)
        {
            this.catalog = catalog;
        }

        public void RunCommand(string command)
        {
            switch (command)
            {
                case "catalog":
                    catalog.PrintToCLI();
                default:
                    defaultCommand();
            }
        }

        private void defaultCommand()
        {
            
        }

    }
}
