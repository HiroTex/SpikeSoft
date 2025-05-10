using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SpikeSoft.UtilityManager
{
    public class XlsxMan
    {
        string excelFilePath = "file.xlsx";

        public XlsxMan(string filePath)
        {
            excelFilePath = filePath;
        }

        /// <summary>
        /// Export Structs to Excel Data
        /// </summary>
        /// <typeparam name="T">Struct Type</typeparam>
        /// <param name="sourceFileNames">Struct Object Names</param>
        /// <param name="data">Structs with Data to Export</param>
        public void ExportToExcel<T>(List<string> sourceFileNames, List<T> data)
        {
            if (sourceFileNames == null || data == null || sourceFileNames.Count != data.Count)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Error: Invalid input data." });
                return;
            }

            try
            {
                using (SpreadsheetDocument spreadsheetDocument = CreateExcelWorkbook(typeof(T).Name))
                {
                    WorksheetPart worksheetPart = AddWorksheet(spreadsheetDocument);
                    FillHeaderRowWithData<T>(worksheetPart);
                    foreach (var item in data)
                    {
                        FillRowWithData(worksheetPart, sourceFileNames[data.IndexOf(item)], item);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Error exporting to Excel: " + ex.Message });
            }
        }

        public Dictionary<string,Dictionary<string,string>> ImportFromExcel()
        {
            // Dependency injection of the EPPlusExcelDataReader
            var excelDataReader = new XlsxReader();
            var dataRetriever = new ExcelDataRetriever(excelDataReader);

            var data = dataRetriever.RetrieveData(excelFilePath);

            return data;
        }

        #region ExportLogic
        private SpreadsheetDocument CreateExcelWorkbook(string sheetName)
        {
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(excelFilePath, SpreadsheetDocumentType.Workbook);

                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                // Create a new worksheet part
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                // Create a new sheet and associate it with the worksheet part
                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = sheetName
                };

                sheets.Append(sheet);

                return spreadsheetDocument;
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Error creating Excel workbook: " + ex.Message });
                throw;
            }
        }
        private WorksheetPart AddWorksheet(SpreadsheetDocument spreadsheetDocument)
        {
            try
            {
                // Find the first existing worksheet part
                var existingWorksheetPart = spreadsheetDocument.WorkbookPart.WorksheetParts.FirstOrDefault();
                if (existingWorksheetPart != null)
                {
                    return existingWorksheetPart;
                }

                // If no existing worksheet part is found, create a new one
                WorksheetPart worksheetPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                return worksheetPart;
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Error adding worksheet: " + ex.Message });
                throw;
            }
        }
        private void FillHeaderRowWithData<T>(WorksheetPart worksheetPart)
        {
            try
            {
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                if (sheetData == null)
                {
                    sheetData = new SheetData();
                    worksheetPart.Worksheet.AppendChild(sheetData);
                }

                Row headerRow = new Row();
                headerRow.Append(
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue("Source") }
                );

                FillHeaderForRowRecursive<T>(headerRow, typeof(T).GetFields());

                sheetData.Append(headerRow);
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Error filling header row with data: " + ex.Message });
                throw;
            }
        }
        private void FillHeaderForRowRecursive<T>(Row headerRow, FieldInfo[] fields, string prefix = "")
        {
            foreach (var field in fields)
            {
                if (field.FieldType.IsEnum)
                {
                    // Handle regular enum
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue($"{prefix}{field.Name}");
                    headerRow.Append(cell);
                }
                else if (field.FieldType.IsValueType && !field.FieldType.IsPrimitive && !field.FieldType.IsArray)
                {
                    // Handle sub-struct field
                    string subStructPrefix = $"{prefix}{field.Name}.";
                    FieldInfo[] subFields = field.FieldType.GetFields();
                    FillHeaderForRowRecursive<T>(headerRow, subFields, subStructPrefix);
                }
                else if (field.FieldType.IsArray)
                {
                    var constSizeAttribute = field.GetCustomAttribute<MarshalAsAttribute>();
                    if (constSizeAttribute != null)
                    {
                        if (!field.FieldType.GetElementType().IsPrimitive && !field.FieldType.GetElementType().IsEnum && field.FieldType.GetElementType().GetFields().Length > 0)
                        {
                            // Handle array of sub-struct
                            FieldInfo[] subFields = field.FieldType.GetElementType().GetFields();

                            // Handle array field with Marshal SizeConst
                            int numberOfColumnsForArray = constSizeAttribute.SizeConst;

                            for (int i = 0; i < numberOfColumnsForArray; i++)
                            {
                                string subStructPrefix = $"{prefix}{field.Name}[{i}].";
                                FillHeaderForRowRecursive<T>(headerRow, subFields, subStructPrefix);
                            }
                        }
                        else
                        {
                            // Handle array field with Marshal SizeConst
                            int numberOfColumnsForArray = constSizeAttribute.SizeConst;

                            for (int i = 0; i < numberOfColumnsForArray; i++)
                            {
                                Cell cell = new Cell();
                                cell.DataType = CellValues.String;
                                cell.CellValue = new CellValue($"{prefix}{field.Name}[{i}]");
                                headerRow.Append(cell);
                            }
                        }
                    }
                }
                else
                {
                    // Handle other types (e.g., primitive types)
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue($"{prefix}{field.Name}");
                    headerRow.Append(cell);
                }
            }
        }
        private void FillRowWithData<T>(WorksheetPart worksheetPart, string sourceFileName, T data)
        {
            try
            {
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                if (sheetData == null)
                {
                    sheetData = new SheetData();
                    worksheetPart.Worksheet.AppendChild(sheetData);
                }

                Row dataRow = new Row();
                dataRow.Append(
                    new Cell() { DataType = CellValues.String, CellValue = new CellValue(sourceFileName) }
                );

                FillBitFieldRowWithDataRecursive<T>(dataRow, data);

                sheetData.Append(dataRow);
            }
            catch (Exception ex)
            {
                ExceptionMan.ThrowMessage(0x2000, new string[] { "Error filling row with data: " + ex.Message });
                throw;
            }
        }
        private void FillBitFieldRowWithDataRecursive<T>(Row dataRow, T data, string prefix = "")
        {
            foreach (var field in data.GetType().GetFields())
            {
                if (field.FieldType.IsEnum)
                {
                    // Handle regular enum
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue($"{field.GetValue(data)?.ToString()}");
                    dataRow.Append(cell);
                }
                else if (field.FieldType.IsValueType && !field.FieldType.IsPrimitive && !field.FieldType.IsArray)
                {
                    // Handle sub-struct field
                    string subStructPrefix = $"{field.Name}.";
                    FillBitFieldRowWithDataRecursive(dataRow, (dynamic)field.GetValue(data), subStructPrefix);
                }
                else if (field.FieldType.IsArray)
                {
                    if (field.FieldType.GetElementType().IsEnum)
                    {
                        // Handle enum array
                        Array enumArray = (Array)field.GetValue(data);
                        foreach (var enumValue in enumArray)
                        {
                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(enumValue.ToString());
                            dataRow.Append(cell);
                        }
                    }
                    else if (field.FieldType.GetElementType().IsPrimitive)
                    {
                        // Handle array of primitives
                        foreach (var element in (Array)field.GetValue(data))
                        {
                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue($"{Convert.ToString(element, CultureInfo.InvariantCulture)}");
                            dataRow.Append(cell);
                        }
                    }
                    else
                    {
                        // Handle array of sub-struct
                        foreach (var element in (Array)field.GetValue(data))
                        {
                            FillBitFieldRowWithDataRecursive(dataRow, element, prefix);
                        }
                    }
                }
                else
                {
                    // Handle other types (e.g., primitive types)
                    Cell cell = new Cell();
                    cell.DataType = CellValues.String;
                    cell.CellValue = new CellValue($"{field.GetValue(data)?.ToString()}");
                    dataRow.Append(cell);
                }
            }
        }
        #endregion
    }
}
