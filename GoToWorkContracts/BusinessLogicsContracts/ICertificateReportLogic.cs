using GoToWorkContracts.ViewModels;
using System.Collections.Generic;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface ICertificateReportLogic
    {
        List<ReportCertificateViewModel> GetWorkerCertificates(List<WorkerViewModel> workers);

        void SaveToWordFile(string fileName, List<WorkerViewModel> workers);

        void SaveToExcelFile(string fileName, List<WorkerViewModel> workers);
    }
}
