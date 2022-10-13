using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;

namespace GoToWorkBusinessLogic.BusinessLogics
{
public class ShiftLogic : IShiftLogic
    {
        private readonly IShiftStorage _shiftStorage;
        public ShiftLogic(IShiftStorage shiftStorage)
        {
            _shiftStorage = shiftStorage;
        }

        public List<ShiftViewModel> Read(ShiftBindingModel model)
        {
            if (model == null)
            {
                return _shiftStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ShiftViewModel> { _shiftStorage.GetElement(model) };
            }
            return _shiftStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ShiftBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _shiftStorage.Update(model);
            }
            else
            {
                _shiftStorage.Insert(model);
            }
        }
        public void Delete(ShiftBindingModel model)
        {
            var shift = _shiftStorage.GetElement(new ShiftBindingModel
            {
                Id = model.Id
            });
            if (shift == null)
            {
                throw new Exception("Смена не найдена");
            }
            _shiftStorage.Delete(model);
        }

    }
}
