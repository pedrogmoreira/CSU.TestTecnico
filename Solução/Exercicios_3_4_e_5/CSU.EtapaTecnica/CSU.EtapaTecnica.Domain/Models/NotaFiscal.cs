using System;
using System.Collections.Generic;

namespace CSU.EtapaTecnica.Domain.Models
{
    public class NotaFiscal
    {
        public int CodNota { get; set; }
        public int? CodVenda { get; set; }
        public string? DestinatarioRemetente { get; set; }
        public DateTime? DtEmissao { get; set; }
        public DateTime? DtSaidaEntrada { get; set; }
        public int? NumNota { get; set; }
        public double? ValorTotalProdutos { get; set; }
        public double? ValorTotalNota { get; set; }
        public int? TransFrete { get; set; }
        public string? NumeroRecibo { get; set; }

        public IEnumerable<NotaFiscalItem> Produtos { get; set; }       
    }
}
