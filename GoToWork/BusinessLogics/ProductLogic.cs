using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductStorage _productStorage;
        public ProductLogic(IProductStorage productStorage)
        {
            _productStorage = productStorage;
        }

        public List<ProductViewModel> Read(ProductBindingModel model)
        {
            if (model == null)
            {
                return _productStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ProductViewModel> { _productStorage.GetElement(model) };
            }
            return _productStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ProductBindingModel model)
        {
            if (model.Id.HasValue)
            {
                _productStorage.Update(model);
            }
            else
            {
                _productStorage.Insert(model);
            }
        }
        public void Delete(ProductBindingModel model)
        {
            var product = _productStorage.GetElement(new ProductBindingModel
            {
                Id = model.Id
            });
            if (product == null)
            {
                throw new Exception("Изделие не найдено");
            }
            _productStorage.Delete(model);
        }

    }
}
