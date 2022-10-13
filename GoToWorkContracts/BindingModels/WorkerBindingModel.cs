
namespace GoToWorkContracts.BindingModels
{
    public class WorkerBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal HourSalary { get; set; }
        public string Position { get; set; }
        public int BossId { get; set; }
    }
}
