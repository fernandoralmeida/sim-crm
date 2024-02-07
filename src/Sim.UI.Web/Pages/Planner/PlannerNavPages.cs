using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Planner
{
    public static class PlannerNavPages
    {
        public static string Inicio => "Index";
        public static string Listar => "Listar";
        public static string Ajuda => "Ajuda";
        public static string? InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string? ListarNavClass(ViewContext viewContext) => PageNavClass(viewContext, Listar);
        public static string? AjudaNavClass(ViewContext viewContext) => PageNavClass(viewContext, Ajuda);
        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
