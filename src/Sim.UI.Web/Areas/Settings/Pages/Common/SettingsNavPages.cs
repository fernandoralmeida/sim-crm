using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Settings.Pages.Common
{
    public static class SettingsNavPages
    {
        public static string Municipio => "Index";
        public static string Unidade => "Unidade";
        public static string Setores => "Setor";
        public static string Eventos => "Evento";
        public static string Servicos => "Servico";
        public static string Parceiros => "Parceiro";
        public static string Canal => "Canal";
        public static string? MunicipioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Municipio);
        public static string? UnidadeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Unidade);
        public static string? SetoresNavClass(ViewContext viewContext) => PageNavClass(viewContext, Setores);
        public static string? EventosNavClass(ViewContext viewContext) => PageNavClass(viewContext, Eventos);
        public static string? ServicosNavClass(ViewContext viewContext) => PageNavClass(viewContext, Servicos);
        public static string? CanalNavClass(ViewContext viewContext) => PageNavClass(viewContext, Canal);
        public static string? ParceirosNavClass(ViewContext viewContext) => PageNavClass(viewContext, Parceiros);
        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageSettings"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
