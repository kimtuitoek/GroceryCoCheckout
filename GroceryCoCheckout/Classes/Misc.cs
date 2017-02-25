using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// Holds all miscellaneous functions to be used by any class
    /// </summary>
    class Misc
    {
        /// <summary>
        /// Formarts a double to 2 decimal places
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string format2DP(double number)
        {
            return String.Format("{0:.00}", number);
        }

        /// <summary>
        /// Draw a line of specific characters
        /// </summary>
        /// <param name="character"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string drawLine(char character, int length)
        {
            string str = "";
            for (int i = 0; i < length; i++)
            {
                str += character;
            }

            return str;
        }
    }
}
