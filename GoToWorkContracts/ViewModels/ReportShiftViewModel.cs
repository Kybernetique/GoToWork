using System;
using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ReportShiftViewModel
    {
        int Id { get; set; }
        [DisplayName("Название продукта")]
        public string ProductName { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Время суток")]
        public string DayTime { get; set; }
    }
}
