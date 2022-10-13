using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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
    public abstract class AbstractSaveToExcel
    {
        public void CreateDocEngineer(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var shift in info.Shifts)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = "Продукт " + shift.ProductName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = " Смена " + shift.DayTime,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "С",
                    RowIndex = rowIndex,
                    Text = " Работники " + shift.Date + " числа.",
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
            }
            SaveExcel(info);

        }

        public void CreateDocBoss(ExcelInfo info)
        {
            CreateExcel(info);
            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });
            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });
            uint rowIndex = 2;
            foreach (var certificate in info.Certificates)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = "Акт приемки " + certificate.CertificateName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "B",
                    RowIndex = rowIndex,
                    Text = "Стоимость " + certificate.Cost,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;
            }
            SaveExcel(info);

        }


        // Создание Excel-файла
        protected abstract void CreateExcel(ExcelInfo info);

        // Добавляем новую ячейку в лист
        protected abstract void InsertCellInWorksheet(ExcelCellParameters
        cellParameters);

        // Объединение ячеек
        protected abstract void MergeCells(ExcelMergeParameters mergeParameters);

        // Сохранение файла
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
