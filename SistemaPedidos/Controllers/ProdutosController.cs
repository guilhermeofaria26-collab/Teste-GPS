using Microsoft.AspNetCore.Mvc;
using SistemaPedidos.Api.Models;

namespace SistemaPedidos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private static readonly List<Produto> _produtos = new();

        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            produto.Id = _produtos.Count + 1;
            _produtos.Add(produto);

            return Ok(produto);
        }

        [HttpGet]
        public IActionResult ListarProdutos()
        {
            return Ok(_produtos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterProdutoPorId(int id)
        {
            var produto = _produtos.FirstOrDefault(p => p.Id == id);

            if (produto == null)
                return NotFound("Produto não encontrado.");

            return Ok(produto);
        }
    }
}