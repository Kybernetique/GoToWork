using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;

namespace GoToWorkContracts.StoragesContracts
{
    public interface IMachineStorage
    {
        List<MachineViewModel> GetFullList();
        List<MachineViewModel> GetFilteredList(MachineBindingModel model);
        MachineViewModel GetElement(MachineBindingModel model);
        void Insert(MachineBindingModel model);
        void Update(MachineBindingModel model);
        void Delete(MachineBindingModel model);
    }
}
