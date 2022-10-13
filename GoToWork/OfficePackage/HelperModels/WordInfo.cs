using GoToWorkContracts.ViewModels;
using System.Collections.Generic;

namespace GoToWorkBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportShiftViewModel> Shifts { get; set; }

        public List<ReportCertificateViewModel> Certificates { get; set; }
    }
}
