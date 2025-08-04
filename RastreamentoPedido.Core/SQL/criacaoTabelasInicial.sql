CREATE TABLE pais (
    "idPais" SERIAL PRIMARY KEY,
    nome VARCHAR,
    sigla VARCHAR,
    cod_bcb VARCHAR
);

CREATE TABLE uf (
    "idUF" SERIAL PRIMARY KEY,
    nome VARCHAR,
    sigla VARCHAR,
    "idPais" INT NOT NULL,
    "codUF" INT NOT NULL
);

CREATE TABLE cidade (
    "idCidade" SERIAL PRIMARY KEY,
    nome VARCHAR,
    cep VARCHAR,
    "idUF" INT NOT NULL,
    "codIBJE" VARCHAR
);

CREATE TABLE tp_lougradouro (
    "idLogradouro" SERIAL PRIMARY KEY,
    nome VARCHAR NOT NULL,
    sigla VARCHAR(5) NOT NULL
);

CREATE TABLE est_civil (
    "idEstCivil" SERIAL PRIMARY KEY,
    "estCivil" VARCHAR
);

CREATE TABLE status_encomenda (
    "idStatusEncomenda" SERIAL PRIMARY KEY,
    status VARCHAR
);

CREATE TABLE ponto_parada (
    "idPontoParada" SERIAL PRIMARY KEY,
    "idRota" INT,
    nome VARCHAR,
    localizao VARCHAR,
    ordem INT
);

CREATE TABLE rota (
    "idRota" SERIAL PRIMARY KEY,
    nome VARCHAR,
    descricao VARCHAR,
    "idPontoParada" INT
);

CREATE TABLE cliente (
    "idCliente" SERIAL PRIMARY KEY,
    "idEncomenda" INT,
    "idEstadoCivil" INT NOT NULL,
    "idEmail" INT,
    "idEndereco" INT,
    nome VARCHAR,
    ativo BOOLEAN DEFAULT TRUE,
    "dataNascimento" TIMESTAMP,
    documento VARCHAR(14)
);

CREATE TABLE encomenda (
    "idEncomenda" SERIAL PRIMARY KEY,
    "codigoRastreamento" VARCHAR,
    "idCliente" INT NOT NULL,
    descricao VARCHAR,
    "idStatusEncomenda" INT NOT NULL,
    dt_criacao TIMESTAMP DEFAULT NOW(),
    dt_previsao TIMESTAMP,
    "idRota" INT NOT NULL,
    "idAuditoria" INT
);

CREATE TABLE auditoria (
    "idAuditoria" SERIAL PRIMARY KEY,
    "idEncomenda" INT NOT NULL,
    local_origem TIMESTAMP,
    local_destino TIMESTAMP,
    status_entrega VARCHAR(20),
    status_atual VARCHAR(20),
    descricao_evento VARCHAR(20),
    responsavel VARCHAR(20),
    observacao VARCHAR(50),
    dt_registro TIMESTAMP DEFAULT NOW()
);

CREATE TABLE endereco (
    "idEndereco" SERIAL PRIMARY KEY,
    "idCliente" INT,
    "idLogradouro" INT NOT NULL,
    "idCidade" INT NOT NULL,
    "idEncomenda" INT,
    complemento VARCHAR,
    bairro VARCHAR,
    numero VARCHAR,
    rua VARCHAR,
    cep VARCHAR
);

CREATE TABLE email (
    "idEmail" SERIAL PRIMARY KEY,
    "idCliente" INT,
    email VARCHAR,
    padrao BOOLEAN DEFAULT FALSE,
    departamento VARCHAR
);

CREATE TABLE telefone (
    "idTelefone" SERIAL PRIMARY KEY,
    "idCliente" INT,
    prefixo VARCHAR(2) NOT NULL,
    numero VARCHAR(10) NOT NULL,
    padrao BOOLEAN DEFAULT FALSE
);

ALTER TABLE uf
    ADD FOREIGN KEY ("idPais") REFERENCES pais("idPais");

ALTER TABLE cidade
    ADD FOREIGN KEY ("idUF") REFERENCES uf("idUF");

ALTER TABLE ponto_parada
    ADD FOREIGN KEY ("idRota") REFERENCES rota("idRota");

ALTER TABLE rota
    ADD FOREIGN KEY ("idPontoParada") REFERENCES ponto_parada("idPontoParada");

ALTER TABLE cliente
    ADD FOREIGN KEY ("idEstadoCivil") REFERENCES est_civil("idEstCivil"),
    ADD FOREIGN KEY ("idEmail") REFERENCES email("idEmail"),
    ADD FOREIGN KEY ("idEndereco") REFERENCES endereco("idEndereco"),
    ADD FOREIGN KEY ("idEncomenda") REFERENCES encomenda("idEncomenda");

ALTER TABLE encomenda
    ADD FOREIGN KEY ("idCliente") REFERENCES cliente("idCliente"),
    ADD FOREIGN KEY ("idStatusEncomenda") REFERENCES status_encomenda("idStatusEncomenda"),
    ADD FOREIGN KEY ("idRota") REFERENCES rota("idRota"),
    ADD FOREIGN KEY ("idAuditoria") REFERENCES auditoria("idAuditoria");

ALTER TABLE auditoria
    ADD FOREIGN KEY ("idEncomenda") REFERENCES encomenda("idEncomenda");

ALTER TABLE endereco
    ADD FOREIGN KEY ("idCliente") REFERENCES cliente("idCliente"),
    ADD FOREIGN KEY ("idLogradouro") REFERENCES tp_lougradouro("idLogradouro"),
    ADD FOREIGN KEY ("idCidade") REFERENCES cidade("idCidade"),
    ADD FOREIGN KEY ("idEncomenda") REFERENCES encomenda("idEncomenda");

ALTER TABLE email
    ADD FOREIGN KEY ("idCliente") REFERENCES cliente("idCliente");

ALTER TABLE telefone
    ADD FOREIGN KEY ("idCliente") REFERENCES cliente("idCliente");
