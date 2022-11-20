using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    internal class Series
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }     // описание
        public int NumOfRun { get; set; }           // колличество показов       
        public int DurationOfSeries { get; set; }   // длительность серии        
        public int NumOfSeries { get; set; }        // колличество  серий
        public DateTime LastRun { get; set; }       // последний показ
        public string Path { get; set; }            // путь к сериалу
    }
}
