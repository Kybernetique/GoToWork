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
    public class ShiftStorage : IShiftStorage
    {
        public List<ShiftViewModel> GetFullList()
        {
            using (var context = new Database())
            {
                return context.Shifts
                    .Include(rec => rec.ShiftWorkers)
                    .ThenInclude(rec => rec.Worker)
                    .Select(CreateModel)
                    .ToList();
            }
        }

        public List<ShiftViewModel> GetFilteredList(ShiftBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                return context.Shifts
                     .Include(rec => rec.ShiftWorkers)
                     .ThenInclude(rec => rec.Worker)
                     .Where(rec => rec.Date >= model.DateFrom && rec.Date <= model.DateTo)
                    .Select(CreateModel).ToList();
            }
        }

        public ShiftViewModel GetElement(ShiftBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new Database())
            {
                var shift = context.Shifts
                     .Include(rec => rec.ShiftWorkers)
                     .ThenInclude(rec => rec.Worker)
                     .FirstOrDefault(rec => rec.Id == model.Id);
                return shift != null ?
                CreateModel(shift) : null;
            }
        }

        public void Insert(ShiftBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        CreateModel(model, new Shift(), context);
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

        public void Update(ShiftBindingModel model)
        {
            using (var context = new Database())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var shift = context.Shifts.FirstOrDefault(rec => rec.Id == model.Id);

                        if (shift == null)
                        {
                            throw new Exception("Смена не найдена");
                        }

                        CreateModel(model, shift, context);
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

        public void Delete(ShiftBindingModel model)
        {
            using (var context = new Database())
            {
                Shift element = context.Shifts.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Shifts.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Смена не найдено");
                }
            }
        }

        private Shift CreateModel(ShiftBindingModel model, Shift shift, Database context)
        {
            shift.Date = model.Date;
            shift.DayTime = model.DayTime;

            if (shift.Id == 0)
            {
                context.Shifts.Add(shift);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var shiftWorkers = context.ShiftWorkers
                     .Where(rec => rec.ShiftId == model.Id.Value)
                     .ToList();

                context.ShiftWorkers.RemoveRange(shiftWorkers.ToList());

                context.SaveChanges();
            }

            foreach (var shiftWorker in model.ShiftWorkers)
            {
                context.ShiftWorkers.Add(new ShiftWorker
                {
                    ShiftId = shift.Id,
                    WorkerId = shiftWorker.Key,
                    Count = shiftWorker.Value.Item2
                });
                context.SaveChanges();
            }
            return shift;
        }

        private ShiftViewModel CreateModel(Shift shift)
        {
            return new ShiftViewModel
            {
                Id = shift.Id,
                Date = shift.Date,
                DayTime = shift.DayTime,
                ShiftWorkers = shift.ShiftWorkers
                            .ToDictionary(rec => rec.WorkerId,
                            rec => (rec.Worker?.Name, rec.Count)),
            };
        }
    }
}
