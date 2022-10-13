using GoToWorkBusinessLogic.HelperModels;
using GoToWorkBusinessLogic.OfficePackage.HelperEnums;
using GoToWorkBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoToWorkBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDocEngineer(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });

            CreateTable(new List<string> { "3cm", "6cm", "3cm", "3cm", "3cm" }); // или "4cm", "3cm", "3cm", "4cm", "3cm"
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Название акта", "Название изделия", "Имя работника",
                                           "Стоимость", "Дата" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var shift in info.ShiftsForEngineer)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { shift.CertificateName,
                        shift.ProductName,
                        shift.WorkerName, 
                        shift.DayTime, 
                        shift.Date.ToShortDateString()
                    },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }

        public void CreateDocBoss(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }",
                Style = "Normal"
            });
            CreateTable(new List<string> { "5cm", "3cm", "5cm", "4cm" }); // или "3cm", "6cm", "3cm", "2cm", "3cm"};
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Название изделия",
                    "Имя работника",
                    "Дата",
                    "Время суток" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var shift in info.ShiftsForBoss)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { 
                        shift.ProductName,
                        shift.WorkerName, 
                        shift.Date.ToShortDateString(),
                        shift.DayTime
                    },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }
            SavePdf(info);
        }

        // Создание doc-файла
        protected abstract void CreatePdf(PdfInfo info);

        // Создание параграфа с текстом
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        // Создание таблицы
        protected abstract void CreateTable(List<string> columns);

        protected abstract void CreateRow(PdfRowParameters rowParameters);

        // Сохранение файла
        protected abstract void SavePdf(PdfInfo info);
    }
}
