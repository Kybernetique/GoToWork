using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoToWorkDatabaseImplement.Models
{
    public class Machine
    {
        public int Id { get; set; }

        [Required]
        public int Guarantee { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("MachineId")]
        public virtual List<MachineWorker> MachineWorkers { get; set; }

        [ForeignKey("MachineId")]
        public virtual List<MachinePart> MachineParts { get; set; }
    }
}
