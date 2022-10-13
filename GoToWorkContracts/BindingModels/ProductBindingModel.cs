using System.Collections.Generic;

namespace GoToWorkContracts.BindingModels
{
    public class ProductBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public Dictionary<int, (string, int)> ProductWorkers { get; set; }
        public Dictionary<int, (string, int)> ProductParts { get; set; }
        public Dictionary<int, string> CertificateProduct { get; set; }
    }
}
