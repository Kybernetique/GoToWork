using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class WorkerLogic : IWorkerLogic
    {
        private readonly IWorkerStorage _workerStorage;
        public WorkerLogic(IWorkerStorage workerStorage)
        {
            _workerStorage = workerStorage;
        }

        public List<WorkerViewModel> Read(WorkerBindingModel model)
        {
            if (model == null)
            {
                return _workerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WorkerViewModel> { _workerStorage.GetElement(model) };
            }
            return _workerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WorkerBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _workerStorage.Update(model);
            }
            else
            {
                _workerStorage.Insert(model);
            }
        }
        public void Delete(WorkerBindingModel model)
        {
            var worker = _workerStorage.GetElement(new WorkerBindingModel
            {
                Id = model.Id
            });
            if (worker == null)
            {
                throw new Exception("Работник не найден");
            }
            _workerStorage.Delete(model);
        }

    }
}
