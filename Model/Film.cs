using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Efir.Model
{
    internal class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // описание
        public int NumOfRun { get; set; }       // колличество показов
        public Duration Duration { get; set; }       // длительность
        public int Series { get; set; }         // часть фильма или серия
        public int NumOfSeries { get; set; }    // колличество частей фильма или серий
        public DateTime LastRun { get; set; }   // последний показ
        public string Path { get; set; }        // путь к фильму

    }
}
