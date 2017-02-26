using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace GroceryCoCheckout
{
    public class GroupPrice : Promotion, Discount
    {
        public Dictionary<string, Dictionary<int, double>> GroupPrices { get; } //List of all items availalbe
        private string filePath;
        private XLWorkbook workbook;  //Workbook object represents an Excel spreadsheet 

        public GroupPrice()
        {
            //Initialize workbook
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            filePath = currentDirectory + "\\..\\..\\Data\\GroupPromotions.xlsx";
            workbook = new XLWorkbook(filePath);

            //Initialize dictionary
            GroupPrices = new Dictionary<string, Dictionary<int, double>>();
             
            //Initialize instance variables
            base.Name = "Group price";
            base.Type = PromotionType.Value;

        }

        /// <summary>
        /// Reads the Data/OnSalePrices.xlsx file and creates OnSale list object
        /// </summary>
        public void ReadExcel()
        {
            const int name = 1;
            const int quantity = 2;
            const int price = 3;

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
                int itemQuantity = (int)catalogRow.Cell(quantity).GetDouble();
                double itemPrice = catalogRow.Cell(price).GetDouble();

                //Create a list of SalePrices keyed by the item name
                Dictionary<int, double> quantityToPriceMap = new Dictionary<int, double>();
                quantityToPriceMap.Add(itemQuantity, itemPrice);
                GroupPrices[itemName] = quantityToPriceMap;

                catalogRow = catalogRow.RowBelow();
            }
        }

        /// <summary>
        /// Applies discount and adds it to the item's promotion collection.
        /// Also updates the total discount for the item.
        /// </summary>
        /// <param name="item"></param>
        public Promotion ApplyDiscount(Item item, out double amount)
        {
            //Check whether item exits in the GroupPrices dictionary
            //TryGetValue returns true if item exists else false
            Dictionary<int, double> quantityToPriceMap;
            double discount = 0;

            Promotion newPromotion = null;
            int quantity = item.Quantity;

            if (GroupPrices.TryGetValue(item.Name, out quantityToPriceMap))
            {
                // Item exists so increment the total discount based on the item's quantity
                if (quantityToPriceMap != null)
                {
                    KeyValuePair<int, double> pair = new KeyValuePair<int, double>();
                    foreach(var v in quantityToPriceMap)
                         pair = v;

                    //Check the quantity of the item and see whether is qualifies for a discount
                    while(quantity >= pair.Key)
                    {
                        discount = (item.Price * quantity - pair.Value);
                        item.TotalDiscount += discount;
                        quantity -= pair.Key;
                    }

                    string info = "@ " + pair.Key + " for $" + Misc.format2DP(pair.Value);
                    newPromotion = new Promotion(Name, info, discount);
                }
            }
            amount = discount;
            return newPromotion;
        }
    }
}
