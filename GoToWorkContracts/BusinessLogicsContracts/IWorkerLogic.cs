using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface IWorkerLogic
    {
        List<WorkerViewModel> Read(WorkerBindingModel model);

        void CreateOrUpdate(WorkerBindingModel model);

        void Delete(WorkerBindingModel model);
    }
}
