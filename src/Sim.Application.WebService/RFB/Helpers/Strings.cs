namespace Sim.Application.WebService.RFB.Helpers;

static internal class Strings {
    /// <summary>
    /// EXTRAI STRING ENTRE STRINGS
    /// </summary>
    /// <param name="Texto">TEXTO</param>
    /// <param name="StrInicio">String Inicial</param>
    /// <param name="StrFinal">String Final</param>
    /// <returns></returns>
    static internal string EntreString(string Texto, string StrInicio, string StrFinal = "")
    {
        //METODO APRIMORADO. AGORA PEGA O VALOR ENTRE DUAS STRING OU APARTIR DA STRING INICIAL
        try
        {
            int Ini, Fim, Diff;
            Ini = Texto.IndexOf(StrInicio);
            if (Ini == -1) return ""; // SE NAO ENCONTRAR PRIMEIRA PALAVRA, NAO DA ERRO
            Fim = Texto.IndexOf(StrFinal, Ini); // AGORA O FIM SEMPRE COMEÇA APARTIR DA PRIMEIRA STRING
            if (Ini == Fim || Fim == -1) Fim = Texto.Length; // SE NAO TIVER FIM, O FIM SERÁ ULTIMA PALAVRA DO TEXTO
            if (Ini > 0) Ini = Ini + StrInicio.Length;
            if (Fim > 0) Fim = Fim + StrFinal.Length;
            Diff = ((Fim - Ini) - StrFinal.Length);
            if ((Fim > Ini) && (Diff > 0))
                return Texto.Substring(Ini, Diff);
            else
                return "";
        }
        catch (Exception)
        {

            return "";
        }

    }
}