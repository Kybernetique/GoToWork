using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;

namespace GoToWorkContracts.StoragesContracts
{
    public interface IShiftStorage
    {
        List<ShiftViewModel> GetFullList();
        List<ShiftViewModel> GetFilteredList(ShiftBindingModel model);
        ShiftViewModel GetElement(ShiftBindingModel model);
        void Insert(ShiftBindingModel model);
        void Update(ShiftBindingModel model);
        void Delete(ShiftBindingModel model);
    }
}
