﻿using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model.Clientes
{
    //[Table("tp_logradouro")]
    public class TpLogradouro : IAggregateRoot
    {
        //[Key]
        public long idTpLogradouro { get; set; }
        public string nome { get; set; } = string.Empty;
        public string sigla {  get; set; } = string.Empty;
    }
}
