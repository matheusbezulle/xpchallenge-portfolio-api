using Microsoft.AspNetCore.Mvc;

namespace XpChallenge.Portfolio.Api.Controllers
{
    [ApiController]
    public class PortfolioController(ILogger<PortfolioController> logger) : ControllerBase
    {
        private readonly ILogger<PortfolioController> _logger = logger;

        [HttpPost]
        public IActionResult CriarPortfolio()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult ExcluirPortfolio()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult ObterPortfolio()
        {
            return Ok();
        }
    }
}
