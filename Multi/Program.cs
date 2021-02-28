using Multi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi
{
    class Program
    {
       
        static void Main(string[] args)
        {
            IItemsRepo repo = new ItemsRepo();
            Console.WriteLine("Multi-Value Dictionary Application.");
            Console.WriteLine("For any help. TYPE 'HELP' and press enter.");
            label1:
            Console.Write(">");
            string line = Console.ReadLine();
            string[] parameters = line.Split(" ".ToCharArray());
            if (parameters.Length > 0 && (parameters[0].Equals("EXIT", StringComparison.InvariantCultureIgnoreCase) || parameters[0].Equals("QUIT", StringComparison.InvariantCultureIgnoreCase)))
            {
                Console.Write("Thank you.");
            }
            else
            {
                Console.WriteLine(repo.Execute(parameters));
                Console.WriteLine("");
                goto label1;
            }

        }
    }
}
