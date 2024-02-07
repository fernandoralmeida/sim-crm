using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Cliente
{
    public static class PessoaNavPages
    {
        public static string Inicio => "Index";
        public static string ConsultaNome => "Consulta.nome";
        public static string ConsultaCpf => "Consulta.cpf";
        public static string Relatórios => "Report";
        public static string? InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string? ConsultaNomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, ConsultaNome);
        public static string? ConsultaCPFNavClass(ViewContext viewContext) => PageNavClass(viewContext, ConsultaCpf);
        public static string? ReportNavClass(ViewContext viewContext) => PageNavClass(viewContext, Relatórios);
        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
