using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sim.UI.Web.Pages.Shared
{
    public class LayoutNavPages
    {
        public static string Inicio => "Inicio";
        public static string InicioBPP => "InicioBPP";
        public static string InicioPAT => "InicioPAT";
        public static string PATEmpregos => "PATEmpregos";
        public static string PATReports => "PATReports";
        public static string BPPContratos => "BPPContratos";
        public static string BPPReports => "BPPReports";
        public static string BPPRepContracts => "BPPRepContracts";
        public static string InicioSA => "InicioSA";
        public static string SA => "SA";
        public static string SAReports => "SAReports";
        public static string SASimples => "SASimples";
        public static string SEReports => "SEReports";
        public static string InicioSE => "InicioSE";
        public static string InicioHome => "InicioHome";
        public static string Atendimento => "Atendimento";
        public static string AtendimentoDiario => "AtendimentoDiario";
        public static string AtendimentoAtivo => "AtendimentoAtivo";
        public static string AtendimentoConsulta => "AtendimentoConsulta";
        public static string AtendimentoAnonimo => "AtendimentoAnonimo";
        public static string Clientes => "Clientes";
        public static string ClientesEmpresas => "ClientesEmpresas";
        public static string ClientesPessoas => "ClientesPessoas";
        public static string ClientesVincular => "ClientesVincular";
        public static string Agenda => "Agenda";
        public static string AgendaEventos => "AgendaEventos";
        public static string AgendaPlanner => "AgendaPlanner";
        public static string PowerBI => "PowerBI";
        public static string PowerBIPat => "PowerBIPat";
        public static string PowerBIBpp => "PowerBIBpp";
        public static string PowerBIEmpregos => "PowerBIEmpregos";
        public static string PowerBIAtendimento => "PowerBIAtendimento";
        public static string PowerBIEmpresas => "PowerBIEmpresas";
        public static string PowerBIPessoas => "PowerBIPessoas";
        public static string PowerBIEventos => "PowerBIEventos";
        public static string Configuracoes => "Configuracoes";
        public static string ConfigSistema => "ConfigSistema";
        public static string ConfigContas => "ConfigContas";
        public static string Login => "Login";
        public static string LoginPerfil => "LoginPerfil";
        public static string LoginOut => "LoginOut";
        public static string? InicioNavClass(ViewContext viewContext) => PageNavClass(viewContext, Inicio);
        public static string? InicioPATNavClass(ViewContext viewContext) => PageNavClass(viewContext, InicioPAT);
        public static string? PATEmpregosNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PATEmpregos);
        //public static string? PATReportsNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PATReports);
        public static string? InicioBPPNavClass(ViewContext viewContext) => PageNavClass(viewContext, InicioBPP);
        public static string? BPPContratosNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, BPPContratos);
        public static string? BPPReportsNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, BPPReports);
        public static string? BPPRepContractsNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, BPPRepContracts);
        public static string? InicioSANavClass(ViewContext viewContext) => PageNavClass(viewContext, InicioSA);
        public static string? SANavClass(ViewContext viewContext) => PageNavClassLi(viewContext, SA);
        public static string? SAReportsNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, SAReports);
        public static string? SASimplesNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, SASimples);
        public static string? SEReportsNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, SEReports);
        public static string? InicioSENavClass(ViewContext viewContext) => PageNavClassLi(viewContext, InicioSE);
        public static string? InicioHomeNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, InicioHome);
        public static string? AtendimentoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Atendimento);
        public static string? AtendimentoDiarioNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoDiario);
        public static string? AtendimentoAtivoNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoAtivo);
        public static string? AtendimentoConsultaNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoConsulta);
        public static string? AtendimentoAnonimoNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AtendimentoAnonimo);
        public static string? ClientesNavClass(ViewContext viewContext) => PageNavClass(viewContext, Clientes);
        public static string? ClientesEmpresasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, ClientesEmpresas);
        public static string? ClientesPessoasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, ClientesPessoas);
        public static string? ClentesVincularNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, ClientesVincular);
        public static string? AgendaNavClass(ViewContext viewContext) => PageNavClass(viewContext, Agenda);
        public static string? AgendaEventosNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AgendaEventos);
        public static string? AgendaPlannerNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, AgendaPlanner);
        public static string? PowerBINavClass(ViewContext viewContext) => PageNavClass(viewContext, PowerBI);
        public static string? PowerBIPatNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIPat);
        public static string? PowerBIBppNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIBpp);
        public static string? PowerBIEmpregosNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIEmpregos);
        public static string? PowerBIAtendimentoNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIAtendimento);
        public static string? PowerBIEmpresasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIEmpresas);
        public static string? PowerBIPessoasNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIPessoas);
        public static string? PowerBIEventosNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, PowerBIEventos);
        public static string? ConfiguracoesNavClass(ViewContext viewContext) => TopNavClass(viewContext, Configuracoes);
        public static string? ConfigSistemaNavClass(ViewContext viewContext) => TopNavClass(viewContext, ConfigSistema);
        public static string? ConfigContasNavClass(ViewContext viewContext) => TopNavClass(viewContext, ConfigContas);
        public static string? LoginNavClass(ViewContext viewContext) => TopNavClass(viewContext, Login);
        public static string? LoginPerfilNavClass(ViewContext viewContext) => TopNavClass(viewContext, LoginPerfil);
        public static string? LoginOutNavClass(ViewContext viewContext) => PageNavClassLi(viewContext, LoginOut);
        private static string? PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        private static string? TopNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active-1" : null;
        }

        private static string? PageNavClassLi(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePageLi"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

    }
}
