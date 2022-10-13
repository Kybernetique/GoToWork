using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using GoToWorkDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToWorkDatabaseImplement.Implements
{
    public class EngineerStorage : IEngineerStorage
    {
        public List<EngineerViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Engineers
                .Select(CreateModel).ToList();
            }
        }

        public List<EngineerViewModel> GetFilteredList(EngineerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                return context.Engineers
                    .Where(rec => rec.FIO.Contains(model.FIO))
                    .Select(CreateModel).ToList();
            }
        }

        public EngineerViewModel GetElement(EngineerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var engineer = context.Engineers
                .FirstOrDefault(rec => rec.Id == model.Id || rec.Login == model.Login);
                return engineer != null ?
                CreateModel(engineer) : null;
            }
        }

        public void Insert(EngineerBindingModel model)
        {
            using (var context = new Database())
            {
                context.Engineers.Add(CreateModel(model, new Engineer()));
                context.SaveChanges();
            }
        }

        public void Update(EngineerBindingModel model)
        {
            using (var context = new Database())
            {
                var element = context.Engineers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Инженер не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }

        public void Delete(EngineerBindingModel model)
        {
            using (var context = new Database())
            {
                Engineer element = context.Engineers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Engineers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Инженер не найден");
                }
            }
        }

        private Engineer CreateModel(EngineerBindingModel model, Engineer engineer)
        {
            engineer.FIO = model.FIO;
            engineer.Login = model.Login;
            engineer.Password = model.Password;
            return engineer;
        }

        private EngineerViewModel CreateModel(Engineer engineer)
        {
            return new EngineerViewModel
            {
                Id = engineer.Id,
                FIO = engineer.FIO,
                Login = engineer.Login,
                Password = engineer.Password
            };
        }
    }
}
