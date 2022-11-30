using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    /// <summary>
    /// Коллекция развлекательного (список директорий)
    /// </summary>
    internal class EntertainmentCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }     // описание
        public int NumOfRun { get; set; }           // колличество показов                  
        public int NumOfSeries { get; set; }        // колличество  серий        
        public DateTime LastRun { get; set; }       // последний показ
        public string Path { get; set; }            // путь к сериалу

        public EntertainmentCollection() { }

        public EntertainmentCollection(int id, string name, string? description, int numOfRun, int numOfSeries, DateTime lastRun, string path)

        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.NumOfRun = numOfRun;
            this.NumOfSeries = numOfSeries;
            this.LastRun = lastRun;
            this.Path = path;
        }
    }
}
