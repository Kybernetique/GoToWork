using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class MachineWorkerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя сотрудника")]
        public string WorkerName { get; set; }
        [DisplayName("Количество часов")]
        public int WorkerCount { get; set; }
    }
}
