using DocumentFormat.OpenXml.Wordprocessing;
using GoToWorkBusinessLogic.OfficePackage.HelperEnums;

namespace GoToWorkBusinessLogic.HelperModels
{
    public class WordTextProperties
    {
        public string Size { get; set; }
        public bool Bold { get; set; }
        public WordJustificationType JustificationType { get; set; }
    }
}
