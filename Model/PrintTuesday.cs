using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    internal class PrintTuesday : IPrintDay
    {
        public string Id { get; set; }
        public int Series { get; set; }
        public TimeSpan TimeToEfir { get; set; }
        public string? EventName { get; set; }
        public string? Description { get; set; }
        public string? Option { get; set; }

        public PrintTuesday() { }

        public PrintTuesday(int series, TimeSpan timeToEfir, string eventName, string? description, string option)
        {
            this.Series = series;
            this.TimeToEfir = timeToEfir;
            this.EventName = eventName;
            this.Description = description;
            this.Option = option;
        }
    }
}

