using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ProductWorkerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя сотрудника")]
        public string WorkerName { get; set; }
        [DisplayName("Кол-во часов")]
        public int WorkerCount { get; set; }
    }
}
