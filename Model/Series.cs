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
        public string? Description { get; set; }     // описание
        public int NumOfRun { get; set; }           // колличество показов       
        public TimeSpan Duration { get; set; }      // длительность серии        
        public int NumOfSeries { get; set; }        // колличество  серий
        public int IsSeries { get; set; }           // текущая серия
        public DateTime LastRun { get; set; }       // последний показ
        public string Path { get; set; }            // путь к сериалу

        public Series() { }

        public Series(int id, string name, string? description, int numOfRun, TimeSpan duration, int numOfSeries, int isSeries, DateTime lastRun, string path)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.NumOfRun = numOfRun;
            this.Duration = duration;
            this.NumOfSeries = numOfSeries;
            this.IsSeries = isSeries;
            this.LastRun = lastRun;
            this.Path = path;
        }
    }
}
