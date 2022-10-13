using System.ComponentModel.DataAnnotations;

namespace GoToWorkDatabaseImplement.Models
{
    public class MachineWorker
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }

        public int MachineId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Worker Worker { get; set; }

        public virtual Machine Machine { get; set; }
    }
}
