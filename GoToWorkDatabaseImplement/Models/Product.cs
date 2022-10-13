using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoToWorkDatabaseImplement.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<ProductWorker> ProductWorkers { get; set; }

        [ForeignKey("ProductId")]
        public virtual List<ProductPart> ProductParts { get; set; }
    }
}
