using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class PartLogic : IPartLogic
    {
        private readonly IPartStorage _partStorage;
        public PartLogic(IPartStorage partStorage)
        {
            _partStorage = partStorage;
        }

        public List<PartViewModel> Read(PartBindingModel model)
        {
            if (model == null)
            {
                return _partStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PartViewModel> { _partStorage.GetElement(model) };
            }
            return _partStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(PartBindingModel model)
        {
            var part = _partStorage.GetElement(new PartBindingModel
            {
                Name = model.Name
            });
            if (part != null && part.Id != model.Id)
            {
                throw new Exception("Уже есть такая деталь");
            }
            if (model.Id.HasValue)
            {
                _partStorage.Update(model);
            }
            else
            {
                _partStorage.Insert(model);
            }
        }

        public void Delete(PartBindingModel model)

        {
            var part = _partStorage.GetElement(new PartBindingModel
            {
                Id = model.Id
            });
            if (part == null)
            {
                throw new Exception("Деталь не найдена");
            }
            _partStorage.Delete(model);
        }

    }
}
