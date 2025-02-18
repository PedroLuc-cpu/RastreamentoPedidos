using System.Globalization;
using System.Text;

namespace RastreamentoPedidos.Utilis
{
    public static class StringUtils
    {
        public static string ApenasNumeros(this string? str)
        {
            if (str == null)
            {
                return string.Empty;
            }
            var retorno = new string(str.Where(char.IsDigit).ToArray());
            return retorno ?? "";
        }
        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
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
    }
}
