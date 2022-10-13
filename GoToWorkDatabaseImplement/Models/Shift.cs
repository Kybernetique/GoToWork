using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoToWorkDatabaseImplement.Models
{
    public class Shift
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string DayTime { get; set; }

        [ForeignKey("ShiftId")]
        public virtual List<ShiftWorker> ShiftWorkers { get; set; }
    }
}
