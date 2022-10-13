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
    public class WorkerStorage : IWorkerStorage
    {
        public List<WorkerViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Workers
                    .Include(rec => rec.Boss)
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public List<WorkerViewModel> GetFilteredList(WorkerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                return context.Workers
                    .Include(rec => rec.Boss)
                    .Where(rec => rec.BossId == model.BossId)
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public WorkerViewModel GetElement(WorkerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                var worker = context.Workers
                    .Include(rec => rec.Boss)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return worker != null ?
                    CreateModel(worker) :
                    null;
            }
        }

        public void Insert(WorkerBindingModel model)
        {
            using (var context = new Database())
            {
                context.Workers.Add(CreateModel(model, new Worker()));
                context.SaveChanges();
            }
        }

        public void Update(WorkerBindingModel model)
        {
            using (var context = new Database())
            {
                var worker = context.Workers.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (worker == null)
                {
                    throw new Exception("Работник не найден");
                }
                CreateModel(model, worker);
                context.SaveChanges();
            }
        }

        public void Delete(WorkerBindingModel model)
        {
            using (var context = new Database())
            {
                var worker = context.Workers.FirstOrDefault(rec => rec.Id == model.Id);

                if (worker == null)
                {
                    throw new Exception("Работник не найден");
                }

                context.Workers.Remove(worker);
                context.SaveChanges();
            }
        }

        private WorkerViewModel CreateModel(Worker worker)
        {
            return new WorkerViewModel
            {
                Id = worker.Id,
                Name = worker.Name,
                HourSalary = worker.HourSalary,
                Position = worker.Position,
                BossId = worker.BossId,
                BossName = worker.Boss?.FIO
            };

        }

        private Worker CreateModel(WorkerBindingModel model, Worker worker)
        {
            worker.Name = model.Name;
            worker.HourSalary = model.HourSalary;
            worker.Position = model.Position;
            worker.BossId = model.BossId;
            return worker;
        }
    }
}
