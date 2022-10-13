using GoToWorkBusinessLogic.HelperModels;
using GoToWorkContracts.BindingModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;
using GoToWorkBusinessLogic.OfficePackage;
using GoToWorkBusinessLogic.OfficePackage.HelperModels;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class ReportProductShiftLogic : IReportProductShiftLogic
    {
        private readonly IShiftStorage _shiftStorage;

        private readonly IWorkerStorage _workerStorage;

        private readonly IProductStorage _productStorage;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportProductShiftLogic(IShiftStorage shiftStorage, IWorkerStorage workerStorage,
     IProductStorage productStorage, AbstractSaveToWord saveToWord,
            AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf)
        {
            _shiftStorage = shiftStorage;
            _workerStorage = workerStorage;
            _productStorage = productStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        public List<ReportProductShiftViewModel> GetProductShift(ReportProductShiftBindingModel model)
        {
            var shifts = _shiftStorage.GetFilteredList(new ShiftBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var workers = _workerStorage.GetFilteredList(new WorkerBindingModel
            {
                BossId = model.BossId
            });
            var products = _productStorage.GetFullList();

            var list = new List<ReportProductShiftViewModel>();

            foreach (var shift in shifts)
            {

                foreach (var worker in workers)
                {
                    if (shift.ShiftWorkers.ContainsKey(worker.Id))
                    {
                        foreach (var product in products)
                        {
                            if (product.ProductWorkers.ContainsKey(worker.Id))
                            {
                                list.Add(new ReportProductShiftViewModel
                                {
                                    DayTime = shift.DayTime,
                                    Date = shift.Date,
                                    WorkerName = worker.Name,
                                    ProductName = product.Name
                                });
                            }
                        }
                    }
                }
            }
            return list;
        }

        public void SaveToPdfFile(ReportProductShiftBindingModel model)
        {
            _saveToPdf.CreateDocBoss(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список актов и смен",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                ShiftsForBoss = GetProductShift(model)
            });
        }

    }
}
