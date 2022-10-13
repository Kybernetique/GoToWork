using System.ComponentModel.DataAnnotations;
namespace GoToWorkDatabaseImplement.Models
{
    public class ShiftWorker
    {
        public int Id { get; set; }

        public int ShiftId { get; set; }

        public int WorkerId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Shift Shift { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
