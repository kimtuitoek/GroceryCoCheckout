using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// This class contains the storage of the list of items that the customer is going to 
    /// purchase
    /// </summary>
    class Cart : Output
    {
        private Dictionary<string, Item> items;
        public int Total { get; set; }
        public int Count { get; set; }


        /// <summary>
        /// adds an item to the cart
        /// </summary>
        public void addItem(string name, Item item)
        {

        }

        public void applyDiscounts()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
