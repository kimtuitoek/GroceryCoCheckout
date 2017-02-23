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
    interface Output
    {
        string ToString();
        void PrintToCLI();
    }
}
