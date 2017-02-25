using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// This class is the base calss for a promotion. A promotion is either a 
    /// percentage or a value and has a name, amount to be applied and additional
    /// information about the promotion.
    /// </summary>
    class Promotion
    {
        /// <summary>
        /// Each promotion has a type
        /// </summary>
        public PromotionType Type { get; set; }
        
        /// <summary>
        /// Name of the promotion
        /// </summary>
        public string Name { get; set; }

        public string Info { get; set; }

        public double Amount { get; set; }

        public Promotion()
        {
            Name = "";
            Info = "";
            Amount = 0;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="amount"></param>
        public Promotion(string name, string description, double amount)
        {
            Name = name;
            Info = description;
            Amount = amount;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        /// <param name="type"></param>
        public Promotion(string name, double amount,PromotionType type)
        {
            Name = name;
            Amount = amount;
            Type = type;
        }
    }
}
