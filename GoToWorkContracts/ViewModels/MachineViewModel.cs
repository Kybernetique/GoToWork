using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GoToWorkContracts.ViewModels
{
    public class MachineViewModel
    {
        public int Id { get; set; }
        [DisplayName("Гарантия на станок")]
        public int Guarantee { get; set; }
        [DisplayName("Название станка")]
        public string Name { get; set; }
        public Dictionary<int, (string, int)> MachineWorkers { get; set; }
        public Dictionary<int, (string, int)> MachineParts { get; set; }
    }
}
