using System;
using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ReportCertificateShiftViewModel
    {
        [DisplayName("Акт приемки")]
        public string CertificateName { get; set; }
        [DisplayName("Название изделия")]
        public string ProductName { get; set; }
        [DisplayName("Имя сотрудника")]
        public string WorkerName { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Время суток")]
        public string DayTime { get; set; }
    }
}
