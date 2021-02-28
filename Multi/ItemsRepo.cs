using Multi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi
{
    public interface IItemsRepo
    {
        string Execute(string[] args);
    }
    public class ItemsRepo : IItemsRepo
    {
        private readonly List<Command> commands = new List<Command>(){
            new Command("KEYS", 1),
            new Command("MEMBERS", 2),
            new Command("ADD", 3),
            new Command("REMOVE", 3),
            new Command("REMOVEALL", 2),
            new Command("CLEAR", 1),
            new Command("KEYEXISTS", 2),
            new Command("VALUEEXISTS", 3),
            new Command("ALLMEMBERS", 1),
            new Command("ITEMS", 1),
            new Command("HELP", 1),
        };
        private string[] args;
        private Dictionary<string, List<string>> items = new Dictionary<string, List<string>>();

        public ItemsRepo()
        {
                
        }

        public string Execute(string[] args)
        {
            this.args = args;
            if (args.Length == 0)
            {
                return "Invalid Command.\nFor any help. TYPE 'HELP' and press enter.";
            } else {
                Command command = commands.FirstOrDefault(x=>x.Name.Equals(args[0], StringComparison.InvariantCultureIgnoreCase));
                if(command==null){
                    return "Invalid Command.\nFor any help. TYPE 'HELP' and press enter.";
                } else if(args.Length< command.MinParameters){
                    return "Parameter missing.\nFor any help. TYPE 'HELP' and press enter.";
                } else {
                    switch(args[0].ToUpper())
                    {
                        case "KEYS":
                            return this.GetKeys();
                        case "HELP":
                            return this.GetHelp();
                        case "ADD":
                            return this.Add();
                        case "MEMBERS":
                            return this.GetMembers();
                        case "REMOVE":
                            return this.Remove();
                        case "REMOVEALL":
                            return this.RemoveAll();
                        case "CLEAR":
                            return this.Clear();
                        case "KEYEXISTS":
                            return this.KeyExists().ToString();
                        case "VALUEEXISTS":
                            if(this.KeyExists()){
                                return "ERROR, Key does not exist.";
                            }
                            return this.ValueExists().ToString();
                        case "ALLMEMBERS":
                            return GetAllMember();
                        case "ITEMS":
                            return this.GetItems();
                    }
                }
            }
            return null;
        }

        public string GetAllMember(){
            if (items.Count > 0)
            {
                int i = 1;
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, List<string>> item in items)
                {
                    foreach (string value in item.Value)
                    {
                        sb.AppendFormat("{0}) {1}\n", i, value);
                        i++;
                    }
                }
                return sb.ToString();
            }
            return "(emty set)";
        }
        public string GetItems(){
            if (items.Count > 0)
            {
                int i = 1;
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, List<string>> item in items)
                {
                    foreach (string value in item.Value)
                    {
                        sb.AppendFormat("{0}) {1}\t: {2}\n", i, item.Key, value);
                        i++;
                    }
                }
                return sb.ToString();
            }
            return "(emty set)";
        }
        public bool KeyExists()
        {
            string key = args[1].ToLower();
            return items.ContainsKey(key);
        }

        public bool ValueExists()
        {
            string key = args[1].ToLower();
            if (!items.ContainsKey(key))
            {
                return false;
            }
            return items[key].Contains(args[2]);
        }

        public string Clear()
        {
            items = new Dictionary<string, List<string>>();
            return "Cleared";
        }

        public string Remove()
        {
            string key = args[1].ToLower();
            if (!items.ContainsKey(key))
            {
                return "ERROR, Key does not exist.";
            }
            if (!items[key].Contains(args[2])){
                return "ERROR, Value does not exist.";
            }
            items[key].Remove(args[2]);
            if (items[key].Count == 0)
            {
                items.Remove(key);
            }
            return "Removed";
        }
        public string RemoveAll()
        {
            string key = args[1].ToLower();
            if (!items.ContainsKey(key))
            {
                return "ERROR, Key does not exist.";
            }
            items.Remove(key);
            return "Removed";
        }

        public string GetMembers()
        {
            string key = args[1].ToLower();
            if (items.ContainsKey(key))
            {
                return String.Join("\n", items[key]);
            }
            return "ERROR, Key does not exists.";
        }

        public string GetKeys()
        {
            if (items.Keys.Count > 0)
            {
                return String.Join("\n", items.Keys);
            }
            return "(empty set)";
        }
        public string Add()
        {
            string key = args[1].ToLower();
            string value = args[2];
            if (!items.ContainsKey(key)){
                items.Add(key, new List<string>());
            }
            if (items[key].Contains(value))
            {
                return "ERROR, value already exists.";
            }
            items[key].Add(args[2]);
            return "Added";
        }



        public string GetHelp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("KEYS\t    Returns all the keys in the dictionary.  Order is not guaranteed.\n");
            sb.AppendFormat("MEMBERS\t    Returns the collection of strings for the given key.  Return order is not guaranteed.  Returns an error if  \t    the  key does not exists.\n");
            sb.AppendFormat("ADD\t    Add a member to a collection for a given key.Display an error if the value already existed in the collection");
            sb.AppendFormat("REMOVE\t    Removes a value from a key.  If the last value is removed from the key, they key is removed from the         \t    dictionary If the key or value does not exist, displays an error.\n");
            sb.AppendFormat("REMOVEALL   Removes all value for a key and removes the key from the dictionary. Returns an error if the key does  \t    not exist.\n");
            sb.AppendFormat("CLEAR\t    Removes all keys and all values from the dictionary.\n");
            sb.AppendFormat("KEYEXISTS   Returns whether a key exists or not.\n");
            sb.AppendFormat("VALUEEXISTS Returns whether a value exists within a key.  Returns false if the key does not exist.\n");
            sb.AppendFormat("ALLMEMBERS  Returns all the values in the dictionary.  Returns nothing if there are none. Order is not guaranteed.\n");
            sb.AppendFormat("ITEMS\t    Returns all keys in the dictionary and all of their values.  Returns nothing if there are none.  Order is   \t    not guaranteed.\n");
            sb.AppendFormat("HELP\t    To Get Help for All Commands.\n");
            sb.AppendFormat("QUIT  \t    To Close Application\n");
            sb.AppendFormat("EXIt  \t    To Close Application");
            return sb.ToString(); 
        }
    }
}
