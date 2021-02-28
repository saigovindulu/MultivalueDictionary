using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi.Models
{
    public class Command
    {
        public string Name { get; set; }
        public int MinParameters { get; set; }

        public Command(string name, int minParameters)
        {
            this.Name = name;
            this.MinParameters = minParameters;
        }
    }
}
