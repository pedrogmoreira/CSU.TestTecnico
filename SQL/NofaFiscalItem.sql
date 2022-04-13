CREATE TABLE "NOTAFISCALITENS"
(
  "CODITEM"  INTEGER NOT NULL,
  "CODNOTA"  INTEGER,
  "CODPRO"  INTEGER,
  "DESCRPRO"  VARCHAR(80),
  "UNIDADE"  VARCHAR(3),
  "QTDE"  DOUBLE PRECISION,
  "VALORTOTAL"  DOUBLE PRECISION,  
  "CODIGOPRODUTOEXTERNO"  VARCHAR(20),
  "VALORUNITARIO"  DOUBLE PRECISION,    
 PRIMARY KEY ("CODITEM")
);
ALTER TABLE "NOTAFISCALITENS" ADD CONSTRAINT "FK_NOTAFISCALITENS_NOTAFISCAL" FOREIGN KEY ("CODNOTA") REFERENCES "NOTA_FISCAL" ("CODNOTA") ON DELETE CASCADE;
