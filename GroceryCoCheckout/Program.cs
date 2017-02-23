using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    class Program
    {
        static void Main(string[] args)
        {
            //-----------Initialize all services-----------
            //Create and initialize item catalog
            Catalog catalog = new Catalog();
            catalog.ReadExcel();

            //Create and intialize promotional prices list

            //Create and initialize group promotions list

            //Create an empty cart and add items to the cart
            Cart cart = new Cart(catalog);
            cart.ReadExcel();

            //Create and initialize CLI(Command Line Interface)
            CLI commands = new CLI(catalog, cart);

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
    }
}
