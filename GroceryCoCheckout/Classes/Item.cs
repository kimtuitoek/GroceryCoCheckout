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
        /// Name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of item to 2 decimal places
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Total discount applied to item
        /// </summary>
        public double TotalDiscount { get; set; }

        /// <summary>
        /// List of all the promotions applied to this item
        /// </summary>
        public List<Promotion> Promotions { get; set; }

        /// <summary>
        /// The quantity of the item
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Constructor. Creates an item based on its name and price
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public Item(string name, double price)
        {
            Name = name;
            Price = price;
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="item"></param>
        public Item(Item item)
        {
            this.Name = item.Name;
            this.Price = item.Price;
            this.Promotions = item.Promotions;
            this.Quantity = 1;
            this.TotalDiscount = 0;
            Promotions = new List<Promotion>();
        }

        public string ToString()
        {
            string str = Name + "\t\t" + Price;
            return str;
        }
    }
}
