using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// A concrete implemtation of the Output interface
    /// </summary>
    class OutputCLI : Output
    {
        public string CommandName {get;}
        public string CommandDescription { get; }
        public string CommandOutput { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="output"></param>
        public OutputCLI(string name, string description, string output)
        {
            CommandName = name;
            CommandDescription = description;
            CommandOutput = output;
        }

        /// <summary>
        /// This method executes the method that returns a string 
        /// </summary>
        public void PrintToCLI()
        {
            Console.WriteLine(CommandOutput);
        }
    }
}
