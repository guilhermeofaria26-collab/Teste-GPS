using SistemaPedidos.Api.Models;

namespace SistemaPedidos.Api.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly List<Pedido> _pedidos = new();
        private readonly List<HistoricoPedido> _historicos = new();

        public Pedido CriarPedido(Cliente cliente, List<Produto> produtos)
        {
            if (cliente == null)
                throw new Exception("Cliente é obrigatório.");

            if (produtos == null || !produtos.Any())
                throw new Exception("O pedido deve conter pelo menos um produto.");

            var pedido = new Pedido
            {
                Id = _pedidos.Count + 1,
                Cliente = cliente,
                Produtos = produtos,
                DataPedido = DateTime.Now,
                Status = StatusPedido.Criado
            };

            _pedidos.Add(pedido);

            _historicos.Add(new HistoricoPedido
            {
                Id = _historicos.Count + 1,
                PedidoId = pedido.Id,
                Acao = "Pedido criado",
                DataAlteracao = DateTime.Now
            });

            return pedido;
        }

        public Pedido PagarPedido(int pedidoId)
        {
            var pedido = _pedidos.FirstOrDefault(p => p.Id == pedidoId);

            if (pedido == null)
                throw new Exception("Pedido não encontrado.");

            if (pedido.Status == StatusPedido.Cancelado)
                throw new Exception("Pedido cancelado não pode ser pago.");

            pedido.Status = StatusPedido.Pago;

            _historicos.Add(new HistoricoPedido
            {
                Id = _historicos.Count + 1,
                PedidoId = pedido.Id,
                Acao = "Pedido pago",
                DataAlteracao = DateTime.Now
            });

            return pedido;
        }

        public Pedido CancelarPedido(int pedidoId)
        {
            var pedido = _pedidos.FirstOrDefault(p => p.Id == pedidoId);

            if (pedido == null)
                throw new Exception("Pedido não encontrado.");

            if (pedido.Status == StatusPedido.Pago)
                throw new Exception("Pedido pago não pode ser cancelado.");

            pedido.Status = StatusPedido.Cancelado;

            _historicos.Add(new HistoricoPedido
            {
                Id = _historicos.Count + 1,
                PedidoId = pedido.Id,
                Acao = "Pedido cancelado",
                DataAlteracao = DateTime.Now
            });

            return pedido;
        }

        public List<Pedido> ListarPorStatus(StatusPedido status)
        {
            return _pedidos
                .Where(p => p.Status == status)
                .ToList();
        }

        public Pedido? ObterPorId(int pedidoId)
        {
            return _pedidos.FirstOrDefault(p => p.Id == pedidoId);
        }

        public List<Pedido> ListarTodos()
        {
            return _pedidos;
        }
    }
}