using System.Net;
using Sim.Application.WebService.RFB.Interfaces;
using Sim.Application.WebService.RFB.Models;
using Sim.Application.WebService.RFB.Helpers;
using System.Text.RegularExpressions;
using System.Text;

namespace Sim.Application.WebService.RFB.Services;

public class ServiceRFB : IServiceRFB
{
    private readonly CookieContainer _cookies = new CookieContainer();
    private readonly string _url = "https://solucoes.receita.fazenda.gov.br/Servicos/cnpjreva/";
    private readonly string _searchpage = "Cnpjreva_Solicitacao_CS.asp";
    private readonly string _validatepage = "valida.asp";
    private readonly string _captchapage = "captcha/gerarCaptcha.asp";

    public string Captcha() {
        string htmlResult;

        using (var wc = new CookieAwareWeb())
        {
            wc.SetCookieContainer(_cookies);
            wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
            wc.Headers[HttpRequestHeader.KeepAlive] = "300";
            htmlResult = wc.DownloadString($"{_url}{_searchpage}");
        }
        if (htmlResult.Length > 0)
        {
            var wc2 = new CookieAwareWeb();
            wc2.SetCookieContainer(_cookies);
            wc2.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
            wc2.Headers[HttpRequestHeader.KeepAlive] = "300";

            byte[] data = wc2.DownloadData($"{_url}{_captchapage}");
            string strewq = "data:image/jpeg;base64," + Convert.ToBase64String(data, 0, data.Length);
            return strewq;
        }
        return null;
    }

    public async Task<ECompany> Search(string document, string captcha) =>
        await Task.Run(() => {
            var _company = new ECompany();

            var request = (HttpWebRequest)WebRequest.Create($"{_url}{_validatepage}");
            request.ProtocolVersion = HttpVersion.Version10;
            request.CookieContainer = _cookies;
            request.Method = "POST";

            string postData = "";
            postData = postData + "origem=comprovante&";
            postData = postData + "cnpj=" + new Regex(@"[^\d]").Replace(document, string.Empty) + "&";
            postData = postData + "txtTexto_captcha_serpro_gov_br=" + captcha + "&";
            postData = postData + "submit1=Consultar&";
            postData = postData + "search_type=cnpj";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            StreamReader stHtml = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));
            string paginaHTML = stHtml.ReadToEnd();

            if (paginaHTML.Contains("Verifique se o mesmo foi digitado corretamente"))
                throw new System.InvalidOperationException("O número do CNPJ não foi digitado corretamente");

            else if (paginaHTML.Contains("Erro na Consulta"))
                throw new System.InvalidOperationException("Os caracteres digitados não correspondem com a imagem");

            else if (paginaHTML.Contains("Esta página tem como objetivo permitir"))
                throw new System.InvalidOperationException("Consulta indisponível, tente novamente mais tarde!");

            else {            
                
                _company.Cnpj = CreatColumns.GetColumnValue(paginaHTML, EColumn.NumeroDaInscricao); //document;
                _company.RazaoSocial = CreatColumns.GetColumnValue(paginaHTML, EColumn.RazaoSocial);
                _company.NomeFantasia = CreatColumns.GetColumnValue(paginaHTML, EColumn.NomeFantasia);
                _company.NaturezaJuridica = CreatColumns.GetColumnValue(paginaHTML, EColumn.NaturezaJuridica);
                _company.AtividadeEconomicaPrimaria = CreatColumns.GetColumnValue(paginaHTML, EColumn.AtividadeEconomicaPrimaria);
                _company.AtividadeEconomicaSecundaria = CreatColumns.GetColumnValue(paginaHTML, EColumn.AtividadesEconomicasSecundarias);
                _company.NumeroDaInscricao = CreatColumns.GetColumnValue(paginaHTML, EColumn.NumeroDaInscricao);
                _company.MatrizFilial = CreatColumns.GetColumnValue(paginaHTML, EColumn.MatrizFilial);
                _company.SituacaoCadastral = CreatColumns.GetColumnValue(paginaHTML, EColumn.SituacaoCadastral);
                _company.DataSituacaoCadastral = CreatColumns.GetColumnValue(paginaHTML, EColumn.DataSituacaoCadastral);
                _company.MotivoSituacaoCadastral = CreatColumns.GetColumnValue(paginaHTML, EColumn.MotivoSituacaoCadastral);

                //Endereço
                _company.Endereco = CreatColumns.GetColumnValue(paginaHTML, EColumn.Endereco);
                _company.Numero = CreatColumns.GetColumnValue(paginaHTML, EColumn.Numero);
                _company.Bairro = CreatColumns.GetColumnValue(paginaHTML, EColumn.Bairro);
                _company.Cidade = CreatColumns.GetColumnValue(paginaHTML, EColumn.Cidade);
                _company.CEP = CreatColumns.GetColumnValue(paginaHTML, EColumn.CEP);
                _company.UF = CreatColumns.GetColumnValue(paginaHTML, EColumn.UF);
                _company.Complemento = CreatColumns.GetColumnValue(paginaHTML, EColumn.Complemento);

                //Contato
                _company.Email = CreatColumns.GetColumnValue(paginaHTML, EColumn.Email);
                _company.Telefones = CreatColumns.GetColumnValue(paginaHTML, EColumn.Telefone);

                _company.Cnae = CreatColumns.GetColumnValue(paginaHTML, EColumn.Cnae);

                _company.DataAbertura = CreatColumns.GetColumnValue(paginaHTML, EColumn.DataAbertura);
                _company.EnteFederativoResponsavel = CreatColumns.GetColumnValue(paginaHTML, EColumn.EnteFederativoResponsavel);
                _company.SituacaoEspecial = CreatColumns.GetColumnValue(paginaHTML, EColumn.SituacaoEspecial);
                _company.DataSituacaoEspecial = CreatColumns.GetColumnValue(paginaHTML, EColumn.DataSituacaoEspecial);
            }
            return _company;
        });
}