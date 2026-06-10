public class Pedido
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; } = new();
    public List<Produto> Produtos { get; set; } = new();
    public DateTime DataPedido { get; set; } = DateTime.Now;
    public StatusPedido Status { get; set; } = StatusPedido.Criado;

    public decimal ValorTotal => Produtos.Sum(p => p.Preco);
}