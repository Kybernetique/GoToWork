using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class ShiftWorkerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя сотрудника")]
        public string WorkerName { get; set; }
    }
}
