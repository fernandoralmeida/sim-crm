using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Atendimento
{
    public static class AtendimentoNavPages
    {
        public static string Inicio => "Index";
        public static string Atendimento => "Iniciar";
        public static string Consulta => "Consulta";
        public static string Anonimo => "Anonimo";
        public static string? InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string? AtendiementoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Atendimento);
        public static string? ConsultaNavClass(ViewContext viewContext) => PageNavClass(viewContext, Consulta);
        public static string? AtendimentoAnonimoClass(ViewContext viewContext) => PageNavClass(viewContext, Anonimo);
        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
