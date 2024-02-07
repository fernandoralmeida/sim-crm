using System.Text.RegularExpressions;

namespace Sim.Application.WebService.RFB.Helpers;

static internal class CreatColumns {
    static internal string GetColumnValue(string pattern, EColumn col)
    {
        string S = pattern.Replace("\n", "").Replace("\t", "").Replace("\r", "");

        switch (col)
        {
            case EColumn.RazaoSocial:
                {
                    S = Strings.EntreString(S, ">NOME EMPRESARIAL<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.NomeFantasia:
                {
                    S = Strings.EntreString(S, ">TÍTULO DO ESTABELECIMENTO (NOME DE FANTASIA)<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.NaturezaJuridica:
                {
                    S = Strings.EntreString(S, ">CÓDIGO E DESCRIÇÃO DA NATUREZA JURÍDICA<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.AtividadeEconomicaPrimaria:
                {
                    S = Strings.EntreString(S, "CÓDIGO E DESCRIÇÃO DA ATIVIDADE ECONÔMICA PRINCIPAL");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.AtividadesEconomicasSecundarias:
                {
                    S = Strings.EntreString(S, "ATIVIDADE ECONOMICA SECUNDARIA", "Fim Linha ATIVIDADE ECONOMICA SECUNDARIA");
                    string[] listaAtividades = Regex.Split(S, "<br>");
                    Regex regex = new Regex(@"\s * | * \s", RegexOptions.IgnoreCase | RegexOptions.Compiled); // REGEX PARA REMOVER EXCESSO DE ESPAÇO
                    string atividades = "";

                    foreach (var item in Regex.Split(S, "<br>")) // PERCORRE ENTRE ATIVIDADES
                    {
                        S = Strings.EntreString(item, "<b>", "</b>"); // PEGA ATIVIDADE
                        if (S != "") // SE EXISTIR ATIVIDADE
                            atividades = atividades + Environment.NewLine + (regex.Replace(S, " ")).Trim(); // ADICIONA LINHA E ADICIONA ATIVIDADE REMOVENDO EXCESSO DE ESPAÇOS
                    }

                    return atividades.Trim();
                }
            case EColumn.NumeroDaInscricao:
                {
                    S = Strings.EntreString(S, ">NÚMERO DE INSCRIÇÃO<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.MatrizFilial:
                {
                    S = Strings.EntreString(S, ">NÚMERO DE INSCRIÇÃO<");
                    S = Strings.EntreString(S, "</b>"); // PULA
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.Endereco:
                {
                    S = Strings.EntreString(S, ">LOGRADOURO<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.Numero:
                {
                    S = Strings.EntreString(S, ">NÚMERO<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.Complemento:
                {
                    S = Strings.EntreString(S, ">COMPLEMENTO<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.CEP:
                {
                    S = Strings.EntreString(S, ">CEP<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.Bairro:
                {
                    S = Strings.EntreString(S, ">BAIRRO/DISTRITO<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.Cidade:
                {
                    S = Strings.EntreString(S, ">MUNICÍPIO<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.UF:
                {
                    S = Strings.EntreString(S, ">UF<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.SituacaoCadastral:
                {
                    S = Strings.EntreString(S, ">SITUAÇÃO CADASTRAL<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.DataSituacaoCadastral:
                {
                    S = Strings.EntreString(S, ">DATA DA SITUAÇÃO CADASTRAL<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.MotivoSituacaoCadastral:
                {
                    S = Strings.EntreString(S, ">MOTIVO DE SITUAÇÃO CADASTRAL<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.Email:
                {
                    S = Strings.EntreString(S, ">ENDEREÇO ELETRÔNICO<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }

            case EColumn.SituacaoEspecial:
                {
                    S = Strings.EntreString(S, ">SITUAÇÃO ESPECIAL<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.DataSituacaoEspecial:
                {
                    S = Strings.EntreString(S, ">DATA DA SITUAÇÃO ESPECIAL<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.EnteFederativoResponsavel:
                {
                    S = Strings.EntreString(S, ">ENTE FEDERATIVO RESPONSÁVEL (EFR)<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.DataAbertura:
                {
                    S = Strings.EntreString(S, ">DATA DE ABERTURA<");
                    S = Strings.EntreString(S, "<b>", "</b>");
                    return S.Trim();
                }
            case EColumn.Telefone:
                {
                    S = Strings.EntreString(S, ">TELEFONE<");
                    S = Strings.EntreString(S, "<b>", "</b>"); // APENAS O TELEFONE QUE NAO SEGUE PADRAO POR CONTER ERRO NO HTML
                    return S.Trim();
                }
            case EColumn.Cnae:
                {
                    S = Strings.EntreString(S, ">CÓDIGO E DESCRIÇÃO DA ATIVIDADE ECONÔMICA PRINCIPAL<");
                    S = Strings.EntreString(S, "<b>", " - ");
                    return S.Trim();
                }
            default:
                {
                    return S;
                }

        }
    }
}

