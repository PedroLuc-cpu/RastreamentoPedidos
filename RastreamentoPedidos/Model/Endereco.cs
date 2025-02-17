﻿using RastreamentoPedidos.DomainObjects;

namespace RastreamentoPedidos.Model
{
    public class Endereco : IAggregateRoot
    {
        public int Id { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public int EncomendaId { get; set; }
        public Encomenda encomenda { get; set; } = new Encomenda();
    }
}
