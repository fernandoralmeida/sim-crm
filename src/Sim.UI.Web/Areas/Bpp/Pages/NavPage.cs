using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Bpp.Pages;

public static class NavPage {
    public static string Inicio => "Index";
    public static string Novo => "Novo";
    public static string Edit => "Edit";
    public static string Consulta => "Consulta";
    public static string Aprovados => "Aprovados";
    public static string Inadimplentes => "Inadimplentes";
    public static string Renegociados => "Renegociados";
    public static string Recusados => "Recusados";
    public static string Liquidados => "Liquidados";
    public static string InicioBPClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
    public static string NovoBPClass(ViewContext viewContext) => PageNavClass(viewContext, Novo);
    public static string EditBPClass(ViewContext viewContext) => PageNavClass(viewContext, Edit);
    public static string ConsultaBPClass(ViewContext viewContext) => PageNavClass(viewContext, Consulta);
    public static string AprovadosBPClass(ViewContext viewContext) => PageNavClass(viewContext, Aprovados);
    public static string InadimplentesBPClass(ViewContext viewContext) => PageNavClass(viewContext, Inadimplentes);
    public static string RenegociadosBPClass(ViewContext viewContext) => PageNavClass(viewContext, Renegociados);
    public static string RecusadosBPClass(ViewContext viewContext) => PageNavClass(viewContext, Recusados);
    public static string LiquidadosBPClass(ViewContext viewContext) => PageNavClass(viewContext, Liquidados);
    private static string PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePageBPP"] as string
            ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null!;
    }
}