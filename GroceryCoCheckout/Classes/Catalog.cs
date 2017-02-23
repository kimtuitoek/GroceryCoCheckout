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
    class Catalog : ExcelReader, Output
    {
        private SortedList<string, Item> catalog; //List of all items availalbe
        private string filePath;
        private XLWorkbook workbook;  //Workbook object represents an Excel spreadsheet 

        public Catalog()
        {
            filePath = System.IO.Directory.GetCurrentDirectory() + "\\..\\..\\Data\\Catalog.xlsx";
            catalog = new SortedList<string, Item>();
            workbook = new XLWorkbook(filePath);
        }

        /// <summary>
        /// Reads the Data/Catalog.xlsx file and creates catalog object
        /// </summary>
        public void ReadExcel()
        {
            const int name = 1;
            const int price = 2;
            const int promotions = 3;

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
                double itemPromotion = catalogRow.Cell(promotions).GetDouble();

                //Create a list of items keyed by the item name
                catalog.Add(itemName, new Item(itemName, itemPrice, itemPromotion));

                catalogRow = catalogRow.RowBelow();
            }
        }

        public void WriteExcel()
        {

        }

        public string ToString()
        {
            string str = "Name" + "\t\t" + "Price $" + "\t\t" + "Promotion %" + "\n";
            str += "---------------------------------------------\n";
            foreach(var pair in catalog)
            {
                str += pair.Value.ToString() + "\n";
            }

            return str; 
        }

        public void PrintToCLI()
        {
            Console.WriteLine(ToString());
        }
    }
}
