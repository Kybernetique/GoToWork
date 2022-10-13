using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class EngineerLogic : IEngineerLogic
    {
        private readonly IEngineerStorage _engineerStorage;
        public EngineerLogic(IEngineerStorage engineerStorage)
        {
            _engineerStorage = engineerStorage;
        }

        public List<EngineerViewModel> Read(EngineerBindingModel model)
        {
            if (model == null)
            {
                return _engineerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<EngineerViewModel> { _engineerStorage.GetElement(model) };
            }
            return _engineerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(EngineerBindingModel model)
        {
            var engineer = _engineerStorage.GetElement(new EngineerBindingModel
            {
                Login = model.Login
            });
            if (engineer != null && engineer.Id != model.Id)
            {
                throw new Exception("Уже есть такой пользователь");
            }
            if (model.Id.HasValue)
            {
                _engineerStorage.Update(model);
            }
            else
            {
                _engineerStorage.Insert(model);
            }
        }

        public void Delete(EngineerBindingModel model)

        {
            var engineer = _engineerStorage.GetElement(new EngineerBindingModel
            {
                Id = model.Id
            });
            if (engineer == null)
            {
                throw new Exception("Инженер не найден");
            }
            _engineerStorage.Delete(model);
        }

        public int CheckPassword(string login, string password)
        {
            var engineer = _engineerStorage.GetElement(new EngineerBindingModel
            {
                Login = login
            });
            if (engineer == null)
            {
                throw new Exception("Нет такого пользователя");
            }
            if (engineer.Password != password)
            {
                throw new Exception("Неверный пароль");
            }
            return engineer.Id;
        }

    }
}
