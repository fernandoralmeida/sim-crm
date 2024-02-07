using OfficeOpenXml;

namespace Sim.Application.Helpers;
   
    public class ExportFile
    {
        public static async Task<(MemoryStream StreamFile, string ContentType, string Name)> ToExcel<T>(IEnumerable<T> list, string filename){
            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var epackage = new ExcelPackage(stream);
            var worksheet = epackage.Workbook.Worksheets.Add("Lista");
            worksheet.Cells.LoadFromCollection(list, true);
            await epackage.SaveAsync();

            stream.Position = 0;
            string excelname = $"{filename}.xlsx";

            return (stream, "application/vnd.openxmlformat-officedocument.spreadsheetml.sheet", excelname);
        }
    }