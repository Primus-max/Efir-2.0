using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    internal class Congratulations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }              // описание              
        public int DurationOfCongratulations { get; set; }   // длительность поздравления       
        public int NumOfCongratulations { get; set; }        // колличество серий       
        public string Path { get; set; }                     // путь к сериалу
    }
}
