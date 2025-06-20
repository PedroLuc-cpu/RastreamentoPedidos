namespace RastreamentoPedido.Core.Helpers
{
    public class GerarCodigoRastreio
    {
        private enum TipoServico
        {
            PAC = 1,
            SEDEX,
            Jadlog,
            TotalExpress,
            AzulCargo,
            Loggi,
            OutrasTransportadoras
        }

        public static string GerarCodigoDeRastreio(string tipoServico, string siglaPais)
        {
            var numeroRatreio = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(tipoServico))
            {
                throw new ArgumentException("O tipo de serviço não pode ser nulo ou vazio.", nameof(tipoServico));
            }
            if (string.IsNullOrWhiteSpace(siglaPais))
            {
                throw new ArgumentException("A sigla do país não pode ser nula ou vazia.", nameof(siglaPais));
            }

            return SetarPrefixo(tipoServico) +
                   numeroRatreio.ToString("N").Substring(0, 8).ToUpper() + 
                   siglaPais.ToUpper();
        }

        private static string SetarPrefixo(string tipoServico)
        {
            switch (tipoServico)
            {
                case "PAC":
                    return "PB";
                case "SEDEX":
                    return "DG";
                case "Jadlog":
                    return "JAD";
                case "TotalExpress":
                    return "TEX";
                case "AzulCargo":
                    return "AZU";
                case "Loggi":
                    return "LOG";
                default:
                    return "OUT";
            }
        }
    }
}
