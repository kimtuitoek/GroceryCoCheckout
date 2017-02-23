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

            //Create empty cart

            //Create and initialize CLI(Command Line Interface)

            //-----------Read input------------------------

            //-----------Exit------------------------------
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
