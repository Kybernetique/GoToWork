using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class CertificateProductViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название изделия")]
        public string ProductName { get; set; }
    }
}
