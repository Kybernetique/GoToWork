
namespace GoToWorkDatabaseImplement.Models
{
    public class CertificateProduct
    {
        public int Id { get; set; }

        public int CertificateId { get; set; }

        public int ProductId { get; set; }

        public virtual Certificate Certificate { get; set; }

        public virtual Product Product { get; set; }

    }
}
