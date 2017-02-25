using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryCoCheckout;

namespace GroceryCoCheckoutTests
{
    [TestClass]
    public class OnSalePriceTests
    {
        [TestMethod]
        public void OnSalePriceReadExcelSuccess()
        {
            //Create an empty OnSalePrice object
            OnSalePrice onSalePrice = new OnSalePrice();

            //Call ReadExcel to add items to the OnSalePrices dictionary. 
            //Any exception that occurs causes the test to fail
            try
            {
                onSalePrice.ReadExcel();
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            //Assert that items were added to the OnSalePrices dictionary
            Assert.IsNotNull(onSalePrice.OnSalePrices);
        }

        [TestMethod]
        public void OnSalePriceApplyDiscountSuccess()
        {
            //Create an OnSalePrice object and read in the item promotion definition
            OnSalePrice onSalePrice = new OnSalePrice();
            onSalePrice.ReadExcel();

            //Create an item with a price definition. An item by default has a total discount of 0
            Item item = new Item("Apple", 2.00);
            item.Quantity = 1;
            double discount;

            onSalePrice.ApplyDiscount(item, out discount);

            //Assert that the total dicount applied to the item is > 0
            Assert.IsTrue(item.TotalDiscount > 0);
        } 
    }
}
