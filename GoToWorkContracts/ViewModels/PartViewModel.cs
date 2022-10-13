using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class PartViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        public string Name { get; set; }
        [DisplayName("Стоимость")]
        public decimal Cost { get; set; }
    }
}
