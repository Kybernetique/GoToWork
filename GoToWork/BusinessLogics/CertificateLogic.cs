using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class CertificateLogic : ICertificateLogic
    {
        private readonly ICertificateStorage _certificateStorage;

        public CertificateLogic(ICertificateStorage certificateStorage)
        {
            _certificateStorage = certificateStorage;
        }

        public List<CertificateViewModel> Read(CertificateBindingModel model)
        {
            if (model == null)
            {
                return _certificateStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CertificateViewModel> { _certificateStorage.GetElement(model) };
            }
            return _certificateStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(CertificateBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _certificateStorage.Update(model);
            }
            else
            {
                _certificateStorage.Insert(model);
            }
        }
        public void Delete(CertificateBindingModel model)
        {
            var certificate = _certificateStorage.GetElement(new CertificateBindingModel
            {
                Id = model.Id
            });
            if (certificate == null)
            {
                throw new Exception("Акт не найден");
            }
            _certificateStorage.Delete(model);
        }

    }
}
