using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using GoToWorkDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToWorkDatabaseImplement.Implements
{
    public class BossStorage : IBossStorage
    {
        public List<BossViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Bosses
                .Select(CreateModel).ToList();
            }
        }

        public List<BossViewModel> GetFilteredList(BossBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                return context.Bosses
                    .Where(rec => rec.FIO.Contains(model.FIO))
                    .Select(CreateModel).ToList();
            }
        }

        public BossViewModel GetElement(BossBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var boss = context.Bosses
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Login == model.Login);
                return boss != null ?
                CreateModel(boss) : null;
            }
        }

        public void Insert(BossBindingModel model)
        {
            using (var context = new Database())
            {
                context.Bosses.Add(CreateModel(model, new Boss()));
                context.SaveChanges();
            }
        }

        public void Update(BossBindingModel model)
        {
            using (var context = new Database())
            {
                var element = context.Bosses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Начальник не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(BossBindingModel model)
        {
            using (var context = new Database())
            {
                Boss element = context.Bosses.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Bosses.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Начальник не найден");
                }
            }
        }

        private Boss CreateModel(BossBindingModel model, Boss boss)
        {
            boss.FIO = model.FIO;
            boss.Login = model.Login;
            boss.Password = model.Password;
            return boss;
        }

        private BossViewModel CreateModel(Boss boss)
        {
            return new BossViewModel
            {
                Id = boss.Id,
                FIO = boss.FIO,
                Login = boss.Login,
                Password = boss.Password
            };
        }
    }
}
