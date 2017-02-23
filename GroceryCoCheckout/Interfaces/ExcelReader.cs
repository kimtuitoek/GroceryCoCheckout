using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML;

namespace GroceryCoCheckout.Interfaces
{
    /// <summary>
    /// Classes that implement this iterface must implement the following methods:
    /// ReadExcel method
    /// WriteExcel method
    /// </summary>
    interface ExcelReader
    {
        /// <summary>
        /// This methods reads an excel file
        /// </summary>
        void ReadExcel(string filePath);

        /// <summary>
        /// This methods writes to an excel file overwriting any existing data.
        /// If the file does not exist create a new one
        /// </summary>
        void WriteExcel(string filePath);
    }
}
