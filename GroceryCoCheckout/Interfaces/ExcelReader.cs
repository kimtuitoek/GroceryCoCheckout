using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML;

namespace GroceryCoCheckout
{
    /// <summary>
    /// Classes that implement this iterface must implement the following methods:
    /// ReadExcel method
    /// WriteExcel method
    /// Information on how to implement these using closedXML is on https://github.com/ClosedXML/ClosedXML/wiki
    /// </summary>
    interface ExcelReader
    {
        /// <summary>
        /// This methods reads an excel file
        /// </summary>
        void ReadExcel();

        /// <summary>
        /// This methods writes to an excel file overwriting any existing data.
        /// If the file does not exist create a new one
        /// </summary>
        void WriteExcel();
    }
}
