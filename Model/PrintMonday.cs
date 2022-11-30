using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    internal class PrintMonday
    {
        public int Id { get; set; }
        public int Series { get; set; }
        public TimeSpan TimeToEfir { get; set; }
        public DateTime DateRun { get; set; }
        public string EventName { get; set; }
        public string? Description { get; set; }
        public string? Option { get; set; }

        public PrintMonday() { }

        public PrintMonday(DateTime dateRun, int series, TimeSpan timeToEfir, string eventName, string? description, string option)
        {
            this.DateRun = dateRun;
            this.Series = series;
            this.TimeToEfir = timeToEfir;
            this.EventName = eventName;
            this.Description = description;
            this.Option = option;
        }
    }
}

