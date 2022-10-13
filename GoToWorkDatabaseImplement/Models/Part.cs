using System.ComponentModel.DataAnnotations;

namespace GoToWorkDatabaseImplement.Models
{
    public class Part
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Cost { get; set; }
    }
}
