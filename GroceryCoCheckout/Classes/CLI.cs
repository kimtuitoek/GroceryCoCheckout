using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryCoCheckout
{
    /// <summary>
    /// This class stores all the commands available. Calls all the PrintToCLI methods of 
    /// each Output object
    /// </summary>
    public class CLI
    {
        private Output[] output;
        private SortedList<string, Action> commands;
        private Dictionary<string, string> descriptions;

        public CLI(params Output[] output)
        {
            //Pass an instance of an existing object to create a commads for it
            this.output = output;

            //Build list of commands mapping the command name to an action delegate
            commands = new SortedList<string, Action>();
            descriptions = new Dictionary<string, string>();

            //Create a command and adds its description
            for (int i = 0; i < output.Length; i++)
            {
                commands.Add(output[i].CommandName, output[i].PrintToCLI);
                descriptions.Add(output[i].CommandName, output[i].CommandDescription);
            }

            //Add help command and its description
            commands.Add("help", ToString);
            descriptions.Add("help", "Shows a list of all available commands");
        }

        /// <summary>
        /// Searches for the command in the list of commands and executes it. If not found
        /// run the defaultCommand method
        /// </summary>
        /// <param name="command"></param>
        public bool RunCommand(string command)
        {
            bool status = false;
            try
            {
                Action run = commands[command];
                run();
                status = true;
            }
            catch(KeyNotFoundException)
            {
                defaultCommand();
            }
            return status;
        }

        /// <summary>
        /// Displays the default message
        /// </summary>
        private void defaultCommand()
        {
            Console.WriteLine("Error. Command does not exist. Type \"help\" to get a list of " + 
                "all available commands");
        }

        /// <summary>
        /// Displays all available commands
        /// </summary>
        private void ToString()
        {
            string line = Misc.drawLine('-', 57);

            //Command table title
            string str = "\n" + line +"\n";
            str += "\t\t" + "All Commands" + "\n";
            str += line + "\n";

            //Cart table captions
            str += "Name" + "\t\t" + "Description" + "\n";
            str += line + "\n";

            //Command table entries
            foreach (var v in commands)
            {
               //Display command name
               str += v.Key + "\t\t";

               //Diplay command description
               string description = "";
               descriptions.TryGetValue(v.Key, out description);

                str += description + "\n";
            }

            str += "exit" + "\t\t" + "Exits the program" + "\n";

            Console.WriteLine(str);
        }

    }
}
