using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout.Interfaces
{
    /// <summary>
    /// Classes that implement this iterface must implement the following methods:
    /// WriteExcel method
    /// Information on how to implement this using closedXML is on https://github.com/ClosedXML/ClosedXML/wiki
    /// </summary>
    interface ExcelWriter
    {
        /// <summary>
        /// This methods writes to an excel file overwriting any existing data.
        /// If the file does not exist create a new one
        /// </summary>
        void WriteExcel();
    }
}
