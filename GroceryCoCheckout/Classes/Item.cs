using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    class Item
    {
        /// <summary>
        /// Name of the item defined in the CSV
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of item to 2 decimal places
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The current discount percentage
        /// </summary>
        public int Discount { get; set; }
        public int Quantity { get; set; }
    }
}
