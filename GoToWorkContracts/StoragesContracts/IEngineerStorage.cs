using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;

namespace GoToWorkContracts.StoragesContracts
{
    public interface IEngineerStorage
    {
        List<EngineerViewModel> GetFullList();
        List<EngineerViewModel> GetFilteredList(EngineerBindingModel model);
        EngineerViewModel GetElement(EngineerBindingModel model);
        void Insert(EngineerBindingModel model);
        void Update(EngineerBindingModel model);
        void Delete(EngineerBindingModel model);
    }
}
