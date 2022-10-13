using System.ComponentModel.DataAnnotations;

namespace GoToWorkDatabaseImplement.Models
{
    public class ProductWorker
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int WorkerId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Product Product { get; set; }

        public virtual Worker Worker { get; set; }
    }
}
