using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryCoCheckout;

namespace GroceryCoCheckoutTests
{
    [TestClass]
    public class GroupPriceTests
    {
        [TestMethod]
        public void GroupPriceReadExcelSuccess()
        {
            //Create an empty GroupPrice object
            GroupPrice groupPrice = new GroupPrice();

            //Call ReadExcel to add items to the OnSalePrices dictionary. 
            //Any exception that occurs causes the test to fail
            try
            {
                groupPrice.ReadExcel();
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            //Assert that items were added to the OnSalePrices dictionary
            Assert.IsNotNull(groupPrice.GroupPrices);
        }

        [TestMethod]
        public void GroupPriceApplyDiscountSuccess()
        {
            //Create an GroupPrice object and read in the item promotion definition
            GroupPrice groupPrice = new GroupPrice();
            groupPrice.ReadExcel();

            //Create an item with a price definition. An item by default has a total discount of 0
            Item item = new Item("Orange", 2.00);
            item.Quantity = 3; //Quantity is 3 to test group promotion
            double discount;

            groupPrice.ApplyDiscount(item, out discount);

            //Assert that the total dicount applied to the item is > 0
            Assert.IsTrue(item.TotalDiscount > 0);
        }
    }
}
