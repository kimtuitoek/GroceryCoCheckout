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
        public double Promotions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double GroupPromotions { get; set; }

        /// <summary>
        /// The quantity of the item
        /// </summary>
        public int Quantity { get; set; }

        public Item(string name, double price, double promotions)
        {
            Name = name;
            Price = price;
            Promotions = promotions;
        }

        public string ToString()
        {
            string str = Name + "\t\t" + Price + "\t\t" + Promotions;
            return str;
        }
    }
}
