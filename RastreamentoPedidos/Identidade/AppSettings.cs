namespace RastreamentoPedidos.Identidade
{
    public class AppSettings
    {
        /// <summary>
        /// Sistema emissor do token
        /// </summary>
        public const string Emissor = "RastreamentoPedidos";
        /// <summary>
        /// Expiração do token em horas a partir do momento que foi gerado.
        /// </summary>
        public const int ExpiracaoHoras = 12;
        /// <summary>
        /// Chave de autenticação do token
        /// </summary>
        public const string Secret = "db6f3bfc02804862125e98b119776d215bbf91ce";
        /// <summary>
        /// Ambiente onde o token é valido.
        /// </summary>
        public const string ValidoEm = "http://localhost";
    }
}