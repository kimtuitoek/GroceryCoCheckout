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
    /// Information on how to implement these using closedXML is on https://github.com/ClosedXML/ClosedXML/wiki
    /// </summary>
    public interface ExcelReader
    {
        /// <summary>
        /// This methods reads an excel file
        /// </summary>
        void ReadExcel();
    }
}
