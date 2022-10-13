using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface IReportCertificateShiftLogic
    {
        public List<ReportCertificateShiftViewModel> GetCertificateShift(ReportCertificateShiftBindingModel model);

        public void SaveToPdfFile(ReportCertificateShiftBindingModel model);
    }
}
