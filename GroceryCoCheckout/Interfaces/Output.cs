using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// Classes that implement this interface must implement the following methods:
    /// ToString method: converts the class intance variables to strings 
    /// PrintToCLI method: prints the generated string by the ToString method to the CLI
    /// </summary>
    public interface Output
    {
        /// <summary>
        /// The name of the command. All commands should be in small caps
        /// </summary>
        string CommandName { get; }

        /// <summary>
        /// A description of the command
        /// </summary>
        string CommandDescription { get; }

        /// <summary>
        /// The string to be printed to the command line
        /// </summary>
        /// <returns></returns>
        string ToString();

        /// <summary>
        /// Prints the string defined by the String method to the command line
        /// </summary>
        void PrintToCLI();
    }
}
