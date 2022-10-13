using System;
using System.Collections.Generic;

namespace GoToWorkContracts.BindingModels
{
    public class ShiftBindingModel
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public string DayTime { get; set; }
        public Dictionary<int, (string, int)> ShiftWorkers { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
