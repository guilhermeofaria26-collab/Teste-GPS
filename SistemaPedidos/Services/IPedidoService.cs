using SistemaPedidos.Api.Models;

namespace SistemaPedidos.Api.Services
{
    public interface IPedidoService
    {
        Pedido CriarPedido(Cliente cliente, List<Produto> produtos);
        Pedido PagarPedido(int pedidoId);
        Pedido CancelarPedido(int pedidoId);
        List<Pedido> ListarPorStatus(StatusPedido status);
        Pedido? ObterPorId(int pedidoId);
        List<Pedido> ListarTodos();
    }
}