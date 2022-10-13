using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using GoToWorkDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToWorkDatabaseImplement.Implements
{
    public class MachineStorage : IMachineStorage
    {
        public List<MachineViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Machines
                    .Include(rec => rec.MachineWorkers)
                    .ThenInclude(rec => rec.Worker)
                    .Include(rec => rec.MachineParts)
                    .ThenInclude(rec => rec.Part)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public List<MachineViewModel> GetFilteredList(MachineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                return context.Machines
                    .Include(rec => rec.MachineWorkers)
                    .ThenInclude(rec => rec.Worker)
                    .Include(rec => rec.MachineParts)
                    .ThenInclude(rec => rec.Part)
                    .Where(rec => rec.Id == model.Id)
                    .Select(CreateModel)
                    .ToList();
            }
        }
        public MachineViewModel GetElement(MachineBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using (var context = new Database())
            {
                var machine = context.Machines
                     .Include(rec => rec.MachineWorkers)
                    .ThenInclude(rec => rec.Worker)
                    .Include(rec => rec.MachineParts)
                    .ThenInclude(rec => rec.Part)
                    .FirstOrDefault(rec => rec.Id == model.Id);

                return machine != null ?
                    CreateModel(machine) :
                    null;
            }
        }
        public void Insert(MachineBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Machine(), context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Update(MachineBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var machine = context.Machines.FirstOrDefault(rec => rec.Id == model.Id);

                        if (machine == null)
                        {
                            throw new Exception("Станок не найден");
                        }

                        CreateModel(model, machine, context);
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public void Delete(MachineBindingModel model)
        {
            using (var context = new Database())
            {
                var machine = context.Machines.FirstOrDefault(rec => rec.Id == model.Id);

                if (machine == null)
                {
                    throw new Exception("Станок не найден");
                }

                context.Machines.Remove(machine);
                context.SaveChanges();
            }
        }
        private MachineViewModel CreateModel(Machine machine)
        {
            return new MachineViewModel
            {
                Id = machine.Id,
                Guarantee = machine.Guarantee,
                Name = machine.Name,


                MachineWorkers = machine.MachineWorkers
                            .ToDictionary(rec => rec.WorkerId,
                            rec => (rec.Worker?.Name, rec.Count)),

                MachineParts = machine.MachineParts
                            .ToDictionary(rec => rec.PartId,
                            rec => (rec.Part?.Name, rec.Count))

            };

        }

        private Machine CreateModel(MachineBindingModel model, Machine machine, Database context)
        {
            machine.Guarantee = model.Guarantee;
            machine.Name = model.Name;

            if (machine.Id == 0)
            {
                context.Machines.Add(machine);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var machineWorkers = context.MachineWorkers
                    .Where(rec => rec.MachineId == model.Id.Value)
                    .ToList();

                context.MachineWorkers.RemoveRange(machineWorkers.ToList());

                var machineParts = context.MachineParts
                    .Where(rec => rec.MachineId == model.Id.Value)
                    .ToList();

                context.MachineParts.RemoveRange(machineParts.ToList());

                context.SaveChanges();
            }

            foreach (var machineWorkers in model.MachineWorkers)
            {
                context.MachineWorkers.Add(new MachineWorker
                {
                    MachineId = machine.Id,
                    WorkerId = machineWorkers.Key,
                    Count = machineWorkers.Value.Item2
                });
                context.SaveChanges();
            }

            foreach (var machinePart in model.MachineParts)
            {
                context.MachineParts.Add(new MachinePart
                {
                    MachineId = machine.Id,
                    PartId = machinePart.Key,
                    Count = machinePart.Value.Item2
                });
                context.SaveChanges();
            }
            return machine;
        }
    }
}
