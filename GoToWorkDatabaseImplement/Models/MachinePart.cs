using System.ComponentModel.DataAnnotations;

namespace GoToWorkDatabaseImplement.Models
{
    public class MachinePart
    {
        public int Id { get; set; }

        public int PartId { get; set; }

        public int MachineId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Part Part { get; set; }

        public virtual Machine Machine { get; set; }
    }
}
