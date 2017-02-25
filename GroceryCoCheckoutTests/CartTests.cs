using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryCoCheckout;

namespace GroceryCoCheckoutTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void CartReadExcelSuccess()
        {
            //Create a catalog object with items and a discount object
            Catalog catalog = new Catalog();
            catalog.ReadExcel();

            Discount discount = new OnSalePrice();

            Cart cart = new Cart(catalog, discount);
            //Call ReadExcel to add items to the cart. 
            //Any exception that occurs causes the test to fail
            try
            {
                cart.ReadExcel();
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            //Assert that items were added to the catalog
            Assert.IsNotNull(cart.cart);
        }

        [TestMethod]
        public void CartAddItemExists()
        {
            //Create a catalog of all items
            Catalog catalog = new Catalog();
            catalog.ReadExcel();

            //Create a promotion
            OnSalePrice onSalePrice = new OnSalePrice();
            onSalePrice.ReadExcel();

            //Insert 1 item to the cart twice
            Cart cart = new Cart(catalog, onSalePrice);
            cart.addItem("Apple");
            cart.addItem("Apple");

            //Assert that the quantity of the inserted item is 2
            Assert.IsTrue(cart.cart["Apple"].Quantity == 2);
        }

        [TestMethod]
        public void CartAddItemDoesntExist()
        {
            //Create a catalog of all items
            Catalog catalog = new Catalog();
            catalog.ReadExcel();

            //Create a promotion
            OnSalePrice onSalePrice = new OnSalePrice();
            onSalePrice.ReadExcel();

            //Insert 1 item to the cart twice
            Cart cart = new Cart(catalog, onSalePrice);
            cart.addItem("Apple");

            //Assert that the quantity of the inserted item is 1
            Assert.IsTrue(cart.cart["Apple"].Quantity == 1);
        }

        [TestMethod]
        public void CartApplyPromotionSuccess()
        {
            //Create a catalog of all items
            Catalog catalog = new Catalog();
            catalog.ReadExcel();

            //Create a promotion
            OnSalePrice onSalePrice = new OnSalePrice();
            onSalePrice.ReadExcel();

            //Insert 1 item to the cart that has a onsaleprice promotion defined and apply the promotion
            Cart cart = new Cart(catalog, onSalePrice);
            cart.addItem("Apple");
            cart.ApplyPromotions();

            //Assert that the promotion was applied correctly to the added item
            Assert.IsNotNull(cart.cart["Apple"].Promotions);
        }
    }
}
