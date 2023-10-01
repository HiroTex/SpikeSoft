using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSoft.UtilityManager
{
    public interface IXlsxReader
    {
        Dictionary<string, Dictionary<string, string>> ReadExcelData(string filePath);
    }

    class XlsxReader : IXlsxReader
    {
        public Dictionary<string, Dictionary<string, string>> ReadExcelData(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int lastColumn = worksheet.Dimension.End.Column;
                var data = new Dictionary<string, Dictionary<string, string>>();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    string sourceName = worksheet.Cells[row, 1].Text;
                    var structData = new Dictionary<string, string>();

                    for (int col = 2; col <= lastColumn; col++)
                    {
                        string header = worksheet.Cells[1, col].Text;
                        string value = worksheet.Cells[row, col].Text;
                        structData[header] = value;
                    }

                    data[sourceName] = structData;
                }

                return data;
            }
        }
    }

    // Define a class to encapsulate the logic of retrieving Excel data
    public class ExcelDataRetriever
    {
        private readonly IXlsxReader excelDataReader;

        public ExcelDataRetriever(IXlsxReader reader)
        {
            excelDataReader = reader;
        }

        public Dictionary<string, Dictionary<string, string>> RetrieveData(string filePath)
        {
            return excelDataReader.ReadExcelData(filePath);
        }
    }
}
