namespace Sim.Application.WebService.RFB.Helpers;

static internal class Validates {
    static internal bool DocumentCaptcha(this string document, string scaptcha) {
        string erro = "";

        if (document.DocumentValid() == false)
            erro += "CNPJ não informado corretamente\n";

        if (scaptcha.Length != 6) // SEMPRE 6 CARACTERES
            erro += "Caracteres não informados corretamente\n";

        if (erro.Length > 0)
        {
            return false;
        }
        else
            return true;
    }

    static internal bool DocumentValid(this string document) {
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma, resto;
        string digito, tempCnpj;

        document = document.Trim();
        document = document.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");

        if (document.Length != 14)
        {
            return false;
        }
        else
        {
            tempCnpj = document.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return document.EndsWith(digito);
        }
    }
}