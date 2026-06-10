using Microsoft.AspNetCore.Mvc;
using SistemaPedidos.Api.Models;

namespace SistemaPedidos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private static readonly List<Cliente> _clientes = new();

        [HttpPost]
        public IActionResult CadastrarCliente(Cliente cliente)
        {
            cliente.Id = _clientes.Count + 1;
            _clientes.Add(cliente);

            return Ok(cliente);
        }

        [HttpGet]
        public IActionResult ListarClientes()
        {
            return Ok(_clientes);
        }

        [HttpGet("{id}")]
        public IActionResult ObterClientePorId(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
                return NotFound("Cliente não encontrado.");

            return Ok(cliente);
        }
    }
}