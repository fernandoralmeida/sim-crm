using System.Globalization;
using System.Text;

namespace Sim.Domain.Helpers;

public static class Extensions
{
    public static string DoSetores(this int cnae)
    {
        if (cnae >= 1 && cnae <= 3)
        {
            return "Agro";
        }
        else if (cnae >= 45 && cnae <= 47)
        {
            return "Comercio";
        }
        else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
        {
            return "Industria";
        }
        else if (cnae >= 41 & cnae <= 43)
        {
            return "Construcao";
        }
        else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
            || (cnae >= 49 && cnae <= 53)
            || (cnae >= 55 && cnae <= 56)
            || (cnae >= 58 && cnae <= 63)
            || (cnae >= 64 && cnae <= 66)
            || (cnae == 68)
            || (cnae >= 69 && cnae <= 75)
            || (cnae >= 77 && cnae <= 82)
            || (cnae == 85)
            || (cnae >= 86 && cnae <= 88)
            || (cnae >= 86 && cnae <= 88)
            || (cnae >= 90 && cnae <= 93)
            || (cnae >= 94 && cnae <= 96)
            || (cnae == 97)
            || (cnae == 99))
        {
            return "Servicos";
        }
        else
        {
            return "Outros";
        }
    }

    public static string NormalizeText(this string text)
    {
        return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
    }

    public static string Mask(this string value, string mask, char substituteChar = '#')
    {
        int valueIndex = 0;
        try
        {
            return new string(mask.Select(maskChar => maskChar == substituteChar ? value[valueIndex++] : maskChar).ToArray());
        }
        catch (IndexOutOfRangeException e)
        {
            throw new Exception("Valor muito curto para substituir todos os caracteres substitutos na máscara", e);
        }
    }

    public static string MaskRemove(this string valor)
    {
        try
        {
            var str = valor;
            str = new string((from c in str
                              where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c)
                              select c
                   ).ToArray());

            return str;
        }
        catch
        {
            return valor;
        }
    }

}