using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace RastreamentoPedido.Core.Utils
{
    public static class StringUtils
    {
        public static string ApenasNumeros(this string? srt)
        {
            if (string.IsNullOrEmpty(srt))
            {
                return string.Empty;
            }
            var retorno = new string(srt.Where(char.IsDigit).ToArray());
            return retorno ?? string.Empty;
        }
        public static string RemoveAcentos(this string? text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (var c in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sbReturn.Append(c);
                }
            }
            return sbReturn.ToString();
        }

        public static string RemoveEspacosDuplos(this string text)
        {
            var retorno = text;
            if (retorno.Contains("  "))
            {
                retorno = retorno.Replace("  ", " ");
                return RemoveEspacosDuplos(retorno);
            }
            return retorno;
        }

        public static string RemoveCaracteresEspeciais(this string value, string documento)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : new Regex("[;\\\\/:*?\"<>|=&--'.¨$#%-+,!@()_]").Replace(value, "").Replace(" ", "").Replace("(", "").Replace(")", "");
        }
    }
}
