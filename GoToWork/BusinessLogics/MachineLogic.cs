using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class MachineLogic : IMachineLogic
    {
        private readonly IMachineStorage _machineStorage;
        public MachineLogic(IMachineStorage machineStorage)
        {
            _machineStorage = machineStorage;
        }

        public List<MachineViewModel> Read(MachineBindingModel model)
        {
            if (model == null)
            {
                return _machineStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<MachineViewModel> { _machineStorage.GetElement(model) };
            }
            return _machineStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(MachineBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _machineStorage.Update(model);
            }
            else
            {
                _machineStorage.Insert(model);
            }
        }
        public void Delete(MachineBindingModel model)
        {
            var machine = _machineStorage.GetElement(new MachineBindingModel
            {
                Id = model.Id
            });
            if (machine == null)
            {
                throw new Exception("Станок не найден");
            }
            _machineStorage.Delete(model);
        }

    }
}
