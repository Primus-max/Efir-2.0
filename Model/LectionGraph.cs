using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    internal class LectionGraph
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } // описание        
        public DateTime LectionDate { get; set; }   // последний показ
        public string Path { get; set; }        // путь к фильму

        public LectionGraph() { }

        public LectionGraph(DateTime lectionDate, int id, string name, string description, int numOfRun, TimeSpan duration, int series, int numOfSeries, DateTime lastRun, string path)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.LectionDate = lectionDate;

        }
    }
}
