using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface ICertificateLogic
    {
        List<CertificateViewModel> Read(CertificateBindingModel model);

        void CreateOrUpdate(CertificateBindingModel model);

        void Delete(CertificateBindingModel model);
    }
}
