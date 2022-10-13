using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class StatisticByCertificateViewModel
    {
        [DisplayName("Название акта приемки")]
        public string CertificateName { get; set; }

        [DisplayName("Кол-во изделий")]
        public int ProductCount { get; set; }
    }
}
