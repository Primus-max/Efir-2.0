using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    internal class EfirOnMonday
    {
        public int Id { get; set; }
        public TimeSpan TimeToEfir { get; set; }
        public string Event { get; set; }
        public string? Description { get; set; }
        public string Option { get; set; }

        public EfirOnMonday() { }

        public EfirOnMonday(TimeSpan timeToEfir, string @event, string? description, string option)
        {
            this.TimeToEfir = timeToEfir;
            this.Event = @event;
            this.Description = description;
            this.Option = option;
        }
    }
}
/*Monday
Tuesday
Wednesday
Thursday
Friday
Saturday
Sunday*/