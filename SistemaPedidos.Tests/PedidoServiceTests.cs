using SistemaPedidos.Api.Models;
using SistemaPedidos.Api.Services;
using Xunit;

namespace SistemaPedidos.Tests
{
    public class PedidoServiceTests
    {
        [Fact]
        public void DeveCriarPedidoComClienteEProdutos()
        {
            var service = new PedidoService();

            var cliente = new Cliente
            {
                Id = 1,
                Nome = "João da Silva",
                CPF = "12345678900"
            };

            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Mouse", Preco = 100 },
                new Produto { Id = 2, Nome = "Teclado", Preco = 200 }
            };

            var pedido = service.CriarPedido(cliente, produtos);

            Assert.NotNull(pedido);
            Assert.Equal(StatusPedido.Criado, pedido.Status);
            Assert.Equal(300, pedido.ValorTotal);
        }

        [Fact]
        public void DevePagarPedido()
        {
            var service = new PedidoService();

            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Maria Souza",
                CPF = "98765432100"
            };

            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Monitor", Preco = 800 }
            };

            var pedido = service.CriarPedido(cliente, produtos);

            var pedidoPago = service.PagarPedido(pedido.Id);

            Assert.Equal(StatusPedido.Pago, pedidoPago.Status);
        }

        [Fact]
        public void NaoDeveCancelarPedidoPago()
        {
            var service = new PedidoService();

            var cliente = new Cliente
            {
                Id = 1,
                Nome = "Carlos Lima",
                CPF = "11122233344"
            };

            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Notebook", Preco = 3500 }
            };

            var pedido = service.CriarPedido(cliente, produtos);

            service.PagarPedido(pedido.Id);

            var erro = Assert.Throws<Exception>(() =>
                service.CancelarPedido(pedido.Id)
            );

            Assert.Equal("Pedido pago não pode ser cancelado.", erro.Message);
        }
    }
}