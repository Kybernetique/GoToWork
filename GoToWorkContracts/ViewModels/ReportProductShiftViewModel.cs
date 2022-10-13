using System;
using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ReportProductShiftViewModel
    {
        [DisplayName("Название изделия")]
        public string ProductName { get; set; }
        [DisplayName("Имя работника")]
        public string WorkerName { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Время суток")]
        public string DayTime { get; set; }
    }
}
