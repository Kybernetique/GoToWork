using GoToWorkContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkContracts.BusinessLogicsContracts
{
    public interface IShiftReportLogic
    {
        List<ReportShiftViewModel> GetProductShifts(List<ProductViewModel> products);

        void SaveToWordFile(string fileName, List<ProductViewModel> products);

        void SaveToExcelFile(string fileName, List<ProductViewModel> products);
    }
}
