using GoToWorkBusinessLogic.HelperModels;
using GoToWorkContracts.StoragesContracts;
using GoToWorkContracts.ViewModels;
using System.Collections.Generic;
using GoToWorkContracts.BusinessLogicsContracts;
using GoToWorkBusinessLogic.OfficePackage;
using GoToWorkBusinessLogic.OfficePackage.HelperModels;

namespace GoToWorkBusinessLogic.BusinessLogics
{
    public class CertificateReportLogic : ICertificateReportLogic
    {
        private readonly IProductStorage _productStorage;

        private readonly IWorkerStorage _workerStorage;

        private readonly ICertificateStorage _certificateStorage;

        private readonly AbstractSaveToWord _saveToWord;

        private readonly AbstractSaveToExcel _saveToExcel;

        private readonly AbstractSaveToPdf _saveToPdf;

        public CertificateReportLogic(IProductStorage productStorage, IWorkerStorage
      workerStorage, ICertificateStorage certificateStorage, AbstractSaveToWord saveToWord,
            AbstractSaveToExcel saveToExcel, AbstractSaveToPdf saveToPdf)
        {
            _productStorage = productStorage;
            _workerStorage = workerStorage;
            _certificateStorage = certificateStorage;
            _saveToWord = saveToWord;
            _saveToExcel = saveToExcel;
            _saveToPdf = saveToPdf;
        }

        public List<ReportCertificateViewModel> GetWorkerCertificates(List<WorkerViewModel> workers)
        {
            var products = _productStorage.GetFullList();
            var certificates = _certificateStorage.GetFullList();

            var list = new List<ReportCertificateViewModel>();

            foreach (var worker in workers)
            {
                foreach (var product in products)
                {
                    if (product.ProductWorkers.ContainsKey(worker.Id))
                    {
                        foreach (var certificate in certificates)
                        {
                            if (certificate.CertificateProducts.ContainsKey(product.Id))
                            {
                                list.Add(new ReportCertificateViewModel
                                {
                                    WorkerName = worker.Name,
                                    CertificateName = certificate.Name,
                                    Cost = certificate.Cost
                                });
                            }
                        }
                    }
                }
            }
            return list;
        }

        public void SaveToWordFile(string fileName, List<WorkerViewModel> workers)
        {
            _saveToWord.CreateDocBoss(new WordInfo
            {
                FileName = fileName,
                Title = "Список актов по работникам",
                Certificates = GetWorkerCertificates(workers)
            });
        }

        public void SaveToExcelFile(string fileName, List<WorkerViewModel> workers)
        {
            _saveToExcel.CreateDocBoss(new ExcelInfo
            {
                FileName = fileName,
                Title = "Список актов по работникам",
                Certificates = GetWorkerCertificates(workers)
            });
        }

    }
}
