using Microsoft.AspNetCore.Mvc;
using SistemaPedidos.Api.Models;
using SistemaPedidos.Api.Services;

namespace SistemaPedidos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public IActionResult CriarPedido(Pedido pedido)
        {
            try
            {
                var novoPedido = _pedidoService.CriarPedido(pedido.Cliente, pedido.Produtos);
                return Ok(novoPedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_pedidoService.ListarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var pedido = _pedidoService.ObterPorId(id);

            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            return Ok(pedido);
        }

        [HttpGet("status/{status}")]
        public IActionResult ListarPorStatus(StatusPedido status)
        {
            var pedidos = _pedidoService.ListarPorStatus(status);
            return Ok(pedidos);
        }

        [HttpPut("{id}/pagar")]
        public IActionResult PagarPedido(int id)
        {
            try
            {
                var pedido = _pedidoService.PagarPedido(id);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/cancelar")]
        public IActionResult CancelarPedido(int id)
        {
            try
            {
                var pedido = _pedidoService.CancelarPedido(id);
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}