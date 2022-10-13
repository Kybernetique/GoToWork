using System.ComponentModel.DataAnnotations;

namespace GoToWorkDatabaseImplement.Models
{
    public class ProductPart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int PartId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Product Product { get; set; }

        public virtual Part Part { get; set; }
    }
}
