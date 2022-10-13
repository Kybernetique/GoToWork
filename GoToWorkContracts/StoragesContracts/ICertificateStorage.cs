using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;

namespace GoToWorkContracts.StoragesContracts
{
    public interface ICertificateStorage
    {
        List<CertificateViewModel> GetFullList();
        List<CertificateViewModel> GetFilteredList(CertificateBindingModel model);
        CertificateViewModel GetElement(CertificateBindingModel model);
        void Insert(CertificateBindingModel model);
        void Update(CertificateBindingModel model);
        void Delete(CertificateBindingModel model);
    }
}
