using System;

namespace GoToWorkContracts.BindingModels
{
    public class ReportCertificateShiftBindingModel
    {
        public string FileName { get; set; }
        public int EngineerId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
