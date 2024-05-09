using Microsoft.AspNetCore.Mvc;

namespace XpChallenge.Portfolio.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController(ILogger<ProdutoController> logger) : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger = logger;

        [HttpPost]
        public IActionResult IncluirProduto()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult RemoverProduto()
        {
            return Ok();
        }
    }
}
