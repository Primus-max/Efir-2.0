using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int IsActiv { get; set; }

        public Event(string name, int isActiv)
        {
            this.Name = name;
            this.IsActiv = isActiv;
        }
    }
}
