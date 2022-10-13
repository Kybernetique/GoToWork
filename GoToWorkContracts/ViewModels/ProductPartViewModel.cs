using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ProductPartViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название детали")]
        public string PartName { get; set; }
        [DisplayName("Количество")]
        public int PartCount { get; set; }
    }
}
