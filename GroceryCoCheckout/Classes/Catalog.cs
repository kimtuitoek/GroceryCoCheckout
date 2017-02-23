using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// This class stores an in memory version of all items.
    /// </summary>
    class Catalog : Output
    {
        private SortedList<string, Item> catalog;


        public string ToString()
        {
            string str = "";

            return str; 
        }

        public void PrintToCLI()
        {

        }
    }
}
