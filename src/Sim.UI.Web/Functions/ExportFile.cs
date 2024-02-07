using OfficeOpenXml;
using System.ComponentModel;

namespace Sim.UI.Web.Functions
{
    public class ExportFile
    {
        public async Task<(MemoryStream StremFile, string ContentType, string Name)> ToExcel<T>(IEnumerable<T> list, string filename){
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
}
