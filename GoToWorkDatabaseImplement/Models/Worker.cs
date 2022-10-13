using System.ComponentModel.DataAnnotations;

namespace GoToWorkDatabaseImplement.Models
{
    public class Worker
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal HourSalary { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public int BossId { get; set; }

        public virtual Boss Boss { get; set; }
    }
}
