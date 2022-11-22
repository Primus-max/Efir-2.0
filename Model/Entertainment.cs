﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    internal class Entertainment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } // описание
        public int NumOfRun { get; set; }       // колличество показов
        public TimeSpan Duration { get; set; }  // длительность
        public int Series { get; set; }         // часть фильма или серия
        public int NumOfSeries { get; set; }    // колличество частей фильма или серий
        public DateTime LastRun { get; set; }   // последний показ
        public string Path { get; set; }        // путь к фильму

        public Entertainment() { }

        public Entertainment(int id, string name, string description, int numOfRun, TimeSpan duration, int series, int numOfSeries, DateTime lastRun, string path)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.NumOfRun = numOfRun;
            this.Duration = duration;
            this.Series = series;
            this.NumOfSeries = numOfSeries;
            this.LastRun = lastRun;
            this.Path = path;
        }
    }
}