# GroceryCoCheckout
This is a command line checkout program for the GroceryCo company.

## How to build and run

## Usage

## Assumptions
* There is sufficient memory to run the application on the GroceryCo computers. The Catalog and Promotion objects are read in from excel files and stored in memory.
* Items have already been scanned into the checkout system and stored in an excel file.
* All discounts applied do not exceed the original price of the item.
* The GroceryCo computers have an Excel editor.

## Design choices
I chose to make the command line checkout system interactive and provided a design that allowed future extensibility.

###File inputs
* I decided to use Excel files to store the the list of items, prices and promotions. These files are availabel in the `Data` folder. The reason I chose this file type was because it is easier to read and edit and this would allow the marketing to team easily define new promotions. The downside is that parsing this type of file had to be done by implementing the Excel reader interface. A concrete immplementation of this interace could be provided int the future.
* I considered other storage formats that would be easier to deal with such as `text` files, `CSV`, `JSON`, `XML`. The downside to this is that editing would be difficult.
* Future considerations would be to provide a config file to define all settings applicable to this program.

###Classes
The classes used are :

| Class | Description |
| ---- | ---- |
| Item | |
| Catalog | |
| Cart | |
| CLI | |
| Promotion | |
| OnSalePrice| |
| GroupPrice | |
| OutputCLI | |
| Misc | |

###Interfaces
The interfaces used are:

| Interface | Description |
| --- | --- |
| Excel reader | |
| Discount | |
| Output | |

###Data structures
* `Sorted list` - I chose to use this data structure to model different objects like Catalog, Output or anything that needed searching. Since the Sorted list implements binary search, the search time would be O(log n) (according to MSDN). This ensures a consistent time to search. 
 I had considered using a hash table insted but the downside was that high collision frequencies could not gurrantee consistent performance and memory usage escpecially for a large data set(e.g the catalog). While the a better hash function could be provided to prevent more collisions and ensure consistency, the chances are that the hash function would be slow.
 The downside to using a sorted list is that initialization would take longer since every item needs to be placed in its right place.

* `Dictionary` - I chose this data structure to model objects like the Cart. Since the cart size was relatively small compared to the Catalog object. The TryGetValue implementation gurantees O(1) access time according to the MSDN which would make it faster to find items that have already been added to the cart when adding items from an unsorted shopping list.

###Design patterns
I consisdered using a few design patterns to solve some problems I encountered but was unable to due to time constraints. These are:
* `Singleton` - I wanted to have singletons for objects like Catalog, CLI, or the Promotion objects to ensure only one instance is created. This would ensure that memory usage is at a minium with no extra objects being created.

* `Observer listner` - I wanted to use the FileSystemWatcher class(which imolments the observer listener design pattern) from the System.IO package to watch for any changes to the prices and promotional prices defined in the excel files during runtime. This would ensure that all the prices and promotions defined in memory were the latest price definitions.

## Limitations


