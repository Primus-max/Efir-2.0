using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir
{
    internal interface IPrintDay
    {
        public string Id { get; set; }
        public int Series { get; set; }
        public TimeSpan TimeToEfir { get; set; }
        public string EventName { get; set; }
        public string? Description { get; set; }
        public string? Option { get; set; }

    }
}
