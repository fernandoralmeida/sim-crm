using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Areas.Admin.Pages.Manager
{
    public static class AdminNavPages
    {
        public static string Index => "Index";
        public static string Lock => "Lock";
        public static string Roles => "Roles";
        public static string Claims => "Claims";
        public static string Register => "Register";
        public static string? IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string? LockNavClass(ViewContext viewContext) => PageNavClass(viewContext, Lock);
        public static string? RolesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Roles);
        public static string? ClaimsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Claims);
        public static string? RegisterNavClass(ViewContext viewContext) => PageNavClass(viewContext, Register);
        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageAreaAdmin"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
