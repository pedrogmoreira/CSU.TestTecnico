namespace CSU.EtapaTecnica.Domain.Models
{
    public class NotaFiscalItem
    {
        public int CodItem { get; set; }
        public int CodNota { get; set; }
        public int CodPro { get; set; }
        public string DescrPro { get; set; }
        public string Unidade { get; set; }
        public double Qtde { get; set; }
        public double ValorTotal { get; set; }
        public string CodigoProdutoExterno { get; set; }
        public double ValorUnitario { get; set; }

        public NotaFiscal NotaFiscal { get; set; }
    }
}