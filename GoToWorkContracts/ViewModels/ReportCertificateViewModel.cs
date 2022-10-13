using System;
using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ReportCertificateViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя работника")]
        public string WorkerName { get; set; }
        [DisplayName("Акт приемки")]
        public string CertificateName { get; set; }
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }
    }
}
