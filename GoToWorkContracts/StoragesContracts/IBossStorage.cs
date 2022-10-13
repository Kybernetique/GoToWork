using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;

namespace GoToWorkContracts.StoragesContracts
{
    public interface IBossStorage
    {
        List<BossViewModel> GetFullList();
        List<BossViewModel> GetFilteredList(BossBindingModel model);
        BossViewModel GetElement(BossBindingModel model);
        void Insert(BossBindingModel model);
        void Update(BossBindingModel model);
        void Delete(BossBindingModel model);
    }
}
