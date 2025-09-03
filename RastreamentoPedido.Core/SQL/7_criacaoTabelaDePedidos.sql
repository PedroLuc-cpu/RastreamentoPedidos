CREATE TABLE pedido (
    id_pedido SERIAL PRIMARY KEY,
    id_cliente INT NOT NULL,
    id_endereco INT NULL,
    id_status INT NOT NULL,
    data_pedido TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    valor_total DECIMAL(10,2) NOT NULL,
    observacao TEXT,
    FOREIGN KEY (id_cliente) REFERENCES cliente("idCliente"),
    FOREIGN KEY (id_endereco) REFERENCES endereco("idEndereco"),
    FOREIGN KEY (id_status) REFERENCES "statusEncomenda"("Id")
);

CREATE TABLE pedido_item (
    id_item SERIAL PRIMARY KEY,
    id_pedido INT NOT NULL,
    id_produto INT NOT NULL,
    quantidade INT NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_pedido) REFERENCES pedido(id_pedido),
    FOREIGN KEY (id_produto) REFERENCES produtos(id_produto)
);

