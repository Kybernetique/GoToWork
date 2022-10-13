using System;
using System.Collections.Generic;

namespace GoToWorkContracts.BindingModels
{
    public class CertificateBindingModel
    {
        public int? Id { get; set; }
        public int EngineerId { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<int, string> CertificateProducts { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
