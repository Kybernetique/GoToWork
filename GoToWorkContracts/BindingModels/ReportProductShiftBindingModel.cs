using System;

namespace GoToWorkContracts.BindingModels
{
    public class ReportProductShiftBindingModel
    {
        public string FileName { get; set; }
        public int BossId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
