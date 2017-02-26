# GroceryCoCheckout
This is a command line checkout program for the GroceryCo company.

## How to build and run
Clone this repository to your desktop:
`git clone https://github.com/kimtuitoek/GroceryCoCheckout.git`

### Using Visual studio
Open the **GroceryCoCheckout** file located in the project folder or follow the following steps:

1. Start Visual Studio.
2. On the menu bar, choose **File**, **Open**, **Project/Solution**.
The Open Project dialog box opens.
3. Locate the **GroceryCoCheckout** solution file in the project folder.
4. Choose the **F5 key** to run the project. A Command Prompt window appears with the **GroceryCo Checkout** logo displayed.

### Using the Command-line
Follow the steps outlined on the MSDN [here](https://msdn.microsoft.com/en-ca/library/78f4aasd.aspx)

## Usage
The command line accepts multiple commands. These commands perform a `Console.Write` action to display text on the command line. They are defined as follows:

| Command | Description |
| --- | --- |
| `cart` | Shows all items in the cart and all applicable discounts. |
| `catalog` | Shows all available items. |
| `pay` | Pay and print receipt. |
| `help` | Displays all available commands. |
| `exit` | Exits the program. |

## Assumptions
* There is sufficient memory to run the application on the GroceryCo computers. The Catalog and Promotion objects are read in from excel files and stored in memory.
* Items have already been scanned into the checkout system and stored in a ShoppingList excel file.
* Items in the ShoppingList excel file are in the order they were scanned.
* All discounts applied do not exceed the original price of the item.
* The GroceryCo computers have an Excel editor.

## Design choices
I chose to make an interactive command line checkout system and provided a design that allowed future extensibility.

###File inputs
* I decided to use Excel files to store the the list of items, prices and promotions. These files are available in the `Data` folder. The reason I chose this file type was because of how easy it is for a user to read and edit. This would allow the marketing team to easily define new promotions using an excel editor. The downside is that parsing this type of file had to be done by implementing the Excel reader interface. A concrete implementation of this interace could be provided int the future.
* I considered other storage formats that would be easier to deal as a developer such as `text` files, `CSV`, `JSON`, `XML`. The downside to this is that editing for a regular user would be difficult.
* Future considerations would be to provide a config file to define all settings applicable to this program.

###Classes
The classes used are:

| Class | Description |
| ---- | ---- |
| Item | This class represents an item. |
| Catalog | This class contains a list of all available item. |
| Cart | This class contains a list of items from the customer's shopping list and all promotions that can be applied to an item. |
| CLI | This class contains all available commands and their descriptions. |
| Promotion | This is the base class for all kinds of promotions. For example, the OnSalePrice class is a subclass of  Promotion. |
| OnSalePrice | This class extends the Promotion class and implements the Discount interface. This class applies an "on sale price" promotion to an item based on the price definitions outlined in the `OnSalePrice` excel file. An instance of this is passed during the creation of the cart object. |
| GroupPrice | This class extends the Promotion class and implements the Discount interface. This class applies a "group price" promotion to an item based on the price definition outlined in the `GroupPrice` excel file. An instance of this is passed during the creation of the cart object. |
| OutputCLI | This class is a concrete implementation for the Output interface. An Output object contains a command definition. This allows for the creation of any command. |
| Misc | This class contains miscellaneous methods that are used by any class. |

I designed the Cart and CLI classes with extensibility in mind. In order to add a new promotion, all one needs to do is implement the `Discount` interface and pass the object instance to the constructor of `Cart`. The discount will be applied by the `Cart` object by calling the `ApplyDiscount` method. To add a new command to the CLI, implement the Output interface or use the OutputCLI class to create an Output object that can be passed the CLI's constructor.

###Interfaces
The interfaces used are:

| Interface | Description |
| --- | --- |
| ExcelReader | This interface defines the `ReadExcel` method. Classes that implement this method must provide a way to read an excel file. |
| Discount | This interface defines the `ApplyDiscount` method. Classes that implement this method need to pass an Item and an output variable representing the `TotalDiscountApplied` to the item. It should return a `Promotion` object that represents the promotion applied to the item. |
| Output | This interface define the `PrintToCLI` method. This method is called when executing a command. Classes that implement this interface need to override the `ToString` method.  Could be extended to include command line arguments|

###Data structures
* `Sorted list` - I chose to use this data structure to model different objects like Catalog, CLI or anything that needed searching. Since the Sorted list implements binary search, the search time would be `O(log n)` (according to the MSDN). This ensures a consistent time to search since this operation is the most used in the Catalog and CLI objects.
 I had considered using a hash table instead but the downside was that high collision frequencies could not guarantee consistent performance and memory usage especially for a large data set (e.g. the catalog). While a better hash function could be provided to prevent more collisions and ensure consistency, the chances are that the hash function would be slow.
 The downside to using a sorted list is that initialization would take longer since every item needs to be placed in its right place.

* `Dictionary` - I chose this data structure to model objects like the Cart. Since the cart size was relatively small compared to the Catalog object. The TryGetValue implementation guarantees `O(1)` access time (according to the MSDN) which would make it faster to find items that have already been added to the cart when adding items from an unsorted shopping list.

###Design patterns
I consisdered using a few design patterns to solve some problems I encountered but was unable to due to time constraints. These are:
* `Singleton` - I wanted to have singletons for objects like Catalog, CLI, or the Promotion objects to ensure only one instance is created. This would ensure that memory usage is at a minimum with no extra objects being created.

* `Observer listener` - I wanted to use the FileSystemWatcher class(which implements the observer listener design pattern) from the System.IO package to watch for any changes to the prices and promotional prices defined in the excel files during runtime. This would ensure that all the prices and promotions defined in memory were the latest price definitions.

## Testing
All the tests are located in the GroceryCoCheckoutTests folder

## Limitations
* Time constraint limited the amount of features that could be implemented.
* Cannot enforce the Excel Data files to be formatted correctly so as to not break the functionality of the program.

