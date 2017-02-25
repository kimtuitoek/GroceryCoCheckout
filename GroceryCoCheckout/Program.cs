using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GroceryCoCheckout
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintLogo();
            //-----------Initialize all services-----------
            //Create and initialize item catalog
            Catalog catalog = new Catalog();
            catalog.ReadExcel();

            //Create and intialize OnSalePrice promotion object
            OnSalePrice onSalePrice = new OnSalePrice();
            onSalePrice.ReadExcel();

            //Create and initialize group promotion object

            //Create an empty cart, add items to the cart and then apply all 
            //qualifying promotions
            Cart cart = new Cart(catalog, onSalePrice);
            cart.ReadExcel();
            cart.ApplyPromotions();

            //Create and initialize CLI(Command Line Interface)
            CLI commands = new CLI(catalog, cart, cart.pay());

            //-----------Read input------------------------
            Console.WriteLine("Enter a command. Type \"help\" to get a list of all commands.");
            string input = Console.ReadLine();

            //Continue receiving inputs until user types exit. Run the specified command
            while (!(input.CompareTo("exit") == 0))
            {
                commands.RunCommand(input);
                Console.WriteLine("\n"+ "Enter a command. Type \"help\" to get a list of all commands.");
                input = Console.ReadLine();
            }

            //-----------Exit------------------------------
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }

        /// <summary>
        /// Displays logo
        /// </summary>
        public static void PrintLogo()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string filePath = currentDirectory + "\\..\\..\\Data\\logo.txt";
            string text = File.ReadAllText(filePath, Encoding.UTF8);
            
            Console.WriteLine(text + "\n");
        }
    }
}
