using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoToWorkDatabaseImplement.Models
{
    public class Certificate
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int EngineerId { get; set; }

        public virtual Engineer Engineer { get; set; }

        [ForeignKey("CertificateId")]
        public virtual List<CertificateProduct> CertificateProducts { get; set; }
    }
}
