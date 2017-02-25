using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
namespace GroceryCoCheckout
{
    class OnSalePrice : Promotion, ExcelReader, Discount
    {
        private Dictionary<string, double> OnSalePrices; //List of all items availalbe
        private string filePath;
        private XLWorkbook workbook;  //Workbook object represents an Excel spreadsheet 

        public OnSalePrice()
        {
            //Initialize workbook
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            filePath = currentDirectory + "\\..\\..\\Data\\OnSalePrice.xlsx";
            workbook = new XLWorkbook(filePath);

            //Initialize dictionary
            OnSalePrices = new Dictionary<string, double>();

            //Initialize instance variables
            base.Name = "On sale";
            base.Type = PromotionType.Value;

        }

        /// <summary>
        /// Reads the Data/OnSalePrices.xlsx file and creates OnSale list object
        /// </summary>
        public void ReadExcel()
        {
            const int name = 1;
            const int promotion = 2;

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
                double itemPrice = catalogRow.Cell(promotion).GetDouble();

                //Create a list of SalePrices keyed by the item name
                OnSalePrices.Add(itemName, itemPrice);

                catalogRow = catalogRow.RowBelow();
            }
        }

        public void WriteExcel()
        {

        }

        /// <summary>
        /// Applies discount and adds it to the item's promotion collection.
        /// Also updates the total discount for the item.
        /// </summary>
        /// <param name="item"></param>
        public Promotion ApplyDiscount(Item item, out double amount)
        {
            //Check whether item exits in the OnSalePrices dictionary
            //TryGetValue returns true if item exists
            double onSalePrice;
            double discount = 0 ;
            Promotion newPromotion = null;

            if (OnSalePrices.TryGetValue(item.Name, out onSalePrice))
            {
                // Item exists so increment the total discount based on the item's 
                // quantity
                if (onSalePrice != 0)
                {
                    discount = (item.Price - onSalePrice) * item.Quantity;
                    item.TotalDiscount += discount;
                    string info = "@ " + Misc.format2DP(onSalePrice) + " each";
                    newPromotion = new Promotion(Name, info, discount);
                }
            }
            amount = discount;
            return newPromotion;
        }
    }
}
