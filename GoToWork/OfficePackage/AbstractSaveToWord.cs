using GoToWorkBusinessLogic.HelperModels;
using GoToWorkBusinessLogic.OfficePackage.HelperEnums;
using GoToWorkBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;

namespace GoToWorkBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDocEngineer(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var shift in info.Shifts)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> 
                    { ($"Изделие \"{shift.ProductName}\" во время {shift.DayTime} смены были изготовлены {shift.Date} числа.", new WordTextProperties { Size = "24", Bold = false }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }

        public void CreateDocBoss(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var certificate in info.Certificates)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> { ($"Акт {certificate.CertificateName} стоимость {certificate.Cost} ", new WordTextProperties { Size = "24", Bold = false }) },
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);
        }

        // Создание doc-файла
        protected abstract void CreateWord(WordInfo info);

        // Создание абзаца с текстом
        protected abstract void CreateParagraph(WordParagraph paragraph);

        // Сохранение файла
        protected abstract void SaveWord(WordInfo info);
    }
}
