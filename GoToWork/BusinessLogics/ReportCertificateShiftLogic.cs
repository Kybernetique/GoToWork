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
    public class ReportCertificateShiftLogic : IReportCertificateShiftLogic
    {
        private readonly IShiftStorage _shiftStorage;

        private readonly IWorkerStorage _workerStorage;

        private readonly IProductStorage _productStorage;

        private readonly ICertificateStorage _certificateStorage;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportCertificateShiftLogic(IShiftStorage shiftStorage, IWorkerStorage workerStorage,
     IProductStorage productStorage, ICertificateStorage certificateStorage, AbstractSaveToWord saveToWord,
            AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf)
        {
            _shiftStorage = shiftStorage;
            _workerStorage = workerStorage;
            _productStorage = productStorage;
            _certificateStorage = certificateStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        public List<ReportCertificateShiftViewModel> GetCertificateShift(ReportCertificateShiftBindingModel model)
        {
            var shifts = _shiftStorage.GetFilteredList(new ShiftBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            var workers = _workerStorage.GetFullList();
            var products = _productStorage.GetFullList();
            var certificates = _certificateStorage.GetFilteredList(new CertificateBindingModel
            {
                EngineerId = model.EngineerId
            });

            var list = new List<ReportCertificateShiftViewModel>();

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
                                foreach (var certificate in certificates)
                                {
                                    if (product.ProductWorkers.ContainsKey(worker.Id))
                                    {
                                        list.Add(new ReportCertificateShiftViewModel
                                        {
                                            DayTime = shift.DayTime,
                                            Date = shift.Date,
                                            CertificateName = certificate.Name,
                                            WorkerName = worker.Name,
                                            ProductName = product.Name
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public void SaveToPdfFile(ReportCertificateShiftBindingModel model)
        {
            _saveToPdf.CreateDocEngineer(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Отчет по актам и сменам",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                ShiftsForEngineer = GetCertificateShift(model)
            });
        }
    }
}
