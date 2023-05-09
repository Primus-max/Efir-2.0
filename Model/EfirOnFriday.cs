﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Efir.Model
{
    public class EfirOnFriday
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкрементное свойство
        public int Id { get; set; }
        public TimeSpan TimeToEfir { get; set; }
        public string EventName { get; set; }
        public string? Description { get; set; }
        public string? Option { get; set; }

        public EfirOnFriday() { }

        public EfirOnFriday(TimeSpan timeToEfir, string eventName, string? description, string option)
        {
            this.TimeToEfir = timeToEfir;
            this.EventName = eventName;
            this.Description = description;
            this.Option = option;
        }
    }
}
