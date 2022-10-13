using System.Collections.Generic;
using GoToWorkBusinessLogic.OfficePackage.HelperEnums;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace GoToWorkBusinessLogic.HelperModels
{
    public class PdfRowParameters
    {
        public Table Table { get; set; }
        public List<string> Texts { get; set; }
        public string Style { get; set; }
        public PdfParagraphAlignmentType ParagraphAlignment { get; set; }
    }
}
