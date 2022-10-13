using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using GoToWorkDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GoToWorkDatabaseImplement.Implements
{
    public class PartStorage : IPartStorage
    {
        public List<PartViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Parts
                .Select(CreateModel)
               .ToList();
            }
        }
        public List<PartViewModel> GetFilteredList(PartBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                return context.Parts
                .Where(rec => rec.Name.Contains(model.Name))
               .Select(CreateModel)
                .ToList();
            }
        }
        public PartViewModel GetElement(PartBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var part = context.Parts
                .FirstOrDefault(rec => rec.Name == model.Name ||
               rec.Id == model.Id);
                return part != null ?
                 CreateModel(part) :
               null;
            }
        }
        public void Insert(PartBindingModel model)
        {
            using (var context = new Database())
            {
                context.Parts.Add(CreateModel(model, new Part()));
                context.SaveChanges();
            }
        }
        public void Update(PartBindingModel model)
        {
            using (var context = new Database())
            {
                var part = context.Parts.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (part == null)
                {
                    throw new Exception("Деталь не найдена");
                }
                CreateModel(model, part);
                context.SaveChanges();
            }
        }
        public void Delete(PartBindingModel model)
        {
            using (var context = new Database())
            {
                var part = context.Parts.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (part != null)
                {
                    context.Parts.Remove(part);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Деталь не найдена");
                }
            }
        }

        private PartViewModel CreateModel(Part part)
        {
            return new PartViewModel
            {
                Id = part.Id,
                Name = part.Name,
                Cost = part.Cost
            };
        }
        private Part CreateModel(PartBindingModel model, Part part)
        {
            part.Name = model.Name;
            part.Cost = model.Cost;
            return part;
        }
    }
}
