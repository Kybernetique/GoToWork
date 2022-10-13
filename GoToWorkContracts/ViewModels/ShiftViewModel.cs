using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ShiftViewModel
    {
        public int Id { get; set; }
        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        [DisplayName("Время дня")]
        public string DayTime { get; set; }
        [DisplayName("Список сотрудников")]
        public Dictionary<int, (string, int)> ShiftWorkers { get; set; }
    }
}
