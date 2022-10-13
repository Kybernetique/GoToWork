using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface IPartLogic
    {
        List<PartViewModel> Read(PartBindingModel model);

        void CreateOrUpdate(PartBindingModel model);

        void Delete(PartBindingModel model);
    }
}
