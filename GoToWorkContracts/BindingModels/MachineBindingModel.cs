using System.Collections.Generic;

namespace GoToWorkContracts.BindingModels
{
    public class MachineBindingModel
    {
        public int? Id { get; set; }
        public int Guarantee { get; set; }
        public string Name { get; set; }
        public Dictionary<int, (string, int)> MachineWorkers { get; set; }
        public Dictionary<int, (string, int)> MachineParts { get; set; }
    }
}
