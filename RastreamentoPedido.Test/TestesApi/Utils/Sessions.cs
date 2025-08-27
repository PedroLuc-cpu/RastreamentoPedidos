using RastreamentoPedido.Core.DomainObjects;

namespace RastreamentoPedido.Test.TestesApi.Utils
{
    public sealed class Sessions
    {
        private static volatile Sessions? instance;
        private static readonly Lock sync = new ();

        private Sessions() { }
        public static Sessions Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (sync)
                    {
                        instance ??= new Sessions();
                    }
                }
                return instance;
            }
        }

        public string UserTokenAdministrador { get; set; } = string.Empty;
        public string UserTokenUsuario { get; set; } = string.Empty;
        public string UserTokenTransportadora { get; set; } = string.Empty;
        public string UserTokenEntregador { get; set; } = string.Empty;
        public string UserTokenGerente { get; set; } = string.Empty;
    }
}