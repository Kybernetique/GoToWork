using GoToWorkBusinessLogic.HelperModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;
using GoToWorkBusinessLogic.OfficePackage;
using GoToWorkBusinessLogic.OfficePackage.HelperModels;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class ShiftReportLogic : IShiftReportLogic
    {
        private readonly IProductStorage _productStorage;

        private readonly IWorkerStorage _workerStorage;

        private readonly IShiftStorage _shiftStorage;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToPdf _saveToPdf;

        public ShiftReportLogic(IProductStorage productStorage, IWorkerStorage
      workerStorage, IShiftStorage shiftStorage, AbstractSaveToWord saveToWord,
            AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf)
        {
            _productStorage = productStorage;
            _workerStorage = workerStorage;
            _shiftStorage = shiftStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        public List<ReportShiftViewModel> GetProductShifts(List<ProductViewModel> products)
        {
            var workers = _workerStorage.GetFullList();
            var shifts = _shiftStorage.GetFullList();
            var list = new List<ReportShiftViewModel>();

            foreach (var product in products)
            {
                foreach (var worker in workers)
                {

                    if (product.ProductWorkers.ContainsKey(worker.Id))
                    {
                        foreach (var shift in shifts)
                        {
                            if (shift.ShiftWorkers.ContainsKey(worker.Id))
                            {

                                list.Add(new ReportShiftViewModel
                                {
                                    ProductName = product.Name,
                                    Date = shift.Date,
                                    DayTime = shift.DayTime
                                });
                            }
                        }
                    }
                }

            }
            return list;
        }

        public void SaveToWordFile(string fileName, List<ProductViewModel> products)
        {
            _saveToWord.CreateDocEngineer(new WordInfo
            {
                FileName = fileName,
                Title = "Список смен по изделиям",
                Shifts = GetProductShifts(products)
            });
        }

        public void SaveToExcelFile(string fileName, List<ProductViewModel> products)
        {
            _saveToExcel.CreateDocEngineer(new ExcelInfo
            {
                FileName = fileName,
                Title = "Список смен по изделиям",
                Shifts = GetProductShifts(products)
            });
        }

    }
}
