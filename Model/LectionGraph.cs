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
        public string Name { get; set; }            // Название лекции
        public string? Lecturer { get; set; }       // кто читает   
        public DateTime LectionDate { get; set; }   // дата для лекции
        public string Path { get; set; }            // путь к файлу

        public LectionGraph() { }

        public LectionGraph(DateTime lectionDate, string name, string lecturer, string path)
        {
            this.Name = name;
            this.Lecturer = lecturer;
            this.LectionDate = lectionDate;
            this.Path = path;
        }
    }
}
