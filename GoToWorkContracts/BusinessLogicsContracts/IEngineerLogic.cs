using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface IEngineerLogic
    {
        List<EngineerViewModel> Read(EngineerBindingModel model);

        void CreateOrUpdate(EngineerBindingModel model);

        void Delete(EngineerBindingModel model);

        int CheckPassword(string login, string password);
    }
}
