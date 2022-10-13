using GoToWorkContracts.BindingModels;
using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface IReportProductShiftLogic
    {
        List<ReportProductShiftViewModel> GetProductShift(ReportProductShiftBindingModel model);

        void SaveToPdfFile(ReportProductShiftBindingModel model);
    }
}
