using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class StatisticByShiftViewModel
    {
        [DisplayName("Дата")]
        public string Date { get; set; }
        [DisplayName("Денег за час")]
        public decimal WorkerHourSalary { get; set; }
    }
}
