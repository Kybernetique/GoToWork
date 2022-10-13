using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface IMachineLogic
    {
        List<MachineViewModel> Read(MachineBindingModel model);

        void CreateOrUpdate(MachineBindingModel model);

        void Delete(MachineBindingModel model);


    }
}
