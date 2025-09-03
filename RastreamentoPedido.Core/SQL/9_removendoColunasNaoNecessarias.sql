-- removendo relacionando idRoda da encomanda
ALTER TABLE encomenda DROP COLUMN "idRota";
-- removendo relacionando idAuditoria da encomanda
ALTER TABLE encomenda DROP COLUMN "idAuditoria";


-- removendo relacionando idEncomenda do endereco
ALTER TABLE endereco DROP COLUMN "idEncomenda";

-- removendo relacionando idPontoParada da rota
ALTER TABLE rota DROP COLUMN "idPontoParada";

