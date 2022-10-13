using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class WorkerViewModel
    {
        public int Id { get; set; }
        [DisplayName("Имя сотрудника")]
        public string Name { get; set; }
        [DisplayName("Зарплата в час")]
        public decimal HourSalary { get; set; }
        [DisplayName("Должность")]
        public string Position { get; set; }
        public int BossId { get; set; }
        [DisplayName("Имя начальника")]
        public string BossName { get; set; }
    }
}
