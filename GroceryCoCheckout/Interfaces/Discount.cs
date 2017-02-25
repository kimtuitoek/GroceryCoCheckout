using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// ApplyDiscount should be implemented. This method is used to apply a discount to an
    /// item
    /// </summary>
    interface Discount
    {
        /// <summary>
        /// Applies a promotion to an item. Also returns the total discount applied on the item. 
        /// A promotion consists of a Name, description(e.g On sale @ 0.50 each) and an amount
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amountApplied">The total promotion amount applied to an item</param>
        /// <returns>Returns a promotion object representing the promotion applied</returns>
        Promotion ApplyDiscount(Item item, out double amountApplied);
    }
}
