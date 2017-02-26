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
    public class Cart : ExcelReader, Output
    {
        public double Total { get; set; }
        public double TotalDiscount { get; set; }
        public int ItemCount { get; set; }
        public string CommandName { get; }
        public string CommandDescription { get; }
        public Dictionary<string, Item> cart { get; }
        private Catalog catalog;
        private Discount[] promotionList;
        private string filePath;
        private XLWorkbook workbook;  //Workbook object represents an Excel spreadsheet
        

        public Cart(Catalog catalog, params Discount[] promotionList)
        {
            //Initialize workbook
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            filePath = currentDirectory + "\\..\\..\\Data\\ShoppingList.xlsx";
            workbook = new XLWorkbook(filePath);

            //Initialize an empty cart dictionary
            cart = new Dictionary<string, Item>();

            //Initialize instance variables
            Total = 0;
            ItemCount = 0;
            TotalDiscount = 0;

            //Initialize command name and description
            CommandName = "cart";
            CommandDescription = "Shows all items in the cart and all applicable discounts";

            //Initialize private objects
            this.catalog = catalog;
            this.promotionList = promotionList;
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
            if (cart.TryGetValue(name, out newItem))
            {
                //Item exists so increment it's quantity.
                newItem.Quantity += 1;
            }
            else
            {
                //Searches catalog for the item then adds a new item to the cart
                Item catalogItem = catalog.FindItem(name);
                newItem = new Item(catalogItem);
                cart.Add(newItem.Name, newItem);
            }

            //Increment cart Total
            Total += newItem.Price;
            ItemCount++;
        }

        /// <summary>
        /// Iterates through every cart item and applies all qualifying discounts
        /// </summary>
        public void ApplyPromotions()
        {
            //Apply all promotions that are applicable
            foreach(var item in cart)
            {
                for (int i = 0; i < promotionList.Length; i++)
                {
                    double discount;
                    Promotion promotion = promotionList[i].ApplyDiscount(item.Value, out discount);

                    //Update cart Total accordingly
                    Total -= discount;
                    TotalDiscount += discount;

                    //Add promotion to list of promotions for this item
                    if(promotion != null)
                        item.Value.Promotions.Add(promotion);
                }
            }
        }

        public Output pay()
        {
            //Prints the receipt
            string output = PrintReceipt();

            return new OutputCLI("pay", "Pay and print receipt", output);
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            //Cart table title
            string line = Misc.drawLine('-', 50);
            string str = "\n" + line + "\n";
            str += "\t\t" + "Cart items" + "\n";
            str += line;
            
            //Cart table captions
            str += "\n" +"Name" + "\t\t" + "Price $" + "\t\t" + "Quantity" + 
                "\n";
            str += line + "\n";

            //Discount table title
            string discounts = "\n" + line + "\n";
            discounts += "\t\t" + "Promotions applied" + "\n";
            discounts += line;

            //Discount table captions
            discounts += "\n" + "Name" + "\t\t\t" + "Amount $" +"\n";
            discounts += line + "\n";

            //Table rows
            foreach (var item in cart)
            {
                //Display name, total price before discount and quantity 
                str += item.Value.Name + "\t\t" +
                    Misc.format2DP((item.Value.Price * item.Value.Quantity))+ "\t\t" + 
                    item.Value.Quantity + "\n";

                //Display total discount for each item in brackets. Does not display if total discount == 0
                string discount = "\t\t" + "(-" + Misc.format2DP(item.Value.TotalDiscount) + ")\n";
                str += item.Value.TotalDiscount == 0 ? "" : discount;

                //Add applied promotions to promotions table
                foreach(var promotion in item.Value.Promotions)
                {
                    discounts += item.Value.Name + " " + promotion.Name + " for" + "\t" +
                        Misc.format2DP(promotion.Amount) + " "+  promotion.Info + "\n";
                }
                
            }

            //Cart Total
            str += line + "\n";
            str += "Total:\t\t" + Misc.format2DP(Total);
            str += "\t\t" + ItemCount;

            //Discount Total 
            discounts += line + "\n";
            discounts += "You save:\t\t" + Misc.format2DP(TotalDiscount);
       
            //Display cart table and discount table
            str += "\n\n\n" + discounts;

            return str;
        }

        /// <summary>
        /// Prints a receipt
        /// </summary>
        /// <returns></returns>
        public string PrintReceipt()
        {
            string str = "";
            string line = Misc.drawLine('*', 58);

            //Header for the receipt
            str += "\t" + line + "\n\n";
            str += "\t\t\t" + "GroceryCo Receipt" + "\n";
            str += "\t\t\t"  + "\n";
            str += "\t" + line + "\n";

            //Print cart items and discount applied for each item
            foreach (var item in cart)
            {
                //Display name and quantity bbefore total price and discount
                str += "\t" + item.Value.Name + "\tx" + item.Value.Quantity + "\t\t\t\t\t$" +
                    Misc.format2DP((item.Value.Price * item.Value.Quantity)) + "\n";

                //Add applied promotions to the receipt
                foreach (var promotion in item.Value.Promotions)
                {
                    str += "\t  " + promotion.Name + " " + promotion.Info  +
                        "\t\t\t(-$" + Misc.format2DP(promotion.Amount) + ") " + "\n";
                }
            }

            //Total
            str += "\n" + "\t\t\t\t\t\t" + "Total:\t$" + Misc.format2DP(Total);
            str += "\n" + "\t" + "Items sold: " + ItemCount;
            str += "\n" + "\t" + "You saved : $" + TotalDiscount;

            //Print thank you message
            str += "\n\n\tThank you for shopping at GroceryCo!\n";

            return str;
        }

        public void PrintToCLI()
        {
            Console.WriteLine(ToString());
        } 
    }
}
