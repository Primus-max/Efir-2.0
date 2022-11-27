﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    internal class EfirOnWednesday
    {
        public int Id { get; set; }
        public TimeSpan TimeToEfir { get; set; }
        public string Event { get; set; }
        public string? Description { get; set; }
        public string Option { get; set; }

        public EfirOnWednesday() { }

        public EfirOnWednesday(TimeSpan timeToEfir, string @event, string? description, string option)
        {
            this.TimeToEfir = timeToEfir;
            this.Event = @event;
            this.Description = description;
            this.Option = option;
        }
    }
}