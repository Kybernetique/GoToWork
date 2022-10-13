using System.Collections.Generic;
using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }
        public Dictionary<int, (string, int)> ProductWorkers { get; set; }
        public Dictionary<int, (string, int)> ProductParts { get; set; }
    }
}
