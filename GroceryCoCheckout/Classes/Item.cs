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
        /// Total discount applied to item
        /// </summary>
        public double Discount { get; set; }

        /// <summary>
        /// The current discount percentage
        /// </summary>
        public double Promotions { get; set; }

        /// <summary>
        /// GroupPromotions object
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

        public Item(Item item)
        {
            this.Name = item.Name;
            this.Price = item.Price;
            this.Promotions = item.Promotions;
            this.Quantity = 1;
            this.Discount = 0;
        }

        public string ToString()
        {
            string str = Name + "\t\t" + Price + "\t\t" + Promotions;
            return str;
        }
    }
}
