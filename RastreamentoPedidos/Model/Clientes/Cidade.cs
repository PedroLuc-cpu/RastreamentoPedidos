using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model.Clientes
{
    //[Table("cidade")]
    public class Cidade : IAggregateRoot
    {
        //[Key]
        public long idCidade { get; set; }
        public string nome { get; set; } = string.Empty;
        public long idUF { get; set; }
        public UF UF { get; set; } = new UF();
    }
}
