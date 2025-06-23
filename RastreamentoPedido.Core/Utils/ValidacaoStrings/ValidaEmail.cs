using System.Globalization;
using System.Text.RegularExpressions;

namespace RastreamentoPedido.Core.Utils.ValidacaoStrings
{
    public static class ValidaEmail
    {
        public static bool isEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }
            try             {
                // Normaliza o Domínio
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examina a part do dominio do email e noraliza ela
                string DomainMapper(Match match)
                {
                    // Usa a classe IdnMapping para converter para unicode os nome do dominio
                    // Exemplo: www.exemplo.com.br -> www.xn--exemplo-54a.com.br
                    var idn = new IdnMapping();

                    // Retira e processa o nome do domínio (dispara ArgumentException se invalida)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch(RegexMatchTimeoutException)
            {
                return false;
            }
            catch(ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

            }
            catch(RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
