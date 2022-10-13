using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class BossLogic : IBossLogic
    {
        private readonly IBossStorage _bossStorage;

        public BossLogic(IBossStorage bossStorage)
        {
            _bossStorage = bossStorage;
        }

        public List<BossViewModel> Read(BossBindingModel model)
        {
            if (model == null)
            {
                return _bossStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<BossViewModel> { _bossStorage.GetElement(model) };
            }
            return _bossStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(BossBindingModel model)
        {
            var boss = _bossStorage.GetElement(new BossBindingModel
            {
                Login = model.Login
            });
            if (boss != null && boss.Id != boss.Id)
            {
                throw new Exception("Уже есть такой пользователь");
            }
            if (model.Id.HasValue)
            {
                _bossStorage.Update(model);
            }
            else
            {
                _bossStorage.Insert(model);
            }
        }

        public void Delete(BossBindingModel model)

        {
            var boss = _bossStorage.GetElement(new BossBindingModel
            {
                Id = model.Id
            });
            if (boss == null)
            {
                throw new Exception("Руководитель не найден");
            }
            _bossStorage.Delete(model);
        }

        public int CheckPassword(string login, string password)
        {
            var boss = _bossStorage.GetElement(new BossBindingModel
            {
                Login = login
            });
            if (boss == null)
            {
                throw new Exception("Нет такого пользователя");
            }
            if (boss.Password != password)
            {
                throw new Exception("Неверный пароль");
            }
            return boss.Id;
        }

    }
}
