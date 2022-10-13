using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;

namespace GoToWorkContracts.StoragesContracts
{
    public interface IPartStorage
    {
        List<PartViewModel> GetFullList();
        List<PartViewModel> GetFilteredList(PartBindingModel model);
        PartViewModel GetElement(PartBindingModel model);
        void Insert(PartBindingModel model);
        void Update(PartBindingModel model);
        void Delete(PartBindingModel model);
    }
}
