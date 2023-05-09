using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Efir.Model
{
    public class EfirOnWednesday
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкрементное свойство
        public int Id { get; set; }
        public TimeSpan TimeToEfir { get; set; }
        public string EventName { get; set; }
        public string? Description { get; set; }
        public string? Option { get; set; }

        public EfirOnWednesday() { }

        public EfirOnWednesday(TimeSpan timeToEfir, string eventName, string? description, string option)
        {
            this.TimeToEfir = timeToEfir;
            this.EventName = eventName;
            this.Description = description;
            this.Option = option;
        }
    }
}
