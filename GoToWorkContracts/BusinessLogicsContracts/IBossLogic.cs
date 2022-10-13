using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface IBossLogic
    {
        List<BossViewModel> Read(BossBindingModel model);

        void CreateOrUpdate(BossBindingModel model);

        void Delete(BossBindingModel model);

        int CheckPassword(string login, string password);
    }
}
