using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace GroceryCoCheckout
{
    /// <summary>
    /// This class contains the storage of the list of items that the customer is going to 
    /// purchase
    /// </summary>
    class Cart : ExcelReader, Output
    {
        public double Total { get; set; }
        public int ItemCount { get; set; }
        private Dictionary<string, Item> items;
        private Catalog catalog;
        private string filePath;
        private XLWorkbook workbook;  //Workbook object represents an Excel spreadsheet

        public Cart(Catalog catalog)
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            filePath = currentDirectory + "\\..\\..\\Data\\ShoppingList.xlsx";
            workbook = new XLWorkbook(filePath);

            items = new Dictionary<string, Item>();
            Total = 0;
            ItemCount = 0;

            this.catalog = catalog;
        }

        /// <summary>
        /// Adds an item to the cart
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        public void addItem(string name)
        {
            //Check whether item exits in the items dictionary
            //TryGetValue returns true if item exists
            Item newItem;
            if (items.TryGetValue(name, out newItem))
            {
                //Item exists so increment it's quantity.
                newItem.Quantity += 1;
            }
            else
            {
                //Searches catalog for the item then adds a new item to the cart
                Item catalogItem = catalog.FindItem(name);
                newItem = new Item(catalogItem);
                items.Add(newItem.Name, newItem);
            }

            //Update promotional discount total
            newItem.Discount += newItem.Price * (newItem.Promotions / 100);

            //Increment cart Total
            Total += newItem.Price;
            ItemCount++;
        }

        /// <summary>
        /// Reads the Data/Catalog.xlsx file and creates catalog object
        /// </summary>
        public void ReadExcel()
        {
            const int name = 1;

            //Access the first worksheet in the Excel spreadsheet
            var worksheet = workbook.Worksheet("Sheet1");

            //Find the first row used
            var firstRowUsed = worksheet.FirstRowUsed();

            //Narrow down the row so that it only includes the used part
            var catalogRow = firstRowUsed.RowUsed();

            //Move to the next row(it now has the titles)
            catalogRow = catalogRow.RowBelow();

            //Get all the items while creating item objects
            while (!catalogRow.Cell(name).IsEmpty())
            {
                String itemName = catalogRow.Cell(name).GetString();

                //Create a list of items keyed by the item name
                addItem(itemName);

                catalogRow = catalogRow.RowBelow();
            }
        }

        public void WriteExcel()
        {

        }

        public void ApplyGroupDiscounts()
        {

        }

        public void pay()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            string str = "\n" +"Name" + "\t\t" + "Price $" + 
                "\t\t" + "Quantity" + "\n";
            str += "------------------------------------------------------\n";

            foreach (var pair in items)
            {
                //Display name, total price before discount and quantity 
                str += pair.Value.Name + "\t\t" + 
                    (pair.Value.Price * pair.Value.Quantity) + "\t\t" + 
                    pair.Value.Quantity + "\n";

                //Display total discount for item in brackets. Does not display 
                //if discount == 0
                string discount = "\t\t" + "(-" + pair.Value.Discount + ")\n";
                str += pair.Value.Discount == 0 ? "" : discount;
            }

            str += "------------------------------------------------------\n";
            str += "Total:\t\t" + Total;
            str += "\t\t" + ItemCount;
            return str;
        }

        public void PrintToCLI()
        {
            Console.WriteLine(ToString());
        }
    }
}
