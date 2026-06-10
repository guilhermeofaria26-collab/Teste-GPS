namespace SistemaPedidos.Api.Models
{
    public class HistoricoPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string Acao { get; set; } = string.Empty;
        public DateTime DataAlteracao { get; set; } = DateTime.Now;
    }
}