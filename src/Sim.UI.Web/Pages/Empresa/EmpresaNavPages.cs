using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Empresa
{
    public static class EmpresaNavPages
    {
        public static string Inicio => "Index";
        public static string Consulta => "Consulta";
        public static string RFB => "RFB";
        public static string? InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string? ConsultaNomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Consulta);
        public static string? RFBNavClass(ViewContext viewContext) => PageNavClass(viewContext, RFB);
        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageEmp"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
