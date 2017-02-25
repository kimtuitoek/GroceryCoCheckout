using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GroceryCoCheckout;
using System.Collections.Generic;

namespace GroceryCoCheckoutTests
{
    [TestClass]
    public class CatalogTests
    {
        [TestMethod]
        public void CatalogReadExcelSuccessful()
        {
            //Create an empty catalog object
            Catalog catalog = new Catalog();

            //Call ReadExcel to add items to the catalog. 
            //Any exception that occurs causes the test to fail
            try
            {
                catalog.ReadExcel();
            }
            catch (Exception)
            {
                Assert.Fail();
            }

            //Assert that items were added to the catalog
            Assert.IsNotNull(catalog.catalog);
            
        }

        [TestMethod]
        public void CatalogFindItemExists()
        {
            //Create a catalog of all items
            Catalog catalog = new Catalog();
            catalog.ReadExcel();

            //Create a new item
            Item item = new Item("Apple", 2);

            //Find the item in the catalog and assert that they are equal
            Assert.AreEqual(item.Name, catalog.FindItem("Apple").Name);
        }

        [TestMethod]
        public void CatalogFindItemDoesntExist()
        {
            //Create a catalog of all items
            Catalog catalog = new Catalog();
            catalog.ReadExcel();

            //Search for a non-existing item
            try
            {
                catalog.FindItem("A non-existing item");
            }
            catch(Exception ex)
            {
                //Assert KeyNotFoundException is thrown
                Assert.IsTrue(ex is KeyNotFoundException);
            }          
        }
    }
}
