using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace GroceryCoCheckout
{
    /// <summary>
    /// This class stores an in memory version of all items.
    /// </summary>
    public class Catalog : ExcelReader, Output
    {
        public string CommandName { get; }
        public string CommandDescription { get; }
        public SortedList<string, Item> catalog {get; } //List of all items availalbe
        private string filePath;
        private XLWorkbook workbook;  //Workbook object represents an Excel spreadsheet 

        public Catalog()
        {
            //Initialize workbook
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            filePath = currentDirectory + "\\..\\..\\Data\\Catalog.xlsx";
            workbook = new XLWorkbook(filePath);

            //Initialize command name and description
            CommandName = "catalog";
            CommandDescription = "Shows all available items";

            //Initialize an empty catalog list
            catalog = new SortedList<string, Item>();
        }

        /// <summary>
        /// Reads the Data/Catalog.xlsx file and populates the catalog object
        /// </summary>
        public void ReadExcel()
        {
            const int name = 1;
            const int price = 2;

            //Access the first worksheet in the Excel spreadsheet
            var worksheet = workbook.Worksheet("Sheet1");

            //Find the first row used
            var firstRowUsed = worksheet.FirstRowUsed();

            //Narrow down the row so that it only includes the used part
            var catalogRow = firstRowUsed.RowUsed();

            //Move to the next row(it now has the titles)
            catalogRow = catalogRow.RowBelow();

            //Get all the items while creating item objects
            while(! catalogRow.Cell(name).IsEmpty())
            {
                String itemName = catalogRow.Cell(name).GetString();
                double itemPrice = catalogRow.Cell(price).GetDouble();

                //Create a list of items keyed by the item name
                catalog.Add(itemName, new Item(itemName, itemPrice));

                catalogRow = catalogRow.RowBelow();
            }
        }

        /// <summary>
        /// Searches for an item in the catalog by name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Item FindItem(string name)
        {
            return catalog[name];
        }

        /// <summary>
        /// All items in the catalog in a string representation
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            //Catalog table title
            string line = Misc.drawLine('-', 50);
            string str = "\n" + line + "\n";
            str += "\t\t" + "Catalog" + "\n";
            str += line + "\n";

            //Table captions
            str += "Name" + "\t\t" + "Price $" + "\t\t" + "\n";
            str += line + "\n";

            foreach (var pair in catalog)
            {
                str += pair.Value.ToString() + "\n";
            }

            return str; 
        }

        /// <summary>
        /// Interface implmentation 
        /// </summary>
        public void PrintToCLI()
        {
            Console.WriteLine(ToString());
        }
    }
}
